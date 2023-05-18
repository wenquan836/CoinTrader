using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Network
{
    public static class WebsocketChannels
    {
        public static readonly string Channel_ticker = "tickers";  //价格
        public static readonly string Channel_book_depth5 = "books5"; // 5档深度数据
        public static readonly string Channel_book_depth400 = "books"; // 400档深度数据

        //-----------------私有-------------------
        public static readonly string Channel_account = "account"; //账户
        public static readonly string Channel_positions = "positions"; //持仓频道
        public static readonly string Channel_orders = "orders"; //订单频道
        public static readonly string Channel_BalanceAndPosition = "balance_and_position"; //账户和持仓
    }
}
