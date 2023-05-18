using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Entity
{
    public class InstrumentSpot:InstrumentBase
    {

        /// <summary>
        ///交易货币币种
        /// </summary>
        public string BaseCurrency { get; private set; }

        /// <summary>
        /// 计价货币币种
        /// </summary>
        public string QuoteCurrency { get; private set; } 

        public override void ParseFromJson(JToken data)
        {
            base.ParseFromJson(data);

            this.BaseCurrency = data["baseCcy"].Value<string>();
            this.QuoteCurrency = data["quoteCcy"].Value<string>();
        }
    }
}
