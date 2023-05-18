using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{

    /// <summary>
    /// 平仓
    /// </summary>
    public class SwapClose : SwapCreateOrder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="posSide">要平的仓位如果， Const.PositionSide</param>
        public SwapClose(string instId, string posSide)
            : this(instId, posSide, MarginMode.Isolated)
        {

        }
        public SwapClose(string instId, string posSide, string tdMode)
            : base(instId, posSide, posSide == PositionSide.Short ? OrderSide.Buy : OrderSide.Sell, tdMode)
        {

        }
    }
}
