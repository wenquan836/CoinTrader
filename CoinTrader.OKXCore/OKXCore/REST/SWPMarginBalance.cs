using CoinTrader.Common;
using CoinTrader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 调整保证金
    /// </summary>
    public class SWPMarginBalance : RestAPIBase
    {
        public SWPMarginBalance(string instId, string posSide, decimal amount)
        : base(APIUrl.MarginBalance, Http.Method_Post)
        {
            this.instId = instId;
            this.posSide = posSide;

            this.amt = Math.Abs(amount).ToString();
            this.type = amount > 0 ? "add" : "reduce";

        }

        public string instId { get; set; }
        public string posSide { get; set; }

        public string type { get; set; }

        public string amt { get; set; }

        public string ccy { get; set; }

        public string auto { get; set; }

        public string loanTrans { get; set; }
    }
}
