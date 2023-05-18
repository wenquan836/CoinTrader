using CoinTrader.OKXCore.Event;

namespace CoinTrader.Forms.Command
{
    internal static class CommandRegister
    {
        public static void RegisterAllCommand()
        {
            //系统
            PureMVC.RegisterCommand<CmdSysTick>(CoreEvent.SystemTick);

            //网络
            PureMVC.RegisterCommand<CmdNetConnectSocket>(CoreEvent.SocketConnect);
            PureMVC.RegisterCommand<CmdNetDisconnect>(CoreEvent.SocketDisconnect);
            PureMVC.RegisterCommand<CmdNetSubstribe>(CoreEvent.PublicSocketSubscribe);
            PureMVC.RegisterCommand<CmdNetUnsubstribe>(CoreEvent.PublicSocketUnsubscribe);

            //交易
            PureMVC.RegisterCommand<CmdUpdateInstrument>(CoreEvent.UpdateInstrument);

            //ui
            PureMVC.RegisterCommand<CmdUIPop>(CoreEvent.UIPopError);
            PureMVC.RegisterCommand<CmdUIPop>(CoreEvent.UIPopWarning);
            PureMVC.RegisterCommand<CmdUIPop>(CoreEvent.UIPopInfo);
        }
    }
}
