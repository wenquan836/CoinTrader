
using CoinTrader.Common;
using System;
using System.Threading;

namespace CoinTrader.Strategies
{
    public abstract class StrategyBase : IDisposable
    {
        /// <summary>
        /// 定时器的间隔
        /// </summary>
        private const int TimerInterval = 1000;
        private volatile bool timerRunning = false;
        private Timer timer = null;

        /// <summary>
        /// 是否复盘模式
        /// </summary>
        public bool IsEmulationMode { get; set; }


        /// <summary>
        /// 是否开启，如果没有开启， 逻辑将不执行
        /// </summary>
        public virtual bool Enable { get; set; }

        /// <summary>
        /// 是否正在执行，用于界面显示，可有可无
        /// </summary>
        public bool Executing { get; protected set; }

        /// <summary>
        /// 用于显示界面消息，可有可无
        /// </summary>
        public string Message { get; protected set; } = string.Empty;
        public string InstId { get; protected set; } = "_strategy_undefine_inst_";

        private void InnerOnTimer(object state)
        {
            if (Enable)
            {
                if (!timerRunning)
                {
                    timerRunning = true;
                    this.OnTimer(TimerInterval);
                    timerRunning = false;
                }
            }
        }

        /// <summary>
        /// 定时器回调， 固定间隔执行
        /// </summary>
        /// <param name="deltaTime">间隔时间</param>
        protected virtual void OnTimer(int deltaTime)
        {

        }

        /// <summary>
        /// 等待指定的毫秒数，通常这里是暂停执行线程，不会影响主线程。
        /// OnTick和OnTimer都是由单独的线程来执行， 调用sleep通常不会影响其他策略的执行
        /// 通常用于等待客户端和服务器断的数据同步
        /// 交易策略在Wait期间，所有的报价将被忽略
        /// 这里不适合做长时间等待，尽量控制5秒钟内的等待。 长时间等待请用CountDown
        /// </summary>
        /// <param name="milliseconds">毫秒数</param>
        protected virtual void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        /// <summary>
        /// 等待指定秒数
        /// </summary>
        /// <param name="seconds"></param>
        protected void Wait(float seconds)
        {
            Thread.Sleep((int)(seconds * 1000.0f));
        }

        #region 配置加载、保存

        /// <summary>
        /// 是否有配置文件了
        /// </summary>
        public bool HasConfig { get; private set; }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns></returns>
        public string SaveConfig()
        {
             string err   = StrategyConfig.SaveStrategyConfig(this.InstId, this);
            this.HasConfig = HasConfig || string.IsNullOrEmpty(err);
            return err;
        }

        /// <summary>
        /// 载入配置文件
        /// </summary>
        public void LoadConfig()
        {
             this.HasConfig = StrategyConfig.LoadStrategyConfig(this.InstId, this);
        }
        #endregion

        public  virtual void OnParamaterChanged()
        {

        }

        #region 写日志

        protected void LogDebug(string log)
        {
            Logger.Instance.LogDebug(log);
        }

        protected void LogError(string log)
        {
            Logger.Instance.LogError(log);
        }

        protected void Log(Exception ex)
        {
            Logger.Instance.LogException(ex);
        }
        #endregion

        /// <summary>
        /// 初始化，加载配置文件
        /// </summary>
        public virtual bool Init(string instId)
        {
            this.InstId = instId;
            this.LoadConfig();

            if (!IsEmulationMode)
            {
                timer = new Timer(InnerOnTimer, null, TimerInterval, TimerInterval);
            }

            return true;
        }

        /// <summary>
        /// 释放的时候调用这个函数
        /// </summary>
        public virtual void Dispose()
        {
            this.Enable = false;
            timer?.Dispose();
            timer = null;
        }
    }
}
