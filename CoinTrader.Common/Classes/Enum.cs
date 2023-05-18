using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoinTrader.Common.Classes
{
    public enum PriceTypeEnum:int
    {
        [Description("固定")]
        Fixed = 1, //固定的
        [Description("浮动")]
        Float = 2  //浮动的
    }

    public enum SideEnum : int
    {
        Sell = 0,
        Buy = 1
    }

    public enum CandleGranularity : uint
    {
        [Description( description:"1分钟")]
        M1 = 60,
        [Description(description: "3分钟")]
        M3 = 180,
        [Description(description: "5分钟")]
        M5 = 300,
        [Description(description: "15分钟")]
        M15 = 900,
        [Description(description: "30分钟")]
        M30 = 1800,
        [Description(description: "1小时")]
        H1 = 3600,
        //H2 = 7200,
        [Description(description: "4小时")]
        H4 = 14400,
        [Description(description: "6小时")]
        H6 = 21600,
        [Description(description: "12小时")]
        H12 = 43200,
        [Description(description: "1日")]
        D1 = 86400,
        [Description(description: "1星期")]
        Week1 = 604800,
        [Description(description: "1个月")]
        Month1 = 2678400,
        //Month3 = 8035200,
        //Month6 = 16070400,
        [Description(description: "1年")]
        Y1 = 31536000
    }

    public enum CTCOrderState:int
    {
        Failed = -2,//:失败
        Canceled = -1,//:撤单成功
        Waiting = 0,//:等待成交
        PartCompleted = 1,//:部分成交
        AllCompleted = 2,//:完全成交
        Placing = 3,//:下单中
        Canceling = 4,//:撤单中
        Uncomplete = 6,//: 未完成（等待成交+部分成交）
        Completed = 7//:已完成（撤单成功+完全成交）
    }

    /// <summary>
    /// 支付通道方向
    /// </summary>
    public enum AccountApplyType:int
    {
        All = 0,
        Receipt = 1,
        Payment = 2
    }

    public enum DrawalAddressType:int 
    {
        Okex = 3,
        Address = 4
    }

    public enum HistoryOrderStateEnum : int
    {
        All = 0,
        Uncomplete = 2,
        Cancelled = 3,
        Completed = 4
    }

    public enum HistroyOrderTypeEnum: int
    {
        All = 0,
        Buy = 1,
        Sell = 2,
    }


    /// <summary>
    /// 对应币币历史订单中的各种订单状态
    /// </summary>
    public enum CTCHistoryOrderState: int
    {
         
        Faild = -2,   //:失败
        Cancelled = -1,//撤单成功
        Waitting = 0,   //:等待成交
        Part = 1, //部分成交 
        FullDeal = 2, //:完全成交
        Placing = 3,//:下单中
        Cancelling = 4, //:撤单中
        Uncomplete = 6, //: 未完成（等待成交+部分成交）
        Completed = 7 //:已完成（撤单成功+完全成交）
    }
}
