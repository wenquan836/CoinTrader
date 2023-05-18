
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinTrader.Common;
using CoinTrader.Common.Interface;
using CoinTrader.Common;

using CoinTrader.Common.Interface;
using CoinTrader.OKXCore.Event;
using CoinTrader.OKXCore.Monitor;

namespace CoinTrader.OKXCore.Monitor
{
    /// <summary>
    /// 数据监视器调度,进行周期性的从服务器端抓取数据
    /// </summary>
    public class MonitorSchedule:IEventListener,IDisposable
    {
        private readonly List<MonitorBase> allMonitor = new List<MonitorBase>();

        private DateTime lastUpdateTime;
        public MonitorSchedule()
        {
            lastUpdateTime = DateTime.Now;
            EventCenter.Instance.AddListener(this);
        }

        public List<MonitorBase> GetAllMonitor()
        {
            return new List<MonitorBase>(this.allMonitor.ToArray());
        }

        public void AddMonotor(MonitorBase monitor)
        {
            lock (this.allMonitor)
            {
                if (this.allMonitor.Contains(monitor))
                {
                    return;
                }

                allMonitor.Add(monitor);
            }
         }

        private void DestoryAllMonitors()
        {
            lock (this.allMonitor)
            {
                foreach (var m in this.allMonitor)
                {
                    m.Dispose();
                }

                this.allMonitor.Clear();
            }
         }

        public void RemoveMonitor(MonitorBase monitor)
        {
            lock (this.allMonitor)
            {
                this.allMonitor.Remove(monitor);
                monitor.Dispose();
            }
        }
        
        /// <summary>
        /// 所有数据都正常，如果某一项数据获取不到的话， 返回false
        /// </summary>
        /// <returns></returns>
        public bool AllIsEffective()
        {
            lock (this.allMonitor)
            {
                foreach (var m in this.allMonitor)
                {
                    if (!m.Effective)
                        return false;
                }
            }

            return true;
        }

        private void OnSystemTick(object objTick)
        {
            int dt = (int)((DateTime.Now - lastUpdateTime).TotalMilliseconds);
            lastUpdateTime = DateTime.Now;

            lock (allMonitor)
            {
                foreach (var m in allMonitor)
                {
                    m.Update(dt);
                }
            }
        }

        public IEnumerable<EventListenItem> GetEvents()
        {
            return new EventListenItem[]
            {
                new EventListenItem(CoreEvent.SystemTick, OnSystemTick)
            };
        }

        public void Dispose()
        {
            DestoryAllMonitors();
            EventCenter.Instance.RemoveListener(this);
        }

        private static MonitorSchedule _default = new MonitorSchedule();

        /// <summary>
        /// 默认的公共调度器
        /// </summary>
        public static MonitorSchedule Default => _default;
    }
}
