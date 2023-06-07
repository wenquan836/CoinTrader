using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.REST;
using CoinTrader.OKXCore.VO;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace CoinTrader.Strategies.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public class EmulatorCandleProvider : ICandleProvider
    {
        public EmulatorCandleProvider(CandleGranularity candleGranularity)
        {
            this.granularity = candleGranularity;
        }
        public bool Loaded => true;

        public event EventHandler CandleLoaded;

        List<Candle> candles = new List<Candle>();

        private CandleGranularity granularity = CandleGranularity.Y1;

        public void EachCandle(Func<Candle, bool> callback)
        {
            lock (this.candles)
            {
                foreach (var candle in candles)
                {
                    if (callback(candle))
                        break;
                }
            }
        }

        public void UpdateLastPrice(decimal price, DateTime time)
        {
            lock (this.candles)
            {
                var last = this.candles.Count > 0 ? this.candles[0] : null;
                uint interval = (uint)this.granularity;
                if (last != null)
                {
                    TimeSpan ts = time - last.Time;

                    if (ts.TotalSeconds > interval)
                    {
                        Candle candle = VOPool<Candle>.GetPool().Get();
                        candle.Open = candle.High = candle.Low = candle.Close = price;

                        int y, M, d, h, m;

                        y = time.Year;
                        M = time.Month;
                        d = time.Day;
                        h = time.Hour;
                        m = time.Minute;

                        switch (this.granularity)
                        {
                            case CandleGranularity.M1:
                                break;
                            case CandleGranularity.M3:
                                m -= m % 3;
                                break;
                            case CandleGranularity.M5:
                                m -= m % 5;
                                break;
                            case CandleGranularity.M15:
                                m -= m % 15;
                                break;
                            case CandleGranularity.M30:
                                m -= m % 30;
                                break;
                            case CandleGranularity.H1:
                                m = 0;
                                break;
                            case CandleGranularity.H4:
                                m = 0;
                                h -= h % 4;
                                break;
                            case CandleGranularity.H6:
                                m = 0;
                                h -= h % 6;
                                break;
                            case CandleGranularity.H12:
                                m = 0;
                                h -= h % 12;
                                break;
                            case CandleGranularity.D1:
                                h = 0;
                                m = 0;
                                break;
                            case CandleGranularity.Week1:
                                DateTime newTime = time;
                                newTime = newTime.AddDays(-(int)newTime.DayOfWeek);
                                y = newTime.Year;
                                M = newTime.Month;
                                d = newTime.Day;
                                h = 0;
                                m = 0;
                                break;
                            case CandleGranularity.Month1:
                                d = 1; h = 0; m = 0;
                                break;
                            case CandleGranularity.Y1:
                                M = 0; d = 1; h = 0; m = 0;
                                break;
                        }

                        candle.Time = new DateTime(y, M, d, h, m, 0);
                        candle.Confirm = false;
                        candle.Volume = 0;
                        this.candles.Insert(0, candle);
                    }
                    else
                    {
                        last.Close = price;
                        last.High = Math.Max(price, last.High);
                        last.Low = Math.Min(price, last.Low);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 现货复盘模拟器
    /// </summary>
    public class StrategyEmulatorRuntime : ITradeStrategyRuntime
    {
        /// <summary>
        /// 佣金设置
        /// </summary>
        public decimal Fee { get; set; } = 0;
        public event Action<decimal, decimal> OnTick;
        public string InstId => instId;
        public List<long> idForRemove = new List<long>();
        public List<TradeOrder> orderForAdd = new List<TradeOrder>();


        private BalanceVO baseCcyBalance;
        private BalanceVO quoteCcyBalance;
        private string instId;
        private long orderIdSeed = 10000;
        private bool traversingOrders = false;
        private decimal ask;
        private decimal bid;
        private DateTime now;



        /// <summary>
        /// 基础币种
        /// </summary> 
        public string BaseCurrency => instrument.BaseCcy;

        /// <summary>
        /// 计价币种
        /// </summary>
        public string QuoteCurrency => instrument.QuoteCcy;

        public  BalanceVO BaseBalance => baseCcyBalance;

        public BalanceVO QuoteBalance
        {
            get
            {
                return quoteCcyBalance;
            }

            set
            {
                quoteCcyBalance = value;
            }
        }

        /// <summary>
        /// 最小交易数量
        /// </summary>
        public decimal MinSize => instrument.MinSize;

        public decimal TickSize => instrument.TickSize;

        public bool IsEmulator => true;

        
        public bool Effective => true;

        public uint Lever { get; set; } = 1;

        List<TradeOrder> orders = new List<TradeOrder>();

        List<TradeOrder> historyOrders = new List<TradeOrder>();

        InstrumentBase instrument = null;

        Dictionary<CandleGranularity, EmulatorCandleProvider> candles = new Dictionary<CandleGranularity, EmulatorCandleProvider>();

        public StrategyEmulatorRuntime()
        {

        }

        public void StartEmulation(string instId, CandleGranularity candleGranularity, IEnumerable<Type> strategyTypes)
        {
            foreach (var type in strategyTypes)
            {
                var strategy = Activator.CreateInstance(type) as TradeStrategyBase;

                if (strategy == null)
                {
                    strategy.Init(instId);
                }
            }
        }

        public bool Init(string instId)
        {
            instrument = InstrumentManager.GetInstrument(instId);
            this.instId = instId;
            if (instrument == null) return false;
            return true;
        }

        public void LoadCandle(CandleGranularity granularity)
        {

        }

        public void EachCandle(CandleGranularity granularity, Func<Candle, bool> callback)
        {
            EmulatorCandleProvider provider;
            if (candles.TryGetValue(granularity, out provider))
            {
                provider.EachCandle(callback);
            }
        }

        public ICandleProvider GetCandleProvider(CandleGranularity granularity)
        {
            EmulatorCandleProvider provider;
            if (!this.candles.TryGetValue(granularity, out provider))
            {
                provider = new EmulatorCandleProvider(granularity);
                this.candles[granularity] = provider;
            }

            return provider;
        }
        public void UnloadCandle(CandleGranularity granularity)
        {
            this.candles.Remove(granularity);
        }

        public OrderBase GetOrder(long id)
        {
            TradeOrder order = null;
            foreach (var o in orders)
            {
                if (o.PublicId == id)
                {
                    order = new TradeOrder();
                    order.CopyFrom(o);
                    break;
                }
            }

            return order;
        }

        private void EachOrders(OrderSide side, Action<OrderBase> callback)
        {
            traversingOrders = true;
            for (int i = orders.Count - 1; i >= 0; i--)
            {
                if (orders[i].Side == side)
                {
                    callback?.Invoke(orders[i]);
                }
            }
            traversingOrders = false;

            for(int i = orderForAdd.Count - 1; i >= 0; i--)
            {
                orders.Add(orderForAdd[i]);
            }

            orderForAdd.Clear();
            this.CancelOrders(idForRemove);
            idForRemove.Clear();
        }

        public void EachSellOrder(Action<OrderBase> orderCallback)
        {
            EachOrders(OrderSide.Sell, orderCallback);
        }

        public void EachBuyOrder(Action<OrderBase> orderCallback)
        {
            EachOrders(OrderSide.Buy, orderCallback);
        }

        public void CancelOrderBySide(OrderSide side, bool async)
        {
            for (int i = orders.Count - 1; i >= 0; i--)
            {
                if (orders[i].Side == side)
                {
                    InnerRemoveOrder(orders[i]);
                    orders.RemoveAt(i);
                }
            }
        }

        public void CancelOrders(IEnumerable<long> ids)
        {
            if (ids == null || ids.Count() == 0) return;

            if(traversingOrders)
            {
                idForRemove.AddRange(ids);
                return;
            }

            for (int i = orders.Count - 1; i >= 0; i--)
            {
                if (ids.Contains(orders[i].PublicId))
                {
                    InnerRemoveOrder(orders[i]);
                    orders.RemoveAt(i);
                }
            }
        }

        public List<TradeOrder> GetHistoryList()
        {
            return historyOrders;
        }

        private void InnerRemoveOrder(long id)
        {
            TradeOrder order = GetOrder(id) as TradeOrder;
 
            if (order != null)
            {
                InnerRemoveOrder(order);
            }
        }

        private void InnerRemoveOrder(OrderBase order)
        {
            if (order == null) return;

            if(order.AvailableAmount < order.Amount)
            {
                var newOrder = new TradeOrder();
                newOrder.CopyFrom(order);
                newOrder.FilledSize = order.Amount - order.AvailableAmount;
                historyOrders.Insert(0,newOrder);
            }

            var baseBalance = BaseBalance;
            var quoteBalance = QuoteBalance;

            switch (order.Side)
            {
                case OrderSide.Sell:
                    baseBalance.Avalible += order.AvailableAmount;
                    baseBalance.Frozen -= order.AvailableAmount;
                    break;
                case OrderSide.Buy:
                    quoteBalance.Avalible += order.AvailableAmount * order.Price;
                    quoteBalance.Frozen -= order.AvailableAmount * order.Price;
                    break;
            }

            Debug.Assert(baseBalance.Avalible >=0  && baseBalance.Frozen >=0);
            Debug.Assert(quoteBalance.Avalible >= 0 && quoteBalance.Frozen >= 0);

            baseCcyBalance = baseBalance;
            quoteCcyBalance = quoteBalance;
        }
        public TradeOrder InnerCreateOrder(OrderSide side, decimal amount, decimal price, bool postOnly)
        {
            if (postOnly)
            {
                if (side == OrderSide.Sell && bid > price)
                {
                    return null;
                }

                else if (side == OrderSide.Buy && ask < price)
                {
                    return null;
                }
            }

            switch (side)
            {
                case OrderSide.Sell:
                    BalanceVO b = baseCcyBalance;
                    var frozen = Math.Min(amount, b.Avalible);
                    b.Avalible -= frozen;
                    b.Frozen += frozen;
                    baseCcyBalance = b;
                    break;
                case OrderSide.Buy:
                    var quoteBalance = quoteCcyBalance;
                    var needCash = Math.Min(quoteBalance.Avalible, price * amount);
                    amount = Math.Min(amount, needCash / price);
                    quoteBalance.Avalible -= needCash;
                    quoteBalance.Frozen += needCash;
                    quoteCcyBalance = quoteBalance;
                    break;
            }
            Debug.Assert(baseCcyBalance.Avalible >= 0 && baseCcyBalance.Frozen >= 0);
            Debug.Assert(quoteCcyBalance.Avalible >= 0 && quoteCcyBalance.Frozen >= 0);
            orderIdSeed++;
            TradeOrder order = new TradeOrder
            {
                Side = side,
                Amount = amount,
                Price = price,
                InstId = instrument.InstrumentId,
                PublicId = orderIdSeed,
                AvailableAmount = amount,
                CreatedDate = now
            };

            return order;
        }

        public bool ModifyOrder(long id, decimal amount, decimal newPrice, bool cancelOrderWhenFailed)
        {
            TradeOrder order = GetOrder(id) as TradeOrder;

            if (order == null)
                return false;

            var baseBalance = BaseBalance;
            var quoteBalance = QuoteBalance;

            if (amount <= 0)
            {
                if (cancelOrderWhenFailed)
                    CancelOrder(id);
                return false;
            }


            switch (order.Side)
            {
                case OrderSide.Sell:
                    if (baseBalance.Total < amount)//可出售的数量不足
                    {
                        if (cancelOrderWhenFailed)
                            CancelOrder(order.PublicId);
                        return false;
                    }

                    if (newPrice <= bid) //价格无效
                    {
                        if (cancelOrderWhenFailed)
                            CancelOrder(order.PublicId);
                        return false;
                    }

                    break;
                case OrderSide.Buy:
                    if (quoteBalance.Avalible + order.AvailableAmount * order.Price < amount * newPrice)//可用稳定币的数量不足
                    {
                        if (cancelOrderWhenFailed)
                            CancelOrder(order.PublicId);
                        return false;
                    }

                    if (newPrice >= ask) //价格无效
                    {
                        if (cancelOrderWhenFailed)
                            CancelOrder(order.PublicId);
                        return false;
                    }
                    break;
            }

 
            CancelOrder(order.PublicId);

            TradeOrder newOrder = InnerCreateOrder(order.Side, amount, newPrice, false);
            newOrder.PublicId = order.PublicId;
            newOrder.CreatedDate = order.CreatedDate;

            if (traversingOrders)
            {
                orderForAdd.Add(newOrder);
            }
            else
            {
                orders.Add(newOrder);
            }

            return true;
        }
        
        public long SendOrder(OrderSide side, decimal amount, decimal price, bool postOnly)
        {
            if (amount < instrument.MinSize)
                return 0;
            var order = InnerCreateOrder(side, amount, price, postOnly);

            if(order!= null)
            {
                if (traversingOrders)
                {
                    orderForAdd.Add(order);
                }
                else
                {
                    orders.Add(order);
                }

                return order.PublicId;
            }

            return 0;
        }


        public void Sell(decimal amount)
        {
            SendOrder(OrderSide.Sell, amount, 0, false);
            CheckOrders();
        }
        public void Buy(decimal amount)
        {
            SendOrder(OrderSide.Buy, amount, decimal.MaxValue, false);
            CheckOrders();
        }
        public void CancelOrder(long id)
        {
           CancelOrders(new[] { id });
        }

        private void CheckOrders()
        {
            for (int i = orders.Count - 1; i >= 0; i--)
            {
                var order = orders[i];

                if (order.Side == OrderSide.Buy && ask <= order.Price)
                {
                    var price = Math.Min(order.Price, ask); 
                    var quoteBalance = quoteCcyBalance;
                    var frozenCash = quoteBalance.Frozen;
                    var amount = Math.Min(frozenCash / price, order.AvailableAmount);
                    var fee = Fee * amount;
                    BalanceVO baseBalance = baseCcyBalance;
                    baseBalance.Avalible += amount - fee;
                    baseCcyBalance = baseBalance;
                    quoteBalance.Frozen -= amount * price;
                    quoteCcyBalance = quoteBalance;
                    order.PriceAvg = order.PriceAvg * (order.Amount - order.AvailableAmount) + (amount * ask) / (order.Amount - order.AvailableAmount + amount);
                    order.AvailableAmount -= amount;
                    order.Fee += fee;
                    order.UpdateTime = now;
                    
                    if (order.AvailableAmount <= 0)
                    {
                        order.FeeCurrency = instrument.BaseCcy;
                        historyOrders.Insert(0,order);
                        orders.RemoveAt(i);
                    }
                }
                else if (order.Side == OrderSide.Sell && bid >= order.Price)
                {
                    var price = Math.Max(order.Price, bid);
                    var amount = order.AvailableAmount;
                    var cash = price * amount;
                    var fee = cash * Fee;
                    cash -= fee;
                    BalanceVO quoteBalance = quoteCcyBalance;
                    BalanceVO baseBalance = baseCcyBalance;
                    baseBalance.Frozen -= amount;
                    baseCcyBalance = baseBalance;

                    quoteBalance.Avalible += cash;
                    quoteCcyBalance = quoteBalance;

                    order.PriceAvg = order.PriceAvg * (order.Amount - order.AvailableAmount) + (amount * ask) / (order.Amount - order.AvailableAmount + amount);
                    order.AvailableAmount -= amount;
                    order.Fee = +fee;
                    order.UpdateTime = now;
 
                    if (order.AvailableAmount <= 0)
                    {
                        order.FeeCurrency = instrument.QuoteCcy;
                        historyOrders.Insert(0, order);
                        orders.RemoveAt(i);
                    }
                 }
            }

            Debug.Assert(baseCcyBalance.Avalible >= 0 && baseCcyBalance.Frozen >= 0);
            Debug.Assert(quoteCcyBalance.Avalible >= 0 && quoteCcyBalance.Frozen >= 0);
        }
        public void UpdatePrices(decimal ask, decimal bid,DateTime time)
        {
            this.ask = ask;
            this.bid = bid;
            this.now = time;
            
            foreach(var cp in this.candles)
            {
                cp.Value.UpdateLastPrice(0.5m * (ask + bid),time);
            }

            CheckOrders();

            OnTick?.Invoke(ask, bid);
        }

        public void Dispose()
        {

        }
    }
}
