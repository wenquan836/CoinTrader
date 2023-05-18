
using CoinTrader.OKXCore.Interface;
using System;

namespace CoinTrader.OKXCore.Monitor
{
    public abstract  class MonitorBase : IMonitor,IDisposable
    {
        public DateTime LastUpdate
        {
            get;private set;
        }
        public MonitorBase()
        {
            this.Effective = false;
            this.CustomName = string.Empty;
        }

        public virtual  string CustomName
        {
            get;
            protected set;
        }

        /// <summary>
        /// API调用间隔（毫秒）
        /// </summary>
        public uint Interval { get; set; } = 2000;

        protected int cd = 0;
        private int feedCd = 0;
        
        protected bool isDisposed = false;

        /// <summary>
        /// 监视器是否在正常工作
        /// </summary>
        public virtual bool Effective
        {
            get;protected set;
        }

        public event Action<MonitorBase> OnData = null;
        public event Action<int, string> OnError = null;

        /// <summary>
        /// 如果数据获取成功需要调用这个函数以确认API没用出现错误或超时
        /// </summary>
        protected void Feed()
        {
            if (isDisposed)
                return;

            LastUpdate = DateTime.Now;
            feedCd = (int)Interval + 1000;
            this.Effective = true;
            this.InvokeOnData();
        }
        protected void InvokeOnError(int code,string msg)
        {
            this.OnError?.Invoke(code,msg);
        }

        protected void InvokeOnData()
        {
            this.OnData?.Invoke(this);
        }

        protected abstract void RunInvoke();

        /// <summary>
        /// 增加随机延时
        /// </summary>
        /// <param name="max"></param>
        public void AddCD(int cd)
        {
            this.cd += cd;
        }

        /// <summary>
        /// 增加随机延时
        /// </summary>
        /// <param name="max"></param>
        public void AddRamdonCD(int max)
        {
            Random random = new Random();
            AddCD(random.Next(max));
        }

        /// <summary>
        /// 强制一次刷新， 不考虑时间间隔
        /// </summary>
        public void ForceUpdate()
        {
            this.cd = 0;
            this.Update(0);
        }
        public virtual void Update(int dt)
        {
            if(isDisposed)
                return;

            cd -= dt;
            if (cd <= 0)
            {
                cd = (int)this.Interval;
                RunInvoke();
            }

            if(feedCd <= 0)
            {
                this.Effective = false;
            }
            else
            {
                feedCd -= dt;
            }
        }

        public virtual void Dispose()
        {
            this.Effective = false;
            isDisposed = true;
        }
    }
}
