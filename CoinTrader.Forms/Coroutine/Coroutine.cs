using CommonTools.Coroutines;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    /// <summary>
    /// 协助程序
    /// </summary>
    internal static class Coroutine
    {
        /// <summary>
        /// 用于判断协程执行间隔时间
        /// </summary>
        class TimeProvider:ITimeProvider
        {
            DateTime startTime = DateTime.Now;
            public float GetCurrentTime()
            {
                var now = DateTime.Now;

                return (float)( (now - startTime).TotalMilliseconds * 0.001f);
            }
        }

        static  Timer mainThreadTimer = null;
        private class CoroutineExecutor : ThreadSafeCoroutinesExecutorBase
        {
            internal void Update(int ms)
            {
                base.Update();
            }
        }

        static CoroutineExecutor executor = new CoroutineExecutor();

        internal static void Update(int dt)
        {
            executor?.Update(dt);
        }

        internal static void Init()
        {
            if (mainThreadTimer != null)
                return;

            Timer t  = new Timer();
            t.Tick += Timer_Tick;
            t.Interval = 33;
            t.Enabled = true;
            mainThreadTimer = t;
            TimeProviders.DefaultTimeProvider = new TimeProvider();
        }

        private static void Timer_Tick(object sender, System.EventArgs e)
        {
            Update(mainThreadTimer.Interval);
        }

        internal static ICoroutine StartCoroutine(IEnumerator<IYieldInstruction> iterator)
        {
            return executor?.StartCoroutine(iterator);
        }

        internal static void Dispose()
        {
            executor?.Dispose();
        }
    }

}
