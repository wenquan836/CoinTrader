using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.VO;
using System;
using System.Collections.Generic;
 

namespace CoinTrader.Strategies.Runtime
{
    public interface ITradeStrategyRuntime:IDisposable
    {
        string InstId { get; }
        bool Init(string instId);

        event Action<decimal, decimal> OnTick;

        bool IsEmulator { get; }

        string QuoteCurrency { get; }
        string BaseCurrency { get; }
        decimal MinSize { get; }

        decimal TickSize { get; }
        
        uint Lever { get; set; }

        bool Effective { get; }
        BalanceVO QuoteBalance { get; } 

        BalanceVO BaseBalance { get; }

        void LoadCandle(CandleGranularity granularity);
        void UnloadCandle(CandleGranularity granularity);

        ICandleProvider GetCandleProvider(CandleGranularity granularity);
        void EachCandle(CandleGranularity granularity, Func<Candle, bool> callback);
        OrderBase GetOrder(long id);

        void CancelOrder(long id);

        /// <summary>
        /// 批量撤销订单
        /// </summary>
        /// <param name="ids"></param>
        void CancelOrders(IEnumerable<long> ids);

        void EachSellOrder(Action<OrderBase> orderCallback);

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="newPrice"></param>
        /// <param name="cancelOrderWhenFailed"></param>
        /// <returns></returns>
        bool ModifyOrder(long id, decimal amount, decimal newPrice, bool cancelOrderWhenFailed);
        void EachBuyOrder(Action<OrderBase> orderCallback);

        void CancelOrderBySide(OrderSide side, bool async);

        long SendOrder(OrderSide side, decimal amount, decimal price, bool postOnly);

        void Sell(decimal amount);
        void Buy(decimal amount);
        void UpdatePrices(decimal ask, decimal bid, DateTime time);
    }
}
