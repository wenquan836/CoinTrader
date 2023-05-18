using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.VO
{
    /// <summary>
    /// 消息订阅、取消订阅
    /// </summary>
    public class SubscribeVO
    {
        public string Channel { get; set; }
        public string InstId { get; set; }
    }


}
