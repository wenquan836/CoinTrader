using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Forms
{
    internal static class PureMVC
    {
        private static IFacade facade = null;

        static PureMVC()
        {
            facade = Facade.GetInstance(() => new Facade());
        }


        public static void RegisterCommand( string notification, ICommand command)
        {
            facade.RegisterCommand( notification, ()=>command);
        }

        public static void RegisterCommand<T>(string notification) where T : ICommand, new()
        {
            RegisterCommand(notification,  new T());
        }

        public static void RegisterMediator(IMediator mediator)
        {
            facade.RegisterMediator(mediator);
        }

        public static IProxy RetrieveProxy<T>(bool newIfNotRegister = true) where T : IProxy, new()
        {
            string name = typeof(T).Name;
            IProxy proxy = facade.RetrieveProxy(typeof(T).Name);

            if (proxy == null && newIfNotRegister)
            {
                facade.RegisterProxy(new T());
                proxy = facade.RetrieveProxy(name);
            }

            return proxy;
        }

        public static void SendNotification(string notification, object body = null,string type = null)
        {
            facade.SendNotification(notification,body,type);
        }
    }
}
