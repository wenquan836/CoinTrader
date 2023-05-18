using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.OKXCore.Network;
using CoinTrader.Common;
using CoinTrader.Common.Interface;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Util;

namespace CoinTrader.OKXCore.Monitor
{
    [MonitorName(Name = "盘口报价")]
    public class TickerMonitor:WebSocketMonitorBase,IDepthProvider
    {
        List<DepthInfo> book = new List<DepthInfo>(5);
        public decimal Ask { get; private set; }
        public decimal Bid { get; private set; }

        private string InstrumentId = "";
        private DepthBook depthBook = new DepthBook();
        public override string CustomName
        {
            get
            {
                return this.InstrumentId;
            }
        }

        public TickerMonitor(string instrumentId)
            :base(false)
        {
            this.InstrumentId = instrumentId;// 
            this.Subscribe(WebsocketChannels.Channel_ticker, instrumentId);
        }

        private void ParseTicker(JToken data)
        {
            var ask = data["askPx"].Value<decimal>();
            var bid = data["bidPx"].Value<decimal>();

            if (ask > 0 && bid > 0)//会跌到0或负数吗?
            {
                this.Ask = ask;
                this.Bid = bid;

                this.Feed();
                base.InvokeOnData();
            }
        }

        private DepthInfo ParseFromJArray(JArray arr)
        {
            var d = default(DepthInfo);
            d.Price = arr[0].Value<decimal>();
            d.Total = arr[1].Value<decimal>();
            d.Orders = arr[3].Value<uint>();

            return d;
        }

        private void ParseDepthBook(JToken data)
        {
            long timestamp = data.Value<long>("ts");//时间戳

            this.depthBook.Time = DateUtil.TimestampMSToDateTime (timestamp);
            JArray askList = data["asks"] as JArray;
            JArray bidList = data["bids"] as JArray;

            foreach (var item in askList)
            {
                var info = item as JArray;
                book.Add(ParseFromJArray(info));
            }

            this.depthBook.Update(DepthBookList.ASK, book);
            book.Clear();

            foreach (var item in bidList)
            {
                var info = item as JArray;
                book.Add(ParseFromJArray(info));
            }
            this.depthBook.Update( DepthBookList.BID, book);
            book.Clear();
            this.Feed();
        }

        /// <summary>
        /// 订阅深度数据，默认是不订阅的
        /// </summary>
        public void SubscribeDepth()
        {
            //this.SubscribeDepth400();
            this.SubscribeDepth5();
        }

        /// <summary>
        /// 取消订阅深度表
        /// </summary>
        public void UnsubscribeDepth()
        {
            //this.UnsubscribeDepth400();
            this.UnsubscribeDepth5();
        }

        /// <summary>
        /// 订阅5档深度表
        /// </summary>
        public void SubscribeDepth5()
        {
            this.Subscribe(WebsocketChannels.Channel_book_depth5, this.InstrumentId);
        }

        /// <summary>
        /// 订阅400档深度表,需要高阶账户
        /// </summary>
        public void SubscribeDepth400()
        {
            this.Subscribe(WebsocketChannels.Channel_book_depth400, this.InstrumentId);
        }

        /// <summary>
        /// 取消订阅5档深度表
        /// </summary>
        public void UnsubscribeDepth5()
        {
            this.Unsubscribe(WebsocketChannels.Channel_book_depth5, this.InstrumentId);
        }

        /// <summary>
        /// 取消订阅400档深度表
        /// </summary>
        public void UnsubscribeDepth400()
        {
            this.Unsubscribe(WebsocketChannels.Channel_book_depth400, this.InstrumentId);
        }

        protected override void ProcessData(string channel, string instId, JArray datas)
        {
            if (string.Compare(instId, this.InstrumentId, true) == 0)
            {
                foreach (JToken jt in datas)
                {
                    if (channel == WebsocketChannels.Channel_ticker)
                    {
                        this.ParseTicker(jt);
                    }
                    else if (channel == WebsocketChannels.Channel_book_depth5)
                    {
                        this.ParseDepthBook(jt);
                    }
                }
            }

            base.ProcessData(channel, instId, datas);
        }

        public void EachDepthBook(DepthBookList side, Action<DepthInfo> callback)
        {
            this.depthBook.EachDepthBook(side, callback);
        }
    }
}
