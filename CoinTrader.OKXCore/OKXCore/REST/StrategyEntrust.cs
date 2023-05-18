using CoinTrader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{


    /// <summary>
    /// 策略委托
    /// </summary>
    public class StrategyEntrust : RestAPIBase
    {

        public StrategyEntrust()
        : base(APIUrl.StrategyEntrust, Http.Method_Post)
        {

        }

        public string instId { get; set; }

        public string tdMode { get; set; }

        public string ccy { get; set; }

        public string side { get; set; }

        public string posSide { get; set; }

        public string ordType { get; set; }

        public string sz { get; set; }

        public string tag { get; set; }

        public string tgtCcy { get; set; }

        public bool reduceOnly { get; set; }

    }
}
