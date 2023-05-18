using CoinTrader.Common;
using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 下单
    /// </summary>
    public class CreateOrder : RestAPIBase
    {
        public CreateOrder(string instId, OrderSide side, string mode)
            : base(APIUrl.OrderCreate, Http.Method_Post)
        {
            this.ordType = OrderType.Limit;

            switch (side)
            {
                case OrderSide.Buy:
                    this.side =  Side.Buy;

                    break;
                case OrderSide.Sell:
                    this.side = Side.Sell;
                    break;
            }

            this.tdMode = mode;
            this.instId = instId;

            /*
{
    "code":"0",
    "msg":"",
    "data":[
        {
            "clOrdId":"oktswap6",
            "ordId":"12345689",
            "tag":"",
            "sCode":"0",
            "sMsg":""
        }
    ]
}
             */
        }

        /**
         * 币对名称
         */
        public string instId { get; set; }

        /**
         * 买入 buy 卖出 sell
         */
        public string side { get; set; }

        /**
         * 	limit，market(默认是limit)，当以market（市价）下单，ordType只能选择0:普通委托
         */
        public string ordType { get; set; }

        /**
         *如果是挂单则必须传这个参数，表示挂单价 平台要求传string 
         */
        public string px { get; set; } // 如果是挂单则必须传这个参数，表示挂单价 平台要求传string 

        /**
         * 挂单数量
         */
        public string sz { get; set; }//平台要求传string

        /**
         * 自定义订单标识
         */
        public string clOrdId { get; set; }

        /**
         * 交易模式
保证金模式：isolated：逐仓 ；cross：全仓
非保证金模式：cash：非保证金
         */
        public string tdMode { get; set; }


        /// <summary>
        /// 持仓方向 在双向持仓模式下必填，且仅可选择 long 或 short
        /// </summary>
        public string posSide { get; set; } = "";

        /**
         * 如果是市价立即成交则这个字段为买入金额（USDT）
         * 
         */
        //public string notional { get; set; }

        //{'type': 'limit', 'side': 'buy', 'instrument_id': 'BTC-USDT', 'size': 0.001, 'client_oid': 'oktspot79', 'price': '4638.51', 'funds': '', 'margin_trading': 1, 'order_type': '3'}
    }

}
