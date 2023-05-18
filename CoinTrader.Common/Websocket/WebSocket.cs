using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace CoinTrader.Common
{
   public  class WebSocket: IWebSocket
    {
        ClientWebSocket _socket;
        CancellationToken token = CancellationToken.None;

        public event Action OnOpen = null;
        public event Action<string> OnError = null;
        public event Action OnClose = null;
        public event Action<byte[],int> OnMessage = null;

        private volatile bool sending = false;
        private volatile bool closed = false;

        const int BufferSize = 1024 * 1024;
        ArraySegment<byte> buffer_receive;

        private Queue<ArraySegment<byte>> sendQueue = new Queue<ArraySegment<byte>>();

        public bool IsOpen
        {
            get
            {
                return this._socket != null && !closed  && this._socket.State == WebSocketState.Open;
            }
        }

        public  WebSocket()
        {
            _socket = new ClientWebSocket();
        }

        async public void ConnectAsync(string address)
        {
            lock (sendQueue)
            {
                sendQueue.Clear();
            }

            closed = false;
            try
            {
                await _socket.ConnectAsync(new Uri(address), token);
            }
            catch(Exception ex)
            {
                OnError?.Invoke(ex.Message);
                return;
            }

            if (_socket.State == WebSocketState.Open)
            {
                this.OnOpen?.Invoke();         
                
                this.buffer_receive = new ArraySegment<byte>(new byte[BufferSize]);
                receiveSocketData(_socket, buffer_receive, token);
            }
            else if (_socket.State == WebSocketState.Closed)
            {
                this.OnError?.Invoke("connect failed");
            }
        }

        async public void receiveSocketData(ClientWebSocket socket, ArraySegment<byte> buffer, CancellationToken token)
        {
            var datas = new byte[BufferSize];
            int index = 0;
            while (true)
            {
                try
                {
                    var result = await socket.ReceiveAsync(buffer, token);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        closed = true;
                        OnClose?.Invoke();
                        break;
                    }
                    
                    var newData = buffer.Array;

                    if(index + result.Count > datas.Length)
                        Array.Resize(ref datas, index + result.Count);
                     
                    Array.Copy(newData, 0,datas, index, result.Count);

                    index += result.Count;
                    if (result.EndOfMessage)
                    {
                        OnMessage?.Invoke(datas, index);
                        index = 0;
                    }
                }
                catch (Exception ex)
                {
                    if (socket.State == WebSocketState.CloseReceived || socket.State == WebSocketState.Closed)
                    {
                        closed = true;
                        OnClose?.Invoke();
                        break;
                    }

                    OnError?.Invoke(ex.Message);
                    break;
                }
            }
        }

        public async void CloseAsync()
        {
            if (this._socket != null)
            {
                closed = true;
                if (_socket.State == WebSocketState.Open)
                {
                    await _socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", token);
                }
            }
        }

         public void SendAsync(string datas)
        {
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(datas);
            this.SendAsync(bytes);
        }

        async public void SendAsync(byte[] datas)
        {
            if (!this.IsOpen)
                return;

            var buff = new ArraySegment<byte>(datas);

            if (sending)
            {
                lock (sendQueue)
                {
                    sendQueue.Enqueue(buff);
                }

                return;
            }

            sending = true;
            try
            {
                await _socket.SendAsync(buff, WebSocketMessageType.Text, true, token);
            }
            catch (Exception ex)
            {
                this.OnError?.Invoke(ex.Message);
            }

            sending = false;
            bool resend = false;
            lock (sendQueue)
            {
                if (sendQueue.Count > 0)
                {
                    buff = sendQueue.Dequeue();
                    resend = true;
                }
            }

            if(resend)
            {
                SendAsync(buff.Array);
            }
        }

        ~WebSocket()
        {
            _socket.Abort();
            _socket.Dispose();
        }
    }
}
