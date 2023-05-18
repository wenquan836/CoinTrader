using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 合约下单
    /// </summary>
    public class SwapCreateOrder : CreateOrder
    {

        /// <summary>
        /// 合约下单接口
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="side">买或卖</param>
        /// <param name="tdMode">逐仓还是全仓</param>
        public SwapCreateOrder(string instId, string posSide, OrderSide side, string tdMode)
            : base(instId, side, tdMode)
        {
            this.posSide = posSide;
        }
    }
}
