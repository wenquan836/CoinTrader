using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CoinTrader.Common;
using System.Threading;

namespace CoinTrader.Strategies
{
    /// <summary>
    /// 交易策略基类， 现货和合约均继承自这个基类
    /// </summary>
    public abstract class TradeStrategyBase : StrategyBase
    {
        protected MarketDataProvider dataProvider;
        protected InstrumentBase  instrumentBase;

        private volatile bool disposed = false;
        private volatile bool innerExecuting = false;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init(string instId)
        {
            base.Init(instId);            
            instrumentBase = InstrumentManager.GetInstrument(this.InstId);
            Debug.Assert(instrumentBase != null);
            dataProvider = DataProviderManager.Instance.GetProvider(this.InstId);
            dataProvider.OnTick += this.OnTickInner;
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected OrderBase GetOrder(long id)
        {
           return TradeOrderManager.Instance.GetOrder(id, InstId);
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="amount">数量</param>
        /// <param name="newPrice">价格</param>
        /// <param name="cancelOrderWhenFailed">如果修改失败是否撤单</param>
        protected bool ModifyOrder(long id, decimal amount, decimal newPrice, bool cancelOrderWhenFailed)
        {
            return TradeOrderManager.Instance.ModifyOrder(id, InstId, amount, newPrice, cancelOrderWhenFailed);
        }

        /// <summary>
        /// 由于此处所有的报价都是由websocket数据接收线程驱动，如果不用任务系统调度执行
        /// 如果某个策略执行出现问题或者锁死，将影响整个系统的性能。造成卡死。
        /// 用任务系统隔离每个策略的OnTick，避免相互影响。
        /// 如果逻辑执行时间过长，那么执行期间的报价将被忽略
        /// </summary>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        private void OnTickInner(decimal ask, decimal bid)
        {
            if (disposed) return;
            if (!this.Enable) return;
            if (innerExecuting) return;

            innerExecuting = true;

            var task = Task.Factory.StartNew(() =>
            {
                OnTick();
            }
            , CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    if (t.Exception != null)
                    {
                        Logger.Instance.LogException(t.Exception);
                    }
                }

                if (disposed && dataProvider != null)
                {
                    DataProviderManager.Instance.ReleaseProvider(dataProvider);
                    dataProvider = null;
                }

                innerExecuting = false;
            });
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void Dispose()
        {
            if (dataProvider != null)
            {
                dataProvider.OnTick -= this.OnTickInner;
                disposed = true;
                if (!innerExecuting)
                {
                    DataProviderManager.Instance.ReleaseProvider(dataProvider);
                    dataProvider = null;
                }
            }
            base.Dispose();
        }

        /// <summary>
        /// 数据是否正常
        /// </summary>
        protected bool Effective
        {
            get
            {
                return this.dataProvider.Effective && MonitorSchedule.Default.AllIsEffective();
            }
        }

        /// <summary>
        /// 收到报价时调用， 主要交易逻辑均有这个函数驱动
        /// </summary>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        protected abstract void OnTick();

        /// <summary>
        /// 最新的盘口卖盘报价
        /// </summary>
        protected decimal Ask => dataProvider.Ask;

        /// <summary>
        /// 最新的盘口买盘报价
        /// </summary>
        protected decimal Bid => dataProvider.Bid;

        /// <summary>
        /// 可用的计价货币数量
        /// </summary>
        protected decimal QuoteAvailable
        {
            get
            {
                return AssetsManager.Instance.GetBalance(BalanceType.Trading, BalanceAmountType.Available, instrumentBase.QuoteCcy);
            }
        }

        /// <summary>
        /// 买入,以盘口价直接买入
        /// </summary>
        /// <param name="amount">数量</param>
        protected  abstract void Buy(decimal amount);

        /// <summary>
        /// 卖出,直接吃单卖出
        /// </summary>
        /// <param name="amount">数量</param>
        protected abstract void Sell(decimal amount);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id"></param>
        protected void CancelOrder(long id)
        {
           TradeOrderManager.Instance.CancelOrder(this.InstId, id);
        }


        /// <summary>
        /// 批量撤销订单
        /// </summary>
        /// <param name="ids"></param>
        protected void CancelOrders(IEnumerable<long> ids)
        {
            if (ids == null) return;
            foreach(var id in ids)
            {
                TradeOrderManager.Instance.CancelOrder(this.InstId, id);
            }
        }

        /// <summary>
        /// 撤销全部挂单
        /// </summary>
        public void CancelAllOrders()
        {
            CancelOrderBySide(OrderSide.Buy,false);
            CancelOrderBySide(OrderSide.Sell,false);
        }

        /// <summary>
        /// 撤销全部挂单
        /// </summary>
        public void CancelAllOrdersAsync()
        {
            CancelOrderBySide(OrderSide.Buy,true);
            CancelOrderBySide(OrderSide.Sell,true);
        }


        private void CancelOrderBySide(OrderSide side,bool async)
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
                TradeOrderManager.Instance.BatchCancelOrderAsync(this.InstId, ids);
            }
            else
            {
                TradeOrderManager.Instance.BatchCancelOrder(this.InstId, ids);

            }
        }

