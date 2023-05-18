using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 现货订单
    /// </summary>
    public class SpotCreateOrder : CreateOrder
    {
        public SpotCreateOrder(string instId, OrderSide side)
            : base(instId, side, MarginMode.Cash)
        {



        }
    }
}
