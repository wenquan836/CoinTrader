using CoinTrader.Common;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.Common.Util;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.REST;
using CoinTrader.OKXCore.VO;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CoinTrader.Strategies.Runtime
{
    /// <summary>
    /// 现货策略运行时。
    /// </summary>
    public class SpotStrategyRuntime : ITradeStrategyRuntime
    {
        string instId;
        MarketDataProvider dataProvider = null;
        InstrumentBase instrument = null;

        Wallet baseWallet;
        Wallet quoteWallet;

        public string InstId => instId;

        public bool Init(string instId)
        {
            this.instId = instId;
            dataProvider = DataProviderManager.Instance.GetProvider(instId);

            if (dataProvider == null) return false;
            instrument = InstrumentManager.GetInstrument(instId);
            baseWallet = new Wallet(instrument.BaseCcy);
            quoteWallet = new Wallet(instrument.QuoteCcy);

            dataProvider.OnTick += DataProvider_OnTick;

            return true;
        }

        private void DataProvider_OnTick(decimal ask, decimal bid)
        {
            this.OnTick?.Invoke(ask, bid);
            UpdatePrices(ask,bid,DateUtil.GetServerDateTime());
        }

        public uint Lever { get; set; } = 1;

        public BalanceVO QuoteBalance
        {
            get
            {
                return AssetsManager.Instance.GetBalance(BalanceType.Trading, QuoteCurrency);
            }
        }

        public BalanceVO BaseBalance
        {
            get
            {
                return AssetsManager.Instance.GetBalance(BalanceType.Trading, BaseCurrency);
            }
        }

        public bool IsEmulator => false;

        public string QuoteCurrency => instrument.QuoteCcy;

        public string BaseCurrency => instrument.BaseCcy;

        public decimal MinSize => instrument.MinSize;

        public decimal TickSize => instrument.TickSize;

        public bool Effective => (dataProvider.Effective && MonitorSchedule.Default.AllIsEffective());

 
        public event Action<decimal, decimal> OnTick;

        public List<MonitorBase> GetAllMonitor()
        {
            return dataProvider.GetAllMonitor();
        }

        public void Buy(decimal amount)
        {
            CreateOrder api;
            amount = amount * dataProvider.Ask;//转为USDT数量

            if (QuoteBalance.Avalible >= amount)
            {
                api = new SpotBuyImmediately(instId, amount);
                var result = api.ExecSync();

                if (result.code != 0)
                {
                    Logger.Instance.LogError("币币下单失败 " + result.message.ToString());
                }
                else
                {
                    //成功
                }
            }
            else
            {
                Logger.Instance.LogInfo(instId + "币币买入失败,余额不足 " + amount + "/ " + QuoteBalance.Avalible);
            }
        }

        public void Sell(decimal amount)
        {
            amount = Math.Min(amount, baseWallet.AvailableInTrading);


            if (amount > this.MinSize)
            {
                CreateOrder api = new SpotSellImmediately(instId, amount);
                //api = new SpotCreateOrder(instId, OrderSide.Sell);
                //api.sz = amount.ToString();
                //api.px = this.Bid.ToString();
                var result = api.ExecSync();
            }
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
            var orderManager = TradeOrderManager.Instance;
            var order = orderManager.PlaceOrder(instId, amount, price, side, postOnly, true);

            if (order != null)
            {
                return order.PublicId;
            }

            return 0;
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
