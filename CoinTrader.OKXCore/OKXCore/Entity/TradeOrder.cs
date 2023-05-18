using CoinTrader.Common.Classes;
using CoinTrader.Common.Classes;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Entity
{
    /// <summary>
    /// 交易单
    /// </summary>
    public class TradeOrder : OrderBase
    {
        /// <summary>
        /// 自定义的订单id
        /// </summary>
        public string ClientOid { get; set; }

        public decimal Size { get; set; }

        /// <summary>
        /// 买入金额，市价买入时返回
        /// </summary>
        public decimal Notional { get; set; }  

        /// <summary>
        /// limit或market（默认是limit）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 已成交数量
        /// </summary>
        public decimal FilledSize { get; set; } 

        /// <summary>
        /// 已成交金额
        /// </summary>
        public decimal FilledNotional { get; set; }

        /// <summary>
        ///String	0：普通委托
        ///1：只做Maker（Post only）
        ///2：全部成交或立即取消（FOK）
        ///3：立即成交并取消剩余（IOC）
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        ///state String  订单状态
        ///-2：失败
        ///-1：撤单成功
        ///0：等待成交
        ///1：部分成交
        ///2：完全成交
        ///3：下单中
        ///4：撤单中
        ///</summary>
        public CTCOrderState OrderState { get; set; }

        /// <summary>
        /// 成交均价
        /// </summary>
        public decimal PriceAvg { get; set; }

        /// <summary>
        /// 交易手续费币种，如果是买的话，就是收取的BTC；如果是卖的话，收取的就是USDT
        /// </summary>
        public string FeeCurrency { get; set; }

        /// <summary>
        /// 订单交易手续费。平台向用户收取的交易手续费，为负数，例如：-0.01
        /// </summary>
        public decimal Fee { get; set; }// 

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 返佣金币种 USDT
        /// </summary>
        public string RebateCurrency { get; set; }

        /// <summary>
        /// 返佣金额，平台向达到指定lv交易等级的用户支付的挂单奖励（返佣），如果没有返佣金，该字段为“”，为正数，例如：0.5
        /// </summary>
        public decimal Rebate { get; set; }
        public TradeOrder()
        {

        }

        public void ParseFromDataRow(DataRow row)
        {
            this.PublicId = Convert.ToInt64( row["order_id"]);
            this.ClientOid = row["client_oid"].ToString();
            this.Price = Convert.ToDecimal( row["price"]);
            this.AvailableAmount = Convert.ToDecimal( row["size"]);
            this.Size = this.AvailableAmount;
            this.Notional = Convert.ToDecimal(row["notional"]);
            this.InstId = row["instrument_id"].ToString();
            this.Type = row["type"].ToString();
            this.Side = row["side"].ToString() == "sell" ? OrderSide.Sell : OrderSide.Buy;
            this.CreatedDate = Convert.ToDateTime(row["timestamp"]);
            this.UpdateTime = Convert.ToDateTime(row["update_time"]);
            this.FilledSize = Convert.ToDecimal(row["filled_size"]);
            this.FilledNotional = Convert.ToDecimal( row["filled_notional"]);
            this.OrderType = row["order_type"].ToString();
            this.OrderState = (CTCOrderState)row["state"];
            this.PriceAvg = Convert.ToDecimal( row["price_avg"]);

            this.FeeCurrency = row["fee_currency"].ToString();
            this.Fee = Convert.ToDecimal( row["fee"]);
        }

        public override void ParseFromJson(JToken obj)
        {

            this.PublicId = obj.Value<long>("order_id");
            this.ClientOid = obj.Value<string>("client_oid");
            this.Price = string.IsNullOrEmpty(obj.Value<string>("price")) ? 0 : obj.Value<decimal>("price");
            this.AvailableAmount = obj.Value<decimal>("size");
            this.Size = this.AvailableAmount;
            this.Notional = string.IsNullOrEmpty(obj.Value<string>("notional")) ? 0 : obj.Value<decimal>("notional");// obj.Value<decimal>("notional");
            this.InstId = obj.Value<string>("instrument_id");
            this.Type = obj.Value<string>("type");
            this.Side = obj.Value<string>("side") == "sell" ? OrderSide.Sell: OrderSide.Buy;
            this.CreatedDate = DateTime.Parse(obj.Value<string>("created_at")); // DateTime.Parse( obj.Value<string>("timestamp"));
            this.FilledSize = obj.Value<decimal>("filled_size");
            this.FilledNotional = obj.Value<decimal>("filled_notional");
            this.OrderType = obj.Value<string>("order_type");
            this.OrderState = (CTCOrderState) obj.Value<int>("state");
            this.PriceAvg = obj.Value<decimal>("price_avg");

            this.FeeCurrency = obj.Value<string>("fee_currency");
            this.Fee = string.IsNullOrEmpty(obj.Value<string>("fee")) ? 0 : obj.Value<decimal>("fee");
        }
    }
}
