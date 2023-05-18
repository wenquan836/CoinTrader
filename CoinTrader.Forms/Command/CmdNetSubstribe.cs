using PureMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinTrader.OKXCore.Network;
using CoinTrader.OKXCore.VO;

namespace CoinTrader.Forms.Command
{
    internal class CmdNetSubstribe : ICommand
    {
        public void Execute(INotification notification)
        {
            var vo = notification.Body as SubscribeVO;
            if (vo != null)
            {
                PublicSocketConnect.Instance.Subscribe(vo.Channel,vo.InstId);
            }
        }

        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            throw new NotImplementedException();
        }
    }
}
