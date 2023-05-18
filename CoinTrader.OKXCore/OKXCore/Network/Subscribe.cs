using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Network
{
    /// <summary>
    /// 订阅信息
    /// </summary>
    public class Subscribe
    {
        public Subscribe(string channel, string instId)
        {
            this.InstId = instId;
            this.Channel = channel;
            this.SubscribeTimes = 0;
        }
        public string InstId { get; private set; }
        public string Channel { get; private set; }

        /// <summary>
        /// 被订阅次数，用于对多次订阅计数，当计数为0的时候就取消订阅
        /// </summary>
        public int SubscribeTimes { get; set; }
    }

}
