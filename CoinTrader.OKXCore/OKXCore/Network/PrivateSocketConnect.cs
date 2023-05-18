using CoinTrader.OKXCore.VO;
using CoinTrader.Common;
using CoinTrader.Common.Interface;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinTrader.OKXCore.Event;
using CoinTrader.Common.Interface;
using CoinTrader.Common;

namespace CoinTrader.OKXCore.Network
{
    /// <summary>
    /// 私有频道的websocket连接
    /// </summary>
    public class PrivateSocketConnect : SocketConnect, IEventListener
    {
        SocketDataVO dataWapper = new SocketDataVO();
        internal PrivateSocketConnect()
        {
            this.IsPrivate = true;
            EventCenter.Instance.AddListener(this);
        }

        public IEnumerable<EventListenItem> GetEvents()
        {
            return new EventListenItem[] {
                new EventListenItem(CoreEvent.PrivateSocketSubscribe, OnReceiveEventSubscribe)
                ,new EventListenItem(CoreEvent.PrivateSocketUnsubscribe, OnReceiveEventUnsubscribe)
                ,new EventListenItem(CoreEvent.SystemTick,OnSysTick)
            };
        }
        
        protected override void V5_api_OnData(string channel, string instId, JArray data)
        {
            dataWapper.SetDatas(channel, instId, data);
            EventCenter.Instance.Send(CoreEvent.PrivateSocketData, dataWapper);
        }
        protected override void V5_api_OnLogin(bool success, string msg)
        {
            Logger.Instance.LogDebug("private socket connect is " + (success ? "success" : "failed"));

            base.V5_api_OnLogin(success, msg);
        }

        private void OnSysTick(object objTick)
        {
            if (DateTime.Now.Second % 10 == 0)
            {
                SendPing();
            }
        }

        private void OnReceiveEventSubscribe(object args)
        {
            SubscribeVO vo = args as SubscribeVO;
            if (vo != null)
            {
                Subscribe(vo.Channel, vo.InstId);
            }
        }

        private void OnReceiveEventUnsubscribe(object args)
        {
            SubscribeVO vo = args as SubscribeVO;
            if (vo != null)
            {
                Unsubscribe(vo.Channel, vo.InstId);
            }
        }


        private static PrivateSocketConnect _instance;

        private static object lockIns = new object();
        public static PrivateSocketConnect Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockIns)
                    {
                        if (_instance == null)
                        {
                            _instance = new PrivateSocketConnect();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