        /// <summary>
        /// 撤销所有卖单
        /// </summary>
        public void CancelAllSellOrders()
        {
            CancelOrderBySide(OrderSide.Sell,false);
        }

        /// <summary>
        /// 撤销所有买单
        /// </summary>
        public void CancelAllBuyOrders()
        {
            CancelOrderBySide(OrderSide.Buy,false);
        }

        /// <summary>
        /// 撤销所有卖单
        /// </summary>
        public void CancelAllSellOrdersAsync()
        {
            CancelOrderBySide(OrderSide.Sell,true);
        }

        /// <summary>
        /// 撤销所有买单
        /// </summary>
        public void CancelAllBuyOrdersAsync()
        {
            CancelOrderBySide(OrderSide.Buy, true);
        }

        /// <summary>
        /// 遍历卖单
        /// </summary>
        /// <param name="orderCallback"></param>
        protected void EachSellOrder(Action<OrderBase> orderCallback)
        {
            TradeOrderManager.Instance.EachSellOrder((order) =>
            {
                if (string.Compare(this.InstId, order.InstId, true) == 0)
                {
                    orderCallback(order);
                }
            });
        }

        /// <summary>
        /// 遍历买单
        /// </summary>
        /// <param name="orderCallback"></param>
        protected void EachBuyOrder(Action<OrderBase> orderCallback)
        {
            TradeOrderManager.Instance.EachBuyOrder((order) =>
            {
                if (string.Compare(this.InstId, order.InstId, true) == 0)
                {
                    orderCallback(order);
                }
            });
        }

        /// <summary>
        /// 加载蜡烛图
        /// </summary>
        /// <param name="granularity">粒度</param>
        protected void LoadCandle(CandleGranularity granularity)
        {
            if (this.dataProvider != null)
            {
                this.dataProvider.LoadCandle(granularity);
            }
        }

        /// <summary>
        /// 卸载蜡烛图
        /// </summary>
        /// <param name="granularity">粒度</param>
        protected void UnloadCandle(CandleGranularity granularity)
        {
            if (this.dataProvider != null)
            {
                this.dataProvider.UnloadCandle(granularity);
            }
        }

        /// <summary>
        /// 循环遍历每一根K线，使用之前需要LoadCandle先加载进来
        /// </summary>
        /// <param name="granularity">粒度</param>
        /// <param name="callback">回调， 如果返回true则结束</param>
        /// 
        protected void EachCandle(CandleGranularity granularity, Func<Candle, bool> callback)
        {
            var candleProvider = dataProvider.GetCandleProvider(granularity);
            if (candleProvider != null)
            {
                candleProvider.EachCandle(callback);
            }
        }

        /// <summary>
        /// 获取蜡烛图接口，获取之前需要先LoadCandle，否则获取不到
        /// </summary>
        /// <param name="granularity">粒度</param>
        /// <returns></returns>
        protected ICandleProvider GetCandleProvider(CandleGranularity granularity)
        {
            return dataProvider.GetCandleProvider(granularity);
        }

        /// <summary>
        /// 当前用到的数据监视器列表
        /// </summary>
        public List<MonitorBase> AllMonitor => dataProvider.GetAllMonitor();
    }
}
