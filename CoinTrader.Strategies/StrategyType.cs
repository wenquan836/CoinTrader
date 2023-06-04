using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Strategies
{
    /// <summary>
    /// 策略类型
    /// </summary>
    public enum StrategyType
    {
        /// <summary>
        /// 资金管理策略
        /// </summary>
        Funds,
        /// <summary>
        /// 现货策略
        /// </summary>
        Spot,
        /// <summary>
        /// 合约策略
        /// </summary>
        Swap
    }
}
