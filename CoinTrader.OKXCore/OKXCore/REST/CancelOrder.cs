using CoinTrader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{

    /// <summary>
    /// 取消订单
    /// </summary>
    public class CancelOrder : RestAPIBase
    {
        public CancelOrder(string instrument_id, string id)
           : base(APIUrl.OrderCancel, Http.Method_Post)
        {
            this.instId = instrument_id;
            this.ordId = id;
        }

        /**
         * 订单的币对名称
         */
        public string instId { get; set; }

        /**
                 * 订单的币对名称
                 */
        public string ordId { get; set; }
    }
}
