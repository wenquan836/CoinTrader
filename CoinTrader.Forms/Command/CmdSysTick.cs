
using CoinTrader.Common;
using CoinTrader.OKXCore.Event;
using PureMVC.Interfaces;
using System;


namespace CoinTrader.Forms.Command
{

    /// <summary>
    /// 系统时钟嘀嗒正常是10MS
    /// </summary>
    internal class CmdSysTick : ICommand
    {
        public void Execute(INotification notification)
        {
            int dt = (int)notification.Body;
            EventCenter.Instance.Send(CoreEvent.SystemTick, dt); //往事件调度转发心跳
        }

    
        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            throw new NotImplementedException();
        }
    }
}
