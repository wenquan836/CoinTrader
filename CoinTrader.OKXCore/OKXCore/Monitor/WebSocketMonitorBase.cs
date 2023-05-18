 
using CoinTrader.OKXCore.Network;
using CoinTrader.OKXCore.VO;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore;
using System.Collections.Generic;
using CoinTrader.OKXCore.Event;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.Common.Interface;
using CoinTrader.Common;

namespace CoinTrader.OKXCore.Monitor
{
    /// <summary>
    /// WebSocket接口的数据监视器
    /// </summary>
    public abstract class WebSocketMonitorBase : MonitorBase, IEventListener
    {
        private Dictionary<string, SubscribeVO> subscribeTable = new Dictionary<string, SubscribeVO>();

        SocketConnect connecttion = null;
        protected bool isPrivate = false;//是否是私有数据
        private const int TIMEOUT = 3000;

        protected virtual void ProcessData(string channel, string instId, JArray datas)
        {

        }

        public override bool Effective { get =>connecttion!= null && connecttion.Alive;}

        public WebSocketMonitorBase(bool isPrivate)
        {
            if (isPrivate)
            {
                connecttion = PrivateSocketConnect.Instance;
            }
            else
            {
                connecttion = PublicSocketConnect.Instance;
            }
             
            this.isPrivate = isPrivate;
            this.Interval = TIMEOUT;
            EventCenter.Instance.AddListener(this);
        }


        protected void Subscribe(string channel, string instId)
        {
            string s = channel + "|" + instId;
            if (!this.subscribeTable.ContainsKey(s))
            {
                var vo = new SubscribeVO() { Channel = channel, InstId = instId };
                connecttion.Subscribe(channel, instId);
                //EventCenter.Instance.Emit(CoreEvent.PublicSocketSubscribe, vo);
                this.subscribeTable.Add(s, vo);
            }
        }

        protected void Unsubscribe(string channel, string instId)
        {
            string s = channel + "|" + instId;
            if (this.subscribeTable.TryGetValue(s, out var vo))
            {
                connecttion.Unsubscribe(vo.Channel,vo.InstId); //  EventCenter.Instance.Emit(CoreEvent.PublicSocketUnsubscribe, vo);
                this.subscribeTable.Remove(s);
            }
        }

        /// <summary>
        /// 关闭所有订阅， 销毁对象的时候
        /// </summary>
        private void UnsubscribeAll()
        {
            foreach (var kv in this.subscribeTable)
            {
                var vo = kv.Value;
                connecttion.Unsubscribe(vo.Channel,vo.InstId);

                //EventCenter.Instance.Emit(CoreEvent.PublicSocketUnsubscribe, kv.Value);
            }
            this.subscribeTable.Clear();
        }

        private void Api_OnData(object obj)
        {
            SocketDataVO dataWapper = obj as SocketDataVO;

            if (dataWapper != null)
            {
                this.ProcessData(dataWapper.Channel, dataWapper.InstId, dataWapper.Data);
            }
        }


        public override void Dispose()
        {
            UnsubscribeAll();
            EventCenter.Instance.RemoveListener(this);
            base.Dispose();
        }

        protected override void RunInvoke()
        {

        }

        public IEnumerable<EventListenItem> GetEvents()
        {
            string dataEvent = isPrivate ? CoreEvent.PrivateSocketData : CoreEvent.PublicSocketData;
            return new EventListenItem[] { new EventListenItem(dataEvent, this.Api_OnData) };
        }
    }
}
