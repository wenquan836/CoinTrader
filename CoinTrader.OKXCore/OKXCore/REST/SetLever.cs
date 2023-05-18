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
    /// 设置杠杆倍数
    /// </summary>
    public class SetLever : RestAPIBase
    {
        public SetLever()
            : base(APIUrl.SetLever, Http.Method_Post)
        {

        }

        public string instId { get; set; }

        public string ccy { get; set; }

        public string lever { get; set; }

        public string mgnMode { get; set; }

        public string posSide { get; set; }
    }

}
