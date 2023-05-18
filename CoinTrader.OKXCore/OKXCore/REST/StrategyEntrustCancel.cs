using CoinTrader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{

    /// <summary>
    /// 撤销策略委托
    /// </summary>
    public class StrategyEntrustCancel : RestAPIBase
    {
        public StrategyEntrustCancel()
        : base(APIUrl.CancelStrategyEntrust, Http.Method_Post)
        {

        }

        public string algoId { get; set; }
        public string instId { get; set; }
    }

}
