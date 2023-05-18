using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 平空
    /// </summary>
    public class SwapCloseShort : SwapClose
    {
        public SwapCloseShort(string instId, decimal price, int size, bool market)
        : base(instId, PositionSide.Short)
        {
            this.ordType = market ? OrderType.Market : OrderType.Limit;
            this.sz = "" + size;
            if (!market)
                this.px = price.ToString();
        }

        public SwapCloseShort(string instId, int size)
            : this(instId, 0, size, true)
        {

        }

    }
}
