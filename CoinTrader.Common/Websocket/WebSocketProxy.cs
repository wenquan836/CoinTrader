using CoinTradeGecko.Invoke;
using Gecko;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeGecko
{
    /**
 * 其中 op 的取值为 1--subscribe 订阅； 2-- unsubscribe 取消订阅 ；3--login 登录
 */
    public enum APIOperate
    {
        Login = 3,   //登录
        Subscribe = 1,//订阅
        Unsubscribe = 2,//取消订阅
    }


    class WssInvoke: JSInvoke
    {
        [JsonProperty]
        public string hconnect { get; set; }
    }

    class connect: WssInvoke
    {
        [JsonProperty]
        public string address { get; set; }
    }

    class send : WssInvoke
    {
        [JsonProperty]
        public string data { get; set; }
    }

    class close : WssInvoke
    {

    }

    public class WebSocketProxy:IWebSocket
    {
        const string js_namespace   = "wss_proxy";
        const string msg_onOpen     = "wss_onOpen";
        const string msg_onError    = "wss_onError";
        const string msg_onMessage  = "wss_onMessage";
        const string msg__onClose   = "wss_onClose";

        static int hconnect = 1;
        private string hConnect = "";  //js连接句柄

        BrowserProxy browserProxy = null;

        private void RegMsg(string msg, Action<string> handler)
        {
            browserProxy.browser.AddMessageEventListener(msg, handler);
        }

        public WebSocketProxy(BrowserProxy browser)
        {
            
            this.browserProxy = browser;

            RegMsg(msg_onOpen, _js_wss_onOpen);
            RegMsg(msg_onError, _js_wss_onError);
            RegMsg(msg_onMessage, _js_wss_onMessage);
            RegMsg(msg__onClose, _js_wss_onClose);

        }

        /*获取现货最新成交价、买一价、卖一价和24交易量

        send示例
{"op": "subscribe", "args": ["spot/ticker:ETH-USDT"]
    }*/
        public void ConnectAsync(string address)
        {
            if (!string.IsNullOrEmpty(this.hConnect))
            {
                throw new Exception("areadly connect connect");
            }

            this.hConnect = (hconnect++).ToString();
            connect invoke = new connect() { Namespance = js_namespace, address = address, hconnect = this.hConnect };

            invoke.execAsync(browserProxy);
        }

        public event Action OnOpen = null;

        public event Action<string> OnError = null;

        public event Action OnClose = null;

        public event Action<byte[]> OnMessage = null;
   
        /**
         * 发送心跳
         * 
         */
        public void SendPing()
        {
            
        }

        public void SendAsync(byte[] data)
        {
            if (!string.IsNullOrEmpty(this.hConnect))
            {
                SendAsync(UTF8Encoding.UTF8.GetString(data));
            }
        }

        public void SendAsync(string data)
        {
            if (!string.IsNullOrEmpty( this.hConnect))
            {
                send invoke = new send() { data = data, Namespance = js_namespace, hconnect = this.hConnect};
                invoke.execAsync(browserProxy); 
            }
        }

        public void Send(JObject json)
        {
            string str = json.ToString();
            this.SendAsync(str);
        }

        private void Cleanup()
        {
            this.hConnect = null;
        }

        public void CloseAsync()
        {
            var invoke = new close() { hconnect = this.hConnect, Namespance = js_namespace };
            invoke.execAsync(browserProxy);
        }

        void _js_wss_onOpen(string data)
        {
            if (data == this.hConnect)
            {
                this.OnOpen?.Invoke();
            }
        }

        void _js_wss_onError(string data)
        {
            if (data == this.hConnect)
            {
                this.OnError?.Invoke("");
            }
        }

        void _js_wss_onMessage(string data)
        {
            int index = data.IndexOf("|");
            string h = data.Substring(0, index);
            data = data.Substring(index + 1);
            if(h == this.hConnect)
            {
                byte[] bytes = Convert.FromBase64String(data);
                this.OnMessage?.Invoke(bytes);
            }
        }

        void _js_wss_onClose(string data)
        {
            if(data == this.hConnect)
            {
                this.OnClose?.Invoke();
            }
        }
    }
}
