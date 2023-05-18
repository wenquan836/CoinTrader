using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 现货盘口价吃单卖出
    /// </summary>
    public class SpotSellImmediately : SpotCreateOrder
    {

        /**
         * 市价卖出
         * size 数量
         */
        public SpotSellImmediately(string instId, decimal size) : base(instId, OrderSide.Sell)
        {
            this.ordType = OrderType.Market;
            this.sz = "" + size;
        }
    }

}
