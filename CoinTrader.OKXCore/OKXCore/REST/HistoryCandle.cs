using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    public class HistoryCandle : RestAPIBase
    {
        public HistoryCandle()
            : base(APIUrl.HistoryCandle)
        {

        }

        public string instId { get; set; }

        public string bar { get; set; }

        public string after { get; set; }

        public int limit { get; set; } = 100;
    }
}
