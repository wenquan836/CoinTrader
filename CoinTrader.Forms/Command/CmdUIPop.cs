using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinTrader.Forms.Event;
using CoinTrader.OKXCore.Event;
using PureMVC.Interfaces;

namespace CoinTrader.Forms.Command
{
    internal class CmdUIPop : ICommand
    {
        public void Execute(INotification notification)
        {
            MessageType mt = MessageType.Error;
            string name = notification.Name; 
            if(notification.Name == CoreEvent.UIPopError)
                    mt = MessageType.Error;
             else if(name ==  CoreEvent.UIPopWarning)
                    mt = MessageType.Alert;
              
             else if (name == CoreEvent.UIPopInfo)
                    mt = MessageType.Success;


            WinMessage.Show(mt, notification.Body.ToString());
        }

        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            throw new NotImplementedException();
        }
    }
}
