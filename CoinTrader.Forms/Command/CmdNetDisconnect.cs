using CoinTrader.OKXCore.Network;
using PureMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Forms.Command
{
    internal class CmdNetDisconnect:ICommand
    {
        public void Execute(INotification notification)
        {
            PublicSocketConnect.Instance.CloseConnect();
            PrivateSocketConnect.Instance.CloseConnect();
        }

        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            throw new NotImplementedException();
        }
    }
}
