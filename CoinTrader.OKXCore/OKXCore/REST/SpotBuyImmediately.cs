using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 现货盘口价吃单买入
    /// </summary>
    public class SpotBuyImmediately : SpotCreateOrder
    {
        /**
         * 市价买入
         * notional 金额（USDT）
         */
        public SpotBuyImmediately(string instId, decimal size) : base(instId, OrderSide.Buy)
        {
            this.ordType = OrderType.Market;
            this.sz = "" + size;
        }
    }
}
