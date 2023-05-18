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
    /// 拉取蜡烛图数据
    /// </summary>
    public class CandleList : RestAPIBase
    {
        public string instId { get; set; }

        public string bar { get; set; }

        public string before { get; set; }

        public int limit { get; set; } = 100;


        public CandleList(string instrumentId, string granularity)
            : base(APIUrl.Candle, Http.Method_Get)
        {
            this.instId = instrumentId;
            this.bar = granularity;
            this.limit = 200;
        }
    }
}
