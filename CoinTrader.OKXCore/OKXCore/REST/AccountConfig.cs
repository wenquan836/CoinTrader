using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 获取账户的基本信息
    /// </summary>
    public class AccountConfig : RestAPIBase
    {
        public AccountConfig()
            : base(APIUrl.Account)
        {

        }
    }
}
