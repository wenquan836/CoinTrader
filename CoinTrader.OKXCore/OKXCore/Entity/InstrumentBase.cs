using CoinTrader.Common;
using CoinTrader.Common.Extend;
using CoinTrader.Common.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace CoinTrader.OKXCore.Entity
{
    public class InstrumentBase
    {
        private int GetDigit(decimal num) { return num == 0 ? 0 : (int)Math.Log10((double)(1 / num)); }

        public string QuoteCcy { get; private set; }

        public string BaseCcy { get; private set; }

        public DateTime ListTime { get; private set; }

        public string State{get; private set; }

        public decimal LotSz { get; private set; }

        /// <summary>
        ///币对名称 如："BTC-USDT"
        /// </summary>
        public string InstrumentId { get; private set; }

        /// <summary>
        /// 当前是否处于可交易状态
        /// </summary>
        public bool IsLive { get; set; }

        /// <summary>
        /// 最小可交易数量，如："0.001"
        /// </summary>
        public decimal MinSize { get; private set; }
        public int MinSizeDigit { get { return Math.Max(0, GetDigit(MinSize)); } }

        protected string _amountFormat = null;
        /// <summary>
        /// 格式化时候显示的数字
        /// </summary>
        public virtual string AmountFormat
        {
            get
            {
                if (_amountFormat == null)
                    _amountFormat = MinSize >= 1 ? "0" : (new Regex("[^0\\\\.]")).Replace(this.MinSize.ToString(), "0");// .Replace("1", "0");

                return _amountFormat;
            }
        }

        public int Category { get; private set; }
        /// <summary>
        /// 价格精度，如："0.1"	
        /// </summary>
        public decimal TickSize { get; private set; }
        /// <summary>
        /// 价格精度小数位数:
        /// </summary>
        public int TickSizeDigit { get { return GetDigit(TickSize); } }

        private string _priceFormat = "";
        public string PriceFormat
        {
            get
            {
                if (string.IsNullOrEmpty(_priceFormat))
                {
                    _priceFormat = this.TickSize.ToString().Replace("1", "0");
                }

                return _priceFormat;
            }
        }
        public virtual void ParseFromJson(JToken data)
        {
            this.InstrumentId = data["instId"].Value<string>();
            this.State = data.Value<string>("state");
            this.Category = data.Value<int>("category");

            if (string.Compare(State, "live", true) != 0)
            {
                //不是处于交易中的产品，可能大部分字段都是空字符,
                IsLive = false;
                return;
            }


            this.IsLive = true;
            this.QuoteCcy = data["quoteCcy"].Value<string>();
            this.BaseCcy = data["baseCcy"].Value<string>();
            this.MinSize = data["minSz"].Value<decimal>();
            this.LotSz = data.Value<decimal>("lotSz");
            this.ListTime =  DateUtil.TimestampMSToDateTime(data.ValueWithDefault<long>("listTime"));
            this.TickSize = data["tickSz"].Value<decimal>();
        }
    }
}
