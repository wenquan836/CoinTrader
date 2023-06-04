using CoinTrader.OKXCore.VO;
using CoinTrader.Common;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using System;
using System.Collections.Generic;
using CoinTrader.OKXCore.Event;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.Entity;
using CoinTrader.Common.Util;

namespace  CoinTrader.OKXCore
{
    /// <summary>
    /// 市场数据聚合
    /// 用于获取报价、深度表、K线的数据驱动
    /// </summary>
    public class MarketDataProvider: IDepthProvider, IDisposable
    {
        protected TickerMonitor ctcTickerMonitor = null;
        protected List<CandleMonitor> candleMonitorList = new List<CandleMonitor>();
        protected InstrumentBase instrument;

        /// <summary>
        /// 最后访问深度表的时间如果长时间没有访问则关闭， 深度表比较消耗性能。
        /// </summary>
        DateTime lastAccessDethBook = DateTime.Now;

        /// <summary>
        /// 平台发送报价的时候
        /// </summary>
        public event Action<decimal, decimal> OnTick;

        /// <summary>
        /// 如果10秒都没有访问深度表，则自动关闭
        /// </summary>
        private static readonly int depthBookCloseTime = 100000;

        /// <summary>
        /// 默认不开启深度表
        /// </summary>
        private bool isSubscribeDepth = false;

        /// <summary>
        /// 如果10秒没有访问自动关闭深度表
        /// </summary>
        public bool AutoCloseDepthBook
        {
            get;set;
        }
        
        /// <summary>
        /// 是否已经订阅了深度表，默认不订阅，应为需要消耗性能
        /// </summary>
        public bool IsSubscribeDepth => isSubscribeDepth; 

        /// <summary>
        /// 主币种
        /// </summary>
        public string BaseCurrency
        {
            get
            {
                return instrument.BaseCcy;
            }
        }

        /// <summary>
        /// 计价币种
        /// </summary>
        public string QuoteCurrency
        {
            get
            {
                return instrument.QuoteCcy;
            }
        }
        

        public string InstrumentId
        {
            get; private set;
        }

        /// <summary>
        /// 最后收到的卖盘报价
        /// </summary>
        public decimal Ask
        {
            get; private set;
        }

        /// <summary>
        /// 最后收到的买盘报价
        /// </summary>
        public decimal Bid
        {
            get; private set;
        }

        private MonitorSchedule monitorManager = null;
        public MarketDataProvider(string instrumentId)
        {
            instrument = InstrumentManager.GetInstrument(instrumentId);
            this.AutoCloseDepthBook = true;
            this.InstrumentId = instrumentId;
            monitorManager = new MonitorSchedule();

            ctcTickerMonitor = new TickerMonitor(instrumentId);
            ctcTickerMonitor.OnData += CtcTickerMonitor_OnData;
            monitorManager.AddMonotor(ctcTickerMonitor);
        }

        /// <summary>
        /// 添加监视器
        /// </summary>
        /// <param name="monitor"></param>
        public void AddMonitor(MonitorBase monitor)
        {
            this.monitorManager.AddMonotor(monitor);
        }

        public ICandleProvider GetCandleProvider(CandleGranularity granularity)
        {
            lock (this.candleMonitorList)
            {
                foreach (var s in this.candleMonitorList)
                {
                    if (s.Granularity == granularity)
                    {
                        return s;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 根据时间粒度加载蜡烛图
        /// </summary>
        /// <param name="granularity"></param>
        public CandleMonitor LoadCandle(CandleGranularity granularity)
        {
            var monitor = this.GetCandleProvider(granularity) as CandleMonitor;

            if (monitor == null)
            {
                lock (this.candleMonitorList)
                {
                    monitor = new CandleMonitor(this.InstrumentId, granularity);
                    this.monitorManager.AddMonotor(monitor);
                    this.candleMonitorList.Add(monitor);
                }
            }

            return monitor;
        }

        /// <summary>
        /// 卸载蜡烛图，避免浪费资源
        /// </summary>
        /// <param name="granularity"></param>
        public void UnloadCandle(CandleGranularity granularity)
        {
            var monitor = this.GetCandleProvider(granularity) as CandleMonitor;

            if (monitor != null)
            {
                lock (this.candleMonitorList)
                {
                    this.monitorManager.RemoveMonitor(monitor);
                    this.candleMonitorList.Remove(monitor);
                }
            }
        }

        private void CtcTickerMonitor_OnData(MonitorBase obj)
        {
            this.Ask = ctcTickerMonitor.Ask;
            this.Bid = ctcTickerMonitor.Bid;
            UpdatePrices();
        }

        private void UpdatePrices()
        {
            lock (this.candleMonitorList)
            {
                foreach (var m in this.candleMonitorList)
                {
                    m.UpdateLastPrice((this.Ask + this.Bid) * 0.5m, DateUtil.GetServerUTCDateTime());
                }
            }

            OnTick?.Invoke(Ask, Bid);

            //判断是否深度表处于空闲状态， 如果是则关闭深度表
            if (this.AutoCloseDepthBook && this.isSubscribeDepth)
            {
                if ((DateTime.Now - lastAccessDethBook).TotalMilliseconds > depthBookCloseTime)
                {
                    this.ctcTickerMonitor.UnsubscribeDepth();
                    this.isSubscribeDepth = false;
                }
            }
        }

        public List<MonitorBase> GetAllMonitor()
        {
            List<MonitorBase> list = new List<MonitorBase>();
            list.AddRange(this.monitorManager.GetAllMonitor());

            return list;
        }

        /// <summary>
        /// 订阅深度表
        /// </summary>
        public void SubscribeDepth()
        {
            if (!isSubscribeDepth)
            {
                this.ctcTickerMonitor.SubscribeDepth();
                this.isSubscribeDepth = true;
            }
        }

        /// <summary>
        /// 遍历深度表
        /// </summary>
        /// <param name="side">买入或卖出方向</param>
        /// <param name="callback"></param>
        public virtual void EachDepthBook(DepthBookList side, Action<DepthInfo> callback)
        {
            lastAccessDethBook = DateTime.Now;
            
            if (!isSubscribeDepth)
            {
                SubscribeDepth();
                return;
            }

            this.ctcTickerMonitor.EachDepthBook(side, callback);
        }
        public bool Effective
        {
            get
            {
                return this.monitorManager.AllIsEffective();
            }
        }
        public void Dispose()
        {
            monitorManager.Dispose();
        }
    }
}
