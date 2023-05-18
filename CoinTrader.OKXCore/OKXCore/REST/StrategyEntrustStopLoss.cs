using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 止损 
    /// </summary>
    public class StrategyEntrustStopLoss : StrategyEntrust
    {
        /// <summary>
        /// 止盈触发价，如果填写此参数，必须填写 止盈委托价
        /// </summary>
        public string slTriggerPx { get; set; }

        /// <summary>
        /// 止盈触发价类型
        /// </summary>
        public string slTriggerPxType { get; set; }

        /// <summary>
        /// 止盈委托价，如果填写此参数，必须填写 止盈触发价
        /// 委托价格为-1时，执行市价止盈
        /// </summary>
        public string slOrdPx { get; set; }
    }
}
