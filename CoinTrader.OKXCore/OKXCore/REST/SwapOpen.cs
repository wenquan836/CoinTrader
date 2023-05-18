using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 开仓
    /// </summary>
    public class SwapOpen : SwapCreateOrder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="posSide">要开的仓位如果， PositionSide</param>
        public SwapOpen(string instId, string posSide)
            : this(instId, posSide, MarginMode.Isolated)
        {

        }
        public SwapOpen(string instId, string posSide, string tdMode)
            : base(instId, posSide, posSide == PositionSide.Short ? OrderSide.Sell : OrderSide.Buy, tdMode)
        {

        }
    }
}
