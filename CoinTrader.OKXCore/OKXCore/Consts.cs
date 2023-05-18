using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  CoinTrader.OKXCore
{

    public static class OrderStatus
    {
        public static readonly string Completed = "已完成";
        public static readonly string Canceled = "已取消";
    }

    /// <summary>
    /// 持仓方向
    /// </summary>
    public static class PositionSide
    {
        public static readonly string Net = "net";
        /// <summary>
        /// 空头
        /// </summary>
        public static readonly string Short = "short";
        /// <summary>
        /// 多头
        /// </summary>
        public static readonly string Long = "long";
    }


    /// <summary>
    /// 交易方式
    /// </summary>
    public static class MarginMode
    {
        /// <summary>
        /// 逐仓保证金模式
        /// </summary>
        public static readonly string Isolated = "isolated";
        /// <summary>
        /// 全仓保证金模式
        /// </summary>
        public static readonly string Cross = "cross";
        /// <summary>
        /// 现货非保证金模式
        /// </summary>
        public static readonly string Cash = "cash";
    }

    /// <summary>
    /// 下单方向
    /// </summary>
    public static class Side
    {
        public static readonly string Sell = "sell";
        public static readonly string Buy = "buy";
    }

    /// <summary>
    /// 交易产品类型
    /// </summary>
    public static class InstrumentType
    {
        /// <summary>
        /// 现货
        /// </summary>
        public static readonly string Spot = "SPOT";
        /// <summary>
        /// 币币杠杆
        /// </summary>
        public static readonly string Margin = "MARGIN";

        /// <summary>
        /// 永续合约
        /// </summary>
        public static readonly string Swap = "SWAP";

        /// <summary>
        /// 交割合约
        /// </summary>
        public static readonly string Futures = "FUTURES";

        /// <summary>
        /// 期权
        /// </summary>
        public static readonly string Option = "OPTION";
    }

    /// <summary>
    /// 订单类型
    /// </summary>
    public static class OrderType
    {
        /// <summary>
        /// 市价单
        /// </summary>
        public static readonly string Market = "market";

        /// <summary>
        /// 限价单
        /// </summary>
        public static readonly string Limit = "limit";

        /// <summary>
        /// 只做maker单
        /// </summary>
        public static readonly string PostOnly = "post_only";

        /// <summary>
        /// 全部成交或立即取消
        /// </summary>
        public static readonly string Fok = "fok";

        /// <summary>
        /// 立即成交并取消剩余
        /// </summary>
        public static readonly string IOC = "ioc";

        /// <summary>
        /// 市价委托立即成交并取消剩余（仅适用交割、永续）
        /// </summary>
        public static readonly string OptimalLimitIoc = "optimal_limit_ioc";

    }
}
