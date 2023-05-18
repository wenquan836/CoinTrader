using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 挂单查询
    /// </summary>
    public class OrderList : RestAPIBase
    {
        public OrderList() : base(APIUrl.Orders)
        {
            //this.instId = string.Format("{0}-{1}", currency1, currency2).ToUpper();
            //this.instType = "SPOT";
            //this.ordType = "limit";
        }

        //public Okex_Rest_Api_OrdersV5(string currency1, string currency2) : base(Okex_REST_API_V5.Orders)
        //{
        //this.instId = string.Format("{0}-{1}", currency1, currency2).ToUpper();
        //this.instType = "SPOT";
        //this.ordType = "limit";
        //}
        //public string ordType { get; set; }
        //public string instId { get; set; }
        //public string instType { get; set; }
    }
}
