using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 合约持仓查询
    /// </summary>
    public class SWPPositions : RestAPIBase
    {
        public SWPPositions()
        : base(APIUrl.Position)
        {
            this.instType = InstrumentType.Swap;
        }

        public string instType { get; set; }
        public string instId { get; set; }
        public string posId { get; set; }
    }
}
