
using CoinTrader.OKXCore.Network;
using CoinTrader.OKXCore;
using PureMVC.Interfaces;
using System;

namespace CoinTrader.Forms.Command
{
    internal class CmdNetConnectSocket : ICommand
    {
        public void Execute(INotification notification)
        {
            string publicAddress = Config.Instance.ApiInfo.ApiAddress;
            string privateAddress = Config.Instance.ApiInfo.PrivateAddress;

            PublicSocketConnect.Instance.Connect(publicAddress);
            PrivateSocketConnect.Instance.Connect(privateAddress);
        }

        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            throw new NotImplementedException();
        }
    }
}
