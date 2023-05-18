using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Enum
{
    /// <summary>
    /// 合约持仓方向
    /// </summary>
    public enum PositionType
    {
        [Description(description: "多头")]
        Long,
        [Description(description: "空头")]
        Short
    }
}
