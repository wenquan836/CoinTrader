using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Enum
{
    /// <summary>
    /// 合约保证金模式
    /// </summary>
    public enum SwapMarginMode
    {
        [Description(description: "逐仓")]
        Isolated,

        [Description(description: "全仓")]
        Cross
    }
}
