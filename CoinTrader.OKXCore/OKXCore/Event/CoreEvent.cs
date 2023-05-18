using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Event
{
    /// <summary>
    /// 消息列表
    /// </summary>
    public static class CoreEvent
    {
        public static readonly string SystemTick = "cmd_sys_tick"; //系统时钟调度
        public static readonly string BalanceTransfer = "cmd_balance_transfer";//仓位重新划转
        public static readonly string ConfigChanged = "cmd_config_changed";//修改配置文件
        public static readonly string BalanceChanged = "cmd_balance_changed";
        public static readonly string TradeNewOrder = "cmd_trade_new_order";
        public static readonly string TradeResyncBalance = "cmd_trade_resync_balance";
        public static readonly string TradeTick = "cmd_trade_tick";
        public static readonly string UIPopError = "cmd_ui_pop_err";
        public static readonly string UIPopWarning = "cmd_ui_pop_warning";
        public static readonly string UIPopInfo = "cmd_ui_pop_info";
        public static readonly string UpdateInstrument = "cmd_update_instrument";
        public static readonly string SocketDisconnect = "cmd_net_disconnect"; //断开连接
        public static readonly string SocketConnect = "cmd_net_connect";       //连接
        public static readonly string PublicSocketPing = "cmd_net_send_ping";      //ping
        public static readonly string PublicSocketData = "cmd_net_public_data"; //公共socket数据
        public static readonly string PublicSocketSubscribe = "cmd_net_subscribe"; //SOCKET订阅消息
        public static readonly string PublicSocketUnsubscribe = "cmd_net_unsubscribe"; //SOCKET取消订阅消息
        public static readonly string PrivateSocketData = "cmd_net_private_data"; //私有socket数据
        public static readonly string PrivateSocketSubscribe = "cmd_net_private_subscribe"; //私有频道SOCKET订阅消息
        public static readonly string PrivateSocketUnsubscribe = "cmd_net_private_unsubscribe"; //私有频道SOCKET取消订阅消息
    }
}
