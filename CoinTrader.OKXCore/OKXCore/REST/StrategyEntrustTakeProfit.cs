using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 止盈 
    /// </summary>
    public class StrategyEntrustTakeProfit : StrategyEntrust
    {
        /// <summary>
        /// 止盈触发价，如果填写此参数，必须填写 止盈委托价
        /// </summary>
        public string tpTriggerPx { get; set; }

        /// <summary>
        /// 止盈触发价类型
        /// </summary>
        public string tpTriggerPxType { get; set; }

        /// <summary>
        /// 止盈委托价，如果填写此参数，必须填写 止盈触发价
        /// 委托价格为-1时，执行市价止盈
        /// </summary>
        public string tpOrdPx { get; set; }
    }
}
