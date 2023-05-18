using CoinTrader.Forms.Event;
using CoinTrader.OKXCore.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoinTrader.Forms
{

    /// <summary>
    /// 系统心跳定时器
    /// </summary>
    internal static class Ticker
    {
        private static readonly int interval = 33;//按33毫秒定时器触发心跳
        private static Timer timer;
        public static void Stop()
        {
            timer.Dispose();
            timer = null;
        }

        public static void Start()
        {
            if (timer != null) return;
            timer = new Timer((obj) => PureMVC.SendNotification(CoreEvent.SystemTick, interval), null, interval, interval);
        }
    }
}
