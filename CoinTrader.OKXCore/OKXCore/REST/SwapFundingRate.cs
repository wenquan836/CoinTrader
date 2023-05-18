using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 查询资金费率
    /// </summary>
    public class SwapFundingRate : RestAPIBase
    {
        public SwapFundingRate(string instId)
            : base(APIUrl.FundingRate)
        {
            this.instId = instId;
        }

        public string instId { get; set; }
    }
}
