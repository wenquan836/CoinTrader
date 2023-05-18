
using CoinTrader.Common;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CoinTrader.OKXCore.Network
{
    public class SocketConnect
    {
        protected OkxV5API<WebSocket> v5_api = null;
        protected Dictionary<string, Subscribe> subscribeTable = new Dictionary<string, Subscribe>();
        protected bool IsPrivate { get; set; }
 
        protected string GenerateKey(string channel, string instId)
        {
            return string.Format("{0}_{1}", channel,!string.IsNullOrEmpty( instId) ? instId : "null");
        }

        public bool Alive
        {
            get
            {
                return v5_api != null   && v5_api.IsLogin;
            }
        }

        public void Subscribe(string channel, string instId)
        {

            string key = GenerateKey(channel, instId);
            Subscribe subscribe = null;
            if (subscribeTable.ContainsKey(key))
            {
                subscribe = subscribeTable[key];
            }
            else
            {
                subscribe = new Subscribe(channel, instId);
                subscribeTable.Add(key, subscribe);

                if (v5_api != null && v5_api.IsLogin)
                {
                    v5_api.Subscribe(channel, instId);
                }
            }

            subscribe.SubscribeTimes++;
        }

        public void Unsubscribe(string channel, string instId)
        {
            string key = GenerateKey(channel, instId);

            if (subscribeTable.ContainsKey(key))
            {
                Subscribe subscribe = subscribeTable[key];
                subscribe.SubscribeTimes--;

                if (subscribe.SubscribeTimes == 0)
                {
                    if (v5_api != null && v5_api.IsLogin)
                    {
                        v5_api.Unsubscribe(channel, instId);
                    }
                    subscribeTable.Remove(key);
                }
            }
        }

        protected virtual void V5_api_OnData(string channel, string instId, JArray data)
        {

        }
        protected virtual void V5_api_OnLogin(bool success, string msg)
        {
            if (success)
            {
                foreach (var kv in subscribeTable)
                {
                    v5_api.Subscribe(kv.Value.Channel, kv.Value.InstId); //如果是重新连接并登录后重建订阅表
                }
            }
            else
            {
                Logger.Instance.LogError($"{(IsPrivate ? "private" : "public") } socket connect failed {msg}");
            }
        }

        public void Connect(string address)
        {
            if (v5_api == null)
            {
                v5_api = new OkxV5API<WebSocket>(this.IsPrivate,address);
                v5_api.OnLogin += V5_api_OnLogin;
                v5_api.OnData += V5_api_OnData;
            }
        }

        public void CloseConnect()
        {
            if (v5_api != null)
            {
                v5_api.OnLogin -= V5_api_OnLogin;
                v5_api.OnData -= V5_api_OnData;
                v5_api.Close();
                v5_api = null;
            }
        }

        public void SendPing()
        {
            if (v5_api != null && v5_api.IsLogin)
            {
                v5_api.SendPing();
            }
        }
    }
}
