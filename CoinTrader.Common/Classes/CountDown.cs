using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Classes
{
    /// <summary>
    /// 用于重复时间间隔检查
    /// </summary>
    public class CountDown
    {
        public CountDown(uint interval)
            :this(interval, true)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval">两次检查的毫秒数间隔</param>
        /// <param name="firstTimeIsAvalible">第一次是否有效， 如果为false，首次检查将无效</param>
        public CountDown(uint interval, bool firstTimeIsAvalible)
        {
            this.Interval = interval;
            LastTime = firstTimeIsAvalible ? DateTime.MinValue : DateTime.Now;
        }

        /// <summary>
        /// CD间隔
        /// </summary>
        public uint Interval
        {
            get; set;
        }

        /// <summary>
        /// 最后一次时间
        /// </summary>
        public DateTime LastTime { get; private set; }

        public bool Check()
        {
            var now = DateTime.Now;
            var ts = now - this.LastTime;

            if(ts.TotalMilliseconds > this.Interval)
            {
                this.LastTime = now;
                return true;
            }

            return false;
        }

        public void Skip(uint times)
        {
            this.LastTime = DateTime.Now.AddMilliseconds(this.Interval * times);
        }

        public void SkipOnce()
        {
            this.LastTime = DateTime.Now.AddMilliseconds(this.Interval);
        }
    }
}
