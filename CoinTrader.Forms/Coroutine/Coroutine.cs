using CommonTools.Coroutines;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    internal static class Coroutine
    {
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
