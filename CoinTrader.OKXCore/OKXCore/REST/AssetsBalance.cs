using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 获取资金账户结余
    /// </summary>
    public class AssetsBalance : RestAPIBase
    {

        public AssetsBalance()
            : base(APIUrl.AsstetsBalance)
        {

        }
    }
}
