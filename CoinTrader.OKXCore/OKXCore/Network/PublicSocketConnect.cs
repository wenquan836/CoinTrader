 using CoinTrader.OKXCore.VO;
 using CoinTrader.Common;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using CoinTrader.Common.Interface;
using System;
using CoinTrader.OKXCore.Event;
using CoinTrader.Common.Interface;
using CoinTrader.Common;

namespace CoinTrader.OKXCore.Network
{
    /// <summary>
    /// 公共socket频道的连接
    /// </summary>
    public class PublicSocketConnect : SocketConnect, IEventListener
    {
        SocketDataVO dataWapper = new SocketDataVO();
        PublicSocketConnect()
        {
            this.IsPrivate = false;
            EventCenter.Instance.AddListener(this);
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

        protected override void V5_api_OnData(string channel, string instId, JArray data)
        {
            dataWapper.SetDatas(channel, instId, data);
            EventCenter.Instance.Send(CoreEvent.PublicSocketData, dataWapper);
        }

        private void OnSysTick(object objTick)
        {
            if (DateTime.Now.Second % 10 == 0)
            {
                SendPing();
            }
        }

        public IEnumerable<EventListenItem> GetEvents()
        {
            return new EventListenItem[] {
                new EventListenItem(CoreEvent.PublicSocketSubscribe, OnReceiveEventSubscribe)
                ,new EventListenItem(CoreEvent.PublicSocketUnsubscribe, OnReceiveEventUnsubscribe)
                ,new EventListenItem(CoreEvent.SystemTick,OnSysTick)
            };
        }

        private static PublicSocketConnect _instance;

        private static object lockIns = new object();
        public static PublicSocketConnect Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockIns)
                    {
                        if (_instance == null)
                        {
                            _instance = new PublicSocketConnect();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
