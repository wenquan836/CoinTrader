using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoinTrader.Common
{
    public  interface IWebSocket
    {
        event Action OnOpen;
        event Action<string> OnError;
        event Action OnClose;
        event Action<byte[],int> OnMessage;
        void ConnectAsync(string address);
        void CloseAsync();
        void SendAsync(byte[] datas);
        void SendAsync(string datas);
    }
}
