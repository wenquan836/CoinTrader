using System;
using CoinTrader.OKXCore.Manager;
using PureMVC.Interfaces;

namespace CoinTrader.Forms.Command
{
    internal class CmdUpdateInstrument : ICommand
    {
        public void Execute(INotification notification)
        {
            InstrumentManager.UpdateInstrumentAsync(Config.Instance.UsdCoin);
        }

        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            throw new NotImplementedException();
        }
    }
}
