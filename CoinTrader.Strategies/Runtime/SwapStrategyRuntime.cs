using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.Common.Util;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.VO;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CoinTrader.Strategies.Runtime
{

    /// <summary>
    /// 合约策略运行时
    /// </summary>
    public class SwapStrategyRuntime : ITradeStrategyRuntime
    {
        private string instId;
        private MarketDataProvider dataProvider = null;
        private InstrumentSwap instrument = null;
        private SWPFundingRateMonitor fundingRateMonitor = null;
        
        public string InstId => instId;
        public bool Init(string instId)
        {
            this.instId = instId;
            dataProvider = DataProviderManager.Instance.GetProvider(instId);

            if (dataProvider == null) return false;
            instrument = InstrumentManager.SwapInstrument.GetInstrument(instId);

            dataProvider.OnTick += DataProvider_OnTick;

            fundingRateMonitor = new SWPFundingRateMonitor(InstId);
            dataProvider.AddMonitor(fundingRateMonitor);

            /*
            LeverStorageKey = $"{this.GetType().Name}_lever_{instId}";
           
            uint lever = LocalStorage.GetValue<uint>(LeverStorageKey);

            if (lever == 0)
                lever = 5;

            this.Lever = lever;
            */
            return true;
        }

        private void DataProvider_OnTick(decimal ask, decimal bid)
        {
            this.OnTick?.Invoke(ask, bid);
            UpdatePrices(ask, bid, DateUtil.GetServerDateTime());
        }

        uint lever = 5;
        public uint Lever
        {
            get
            {
                return lever;
            }
            set
            {
                value = Math.Min(value,instrument.Lever);
                value = Math.Max(value, 1);
                lever = value;
                ResetLever();
            }
        }

        private void ResetLever()
        {
            AccountManager.SetLever(instId, PositionType.Short, MarginMode, lever);
            AccountManager.SetLever(instId, PositionType.Long, MarginMode, lever);
            //LocalStorage.SetValue(LeverStorageKey,lever);
        }

        /// <summary>
        /// 获取最大杠杆倍数
        /// </summary>
        /// <returns></returns>
        public uint GetMaxLever()
        {
            return instrument.Lever;
        }

        /// <summary>
        /// 设置杠杆
        /// </summary>
        /// <param name="mode">保证金模式(逐仓和全仓)</param>
        /// <param name="lever">倍数</param>
        public void SetLever(PositionType side, SwapMarginMode mode, uint lever)
        {
            if (lever < 1 || lever > GetMaxLever())
                return;

            AccountManager.SetLever(InstId, side, mode, lever);
        }

        public BalanceVO BaseBalance
        {
            get
            {
                return AssetsManager.Instance.GetBalance(BalanceType.Trading, BaseCurrency);
            }
        }

        /// <summary>
        /// 获取交易账户计价币种的结余
        /// </summary>
        public BalanceVO QuoteBalance
        {
            get
            {
                return AssetsManager.Instance.GetBalance(BalanceType.Trading, QuoteCurrency);
            }
        }
        public bool IsEmulator => false;

        /// <summary>
        /// 计价币种
        /// </summary>
        public string QuoteCurrency => instrument.QuoteCcy;
        
        /// <summary>
        /// 交易币种
        /// </summary>
        public string BaseCurrency => instrument.BaseCcy;

        /// <summary>
        /// 最小交易数量， 1张合约* 合约面值
        /// </summary>
        public decimal MinSize => instrument.CtVal * instrument.MinSize;

        public decimal TickSize => instrument.TickSize;

        public bool Effective => (dataProvider.Effective && MonitorSchedule.Default.AllIsEffective());

        /// <summary>
        /// 保证金模式， 全仓或逐仓
        /// </summary>
        public SwapMarginMode MarginMode { get; set; } = SwapMarginMode.Isolated;

 
        public event Action<decimal, decimal> OnTick;



        /// <summary>
        /// 获取资金费率
        /// </summary>
        /// <returns></returns>
        public double GetFundingRate()
        {
            
             return this.fundingRateMonitor!= null? this.fundingRateMonitor.FundingRate.Rate : 0;
        }

        /// <summary>
        /// 平仓某个方向指定数量的仓位
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private decimal ClosePoistionWithAmount(decimal amount, PositionType type)
        {
            var lPos = GetPosition(type);

            if(lPos != null)
            {
                var closeAmt = Math.Min(amount, PositionManager.SizeToAmount(instId, lPos.AvailPos));
                if (PositionManager.Instance.ClosePosition(lPos.PosId, closeAmt))
                {
                    amount -= closeAmt;
                    if (amount <= 0)
                        return 0;
                }
            }

            return amount;
        }

        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        public void ClosePosition(long id)
        {
            PositionManager.Instance.ClosePosition(id);
        }

        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount">数量</param>
        public void ClosePosition(long id, decimal amount)
        {
            PositionManager.Instance.ClosePosition(id, amount);
        }


        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        /// <param name="size">合约张数</param>
        public void ClosePosition(long id, int size)
        {
            PositionManager.Instance.ClosePosition(id, size);
        }

        /// <summary>
        /// 市价创建仓位
        /// </summary>
        /// <param name="side">方向</param>
        /// <param name="amount">数量</param>
        /// <param name="mode">仓位模式</param>
        /// <returns>返回非0则成功</returns>
        public long CreatePosition(PositionType side, decimal amount, SwapMarginMode mode)
        {
            return PositionManager.Instance.CreatePosition(InstId, side, amount, mode);
        }


        /// <summary>
        /// 遍历所有持仓
        /// </summary>
        /// <param name="callback"></param>
        public void EachPosition(Action<Position> callback)
        {
            PositionManager.Instance.EachPosition(callback);
        }

        /// <summary>
        /// 市价创建仓位
        /// </summary>
        /// <param name="side">方向</param>
        /// <param name="size">合约张数</param>
        /// <param name="mode">仓位模式</param>
        /// <returns>返回非0则成功</returns>
        public long CreatePosition(PositionType side, int size, SwapMarginMode mode)
        {
            return PositionManager.Instance.CreatePosition(InstId, side, size, mode);
        }


        /// <summary>
        /// 建立多头仓位或从市场买入平仓空头仓位
        /// </summary>
        /// <param name="amount"></param>
        public void Buy(decimal amount)
        {
            //如果足够覆盖卖出份额的话，优先平仓空头持仓
            amount = ClosePoistionWithAmount(amount, PositionType.Short);
           if(amount > 0)  PositionManager.Instance.CreatePosition(instId, PositionType.Long, amount, MarginMode);
        }

        /// <summary>
        /// 市价卖出
        /// </summary>
        /// <param name="amount"></param>
        public void Sell(decimal amount)
        {
            // 如果足够覆盖卖出份额的话，优先平仓多头持仓
            amount = ClosePoistionWithAmount(amount, PositionType.Long);
           if(amount > 0) PositionManager.Instance.CreatePosition(instId, PositionType.Short, amount, MarginMode);
        }

        /// <summary>
        /// 根据ID获取持仓
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Position GetPosition(long id)
        {
            return PositionManager.Instance.GetPosition(id);
        }

        public IList<Position> GetPositions()
        {
            return PositionManager.Instance.GetPositions(instId);
        }

        /// <summary>
        /// 获取某一反向上的持仓
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Position GetPosition(PositionType type)
        {
            return PositionManager.Instance.GetPosition(instId, type);
        }

        public void CancelOrder(long id)
        {
            TradeOrderManager.Instance.CancelOrder(this.instId, id);
        }

        public void CancelOrderBySide(OrderSide side, bool async)
        {
            var ids = new List<long>();

            switch (side)
            {
                case OrderSide.Buy:
                    EachBuyOrder((order) =>
                    {
                        ids.Add(order.PublicId);
                    });
                    break;
                case OrderSide.Sell:
                    EachSellOrder((order) =>
                    {
                        ids.Add(order.PublicId);
                    });
                    break;
            }

            if (async)
            {
                TradeOrderManager.Instance.BatchCancelOrderAsync(this.instId, ids);
            }
            else
            {
                TradeOrderManager.Instance.BatchCancelOrder(this.instId, ids);

            }
        }

        public void CancelOrders(IEnumerable<long> ids)
        {
            if (ids == null || ids.Count() == 0) return;
            TradeOrderManager.Instance.BatchCancelOrder(this.instId, ids);
        }

        public void EachBuyOrder(Action<OrderBase> orderCallback)
        {
            TradeOrderManager.Instance.EachBuyOrder((order) =>
            {
                if (string.Compare(this.instId, order.InstId, true) == 0)
                {
                    orderCallback(order);
                }
            });
        }

        public void EachSellOrder(Action<OrderBase> orderCallback)
        {
            TradeOrderManager.Instance.EachSellOrder((order) =>
            {
                if (string.Compare(this.instId, order.InstId, true) == 0)
                {
                    orderCallback(order);
                }
            });
        }

        public OrderBase GetOrder(long id)
        {
            return TradeOrderManager.Instance.GetOrder(id, instId);
        }
        public bool ModifyOrder(long id, decimal amount, decimal newPrice, bool cancelOrderWhenFailed)
        {
            return TradeOrderManager.Instance.ModifyOrder(id, instId, amount, newPrice, cancelOrderWhenFailed);
        }

        public ICandleProvider GetCandleProvider(CandleGranularity granularity)
        {
            return dataProvider.GetCandleProvider(granularity);
        }
        public void UnloadCandle(CandleGranularity granularity)
        {
            this.dataProvider.UnloadCandle(granularity);
        }
        public void LoadCandle(CandleGranularity granularity)
        {
            this.dataProvider.LoadCandle(granularity);
        }
        public void EachCandle(CandleGranularity granularity, Func<Candle, bool> callback)
        {
            var candleProvider = GetCandleProvider(granularity);
            if (candleProvider != null)
            {
                candleProvider.EachCandle(callback);
            }
        }
        public long SendOrder(OrderSide side, decimal amount, decimal price, bool postOnly)
        {
            PositionType posType = PositionType.Long;

            switch (side)
            {
                case OrderSide.Buy:
                    posType = PositionType.Long;
                    break;
                case OrderSide.Sell:
                    posType = PositionType.Short;
                    break;
            }

            return PositionManager.Instance.CreatePosition(instId, posType, amount, MarginMode, price);
        }

        public void UpdatePrices(decimal ask, decimal bid,DateTime time)
        {

        }

        public void Dispose()
        {
            if (dataProvider != null)
            {
                dataProvider.OnTick -= DataProvider_OnTick;
                DataProviderManager.Instance.ReleaseProvider(dataProvider);
                dataProvider = null;
            }
        }
    }
}
