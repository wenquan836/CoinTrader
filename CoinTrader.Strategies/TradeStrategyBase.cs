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
using CoinTrader.Strategies.Runtime;

namespace CoinTrader.Strategies
{
    /// <summary>
    /// 交易策略基类， 现货和合约均继承自这个基类
    /// </summary>
    public abstract class TradeStrategyBase : StrategyBase
    {
         protected InstrumentBase  instrumentBase;
        protected ITradeStrategyRuntime runtime = null;
        public ITradeStrategyRuntime Runtime { get { return runtime; } }


        private volatile bool disposed = false;
        private volatile bool innerExecuting = false;

        protected virtual StrategyType StrategyType => StrategyType.Spot;


        /// <summary>
        /// 最新的盘口卖盘报价
        /// </summary>
        protected decimal Ask { get; set; }

        /// <summary>
        /// 最新的盘口买盘报价
        /// </summary>
        protected decimal Bid { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public override bool Init(string instId)
        {
            runtime = StrategyRuntimeManager.Instance.GetRuntime(instId, IsEmulationMode);
            if (runtime == null) return false;
            instrumentBase = InstrumentManager.GetInstrument(instId);

            if (!base.Init(instId))
            {
                StrategyRuntimeManager.Instance.ReleaseRuntime(runtime);
                runtime = null;
                return false;
            }
            runtime.OnTick += this.OnTickInner;

            return true;
        }

 

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected OrderBase GetOrder(long id)
        {
           return runtime.GetOrder(id);
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
           return runtime.ModifyOrder(id, amount, newPrice, cancelOrderWhenFailed);
        }

        /// <summary>
        /// 挂单
        /// </summary>
        /// <param name="side">卖出还是买入</param>
        /// <param name="amount">数量</param>
        /// <param name="price">价格</param>
        /// <param name="postOnly">是否是限价模式，如何是限价模式则，高宇盘口价买入或低于盘口价卖出则失败</param>
        /// <returns>非0，则返回订单ID， 返回0则失败</returns>
        protected long SendOrder(OrderSide side, decimal amount, decimal price, bool postOnly)
        {
            return runtime.SendOrder(side, amount, price, postOnly);
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
            Ask = ask;
            Bid = bid;
            if (disposed) return;
            if (!this.Enable) return;
            if (innerExecuting) return;

            innerExecuting = true;

            if (runtime.IsEmulator)
            {
                OnTick();
                innerExecuting = false;
                return;
            }

            var task = Task.Factory.StartNew(() =>
            {
                OnTick();
            }
            , CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)//策略执行出现异常
                {
                    if (t.Exception != null)
                    {
                        Logger.Instance.LogException(t.Exception);
                    }
                }

                if (disposed && runtime != null)
                {
                    StrategyRuntimeManager.Instance.ReleaseRuntime(runtime);
                    runtime = null;
                }

                innerExecuting = false;
            });
        }

        protected override void Wait(int milliseconds)
        {
            if (runtime.IsEmulator) return;

            base.Wait(milliseconds);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void Dispose()
        {         
            disposed = true;
            if(runtime != null)
            {
                runtime.OnTick -= this.OnTickInner;
       
                if (!innerExecuting)
                {
                    StrategyRuntimeManager.Instance.ReleaseRuntime(runtime);
                    runtime = null;
                }
            }

            base.Dispose();
        }

        /// <summary>
        /// 数据是否正常
        /// </summary>
        protected bool Effective=>runtime.Effective;

        /// <summary>
        /// 收到报价时调用， 主要交易逻辑均有这个函数驱动
        /// </summary>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        protected abstract void OnTick();



        /// <summary>
        /// 可用的计价货币数量
        /// </summary>
        protected decimal QuoteAvailable=>runtime.QuoteBalance.Avalible;

        /// <summary>
        /// 买入,以盘口价直接买入
        /// </summary>
        /// <param name="amount">数量</param>
        protected  void Buy(decimal amount)
        {
            runtime.Buy(amount);
        }

        /// <summary>
        /// 卖出,直接吃单卖出
        /// </summary>
        /// <param name="amount">数量</param>
        protected  void Sell(decimal amount)
        {
            runtime.Sell(amount);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id"></param>
        protected void CancelOrder(long id)
        {
            runtime.CancelOrder(id);
        }

        /// <summary>
        /// 批量撤销订单
        /// </summary>
        /// <param name="ids"></param>
        protected void CancelOrders(IEnumerable<long> ids)
        {
            runtime.CancelOrders(ids);
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
            runtime.CancelOrderBySide(side, async);
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
           runtime.EachSellOrder(orderCallback);
        }

        /// <summary>
        /// 遍历买单
        /// </summary>
        /// <param name="orderCallback"></param>
        protected void EachBuyOrder(Action<OrderBase> orderCallback)
        {
            runtime.EachBuyOrder(orderCallback);
        }

        /// <summary>
        /// 加载蜡烛图
        /// </summary>
        /// <param name="granularity">粒度</param>
        protected void LoadCandle(CandleGranularity granularity)
        {
            runtime?.LoadCandle(granularity);
        }

        /// <summary>
        /// 卸载蜡烛图
        /// </summary>
        /// <param name="granularity">粒度</param>
        protected void UnloadCandle(CandleGranularity granularity)
        {
            runtime?.UnloadCandle(granularity);
        }

        /// <summary>
        /// 循环遍历每一根K线，使用之前需要LoadCandle先加载进来
        /// </summary>
        /// <param name="granularity">粒度</param>
        /// <param name="callback">回调， 如果返回true则结束</param>
        /// 
        protected void EachCandle(CandleGranularity granularity, Func<Candle, bool> callback)
        {
            runtime.EachCandle(granularity, callback);
        }

        /// <summary>
        /// 获取蜡烛图接口，获取之前需要先LoadCandle，否则获取不到
        /// </summary>
        /// <param name="granularity">粒度</param>
        /// <returns></returns>
        protected ICandleProvider GetCandleProvider(CandleGranularity granularity)
        {
            var cm = runtime.GetCandleProvider(granularity);
            if (cm == null)
                runtime.LoadCandle(granularity);
            return runtime.GetCandleProvider(granularity);
        }
    }
}
