using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 服务器时间
    /// </summary>
    public class TimeService : RestAPIBase
    {
        public TimeService()
            : base(APIUrl.TimeService)
        {
            this.NeedAuthentication = false;
        }
    }
}
