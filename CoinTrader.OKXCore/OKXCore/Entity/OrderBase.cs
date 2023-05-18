using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.Enum;
using System;
using CoinTrader.Common.Util;

namespace CoinTrader.OKXCore.Entity
{
    public class OrderBase
    {
        public int Index { get; set; }
        public string InstId { get; set;}

        public string InstType { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// 总量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 剩余数量，指的是已经总量减去已成交数量
        /// </summary>
        public decimal AvailableAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public long PublicId { get; set; }
        public OrderSide Side { get; set; }

        public string State { get; set; }

        public OrderBase()
        {

        }

        public virtual OrderBase CopyFrom(OrderBase others)
        {
            this.PublicId = others.PublicId;
            this.Price = others.Price;
            this.AvailableAmount = others.AvailableAmount;
            this.Side = others.Side;
            this.CreatedDate = others.CreatedDate;
            this.InstId = others.InstId;
            this.InstType = others.InstType;
            this.Amount = others.Amount;
            this.State = others.State;

            return this;
        }

        public virtual void ParseFromJson (JToken obj)
        {
            this.PublicId = obj.Value<long>("ordId");
            decimal px = 0;
            Decimal.TryParse(obj.Value<string>("px"), out px);
            this.Price = px;

            this.Amount = obj.Value<decimal>("sz");
            this.AvailableAmount = this.Amount - obj.Value<decimal>("fillSz");
            this.Side = obj.Value<string>("side") == "sell" ? OrderSide.Sell : OrderSide.Buy;
            this.CreatedDate = DateUtil.TimestampMSToDateTime( obj.Value<long>("cTime"));
            this.InstId = obj.Value<string>("instId");
            this.InstType = obj.Value<string>("instType");
            this.State = obj.Value<string>("state");
            
            
            /*
            订单状态
canceled：撤单成功
live：等待成交
partially_filled：部分成交
filled：完全成交*/
        }
    }
}
