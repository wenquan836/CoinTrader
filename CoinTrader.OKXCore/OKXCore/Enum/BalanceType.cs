using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Enum
{
    /// <summary>
    /// 余额位置
    /// </summary>
    public enum BalanceType
    {
        /// <summary>
        /// 资金账户
        /// </summary>
        Account = 6,
        /// <summary>
        /// 交易账户
        /// </summary>
        Trading = 18,
    }
}
