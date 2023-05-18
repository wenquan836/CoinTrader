using CoinTrader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 修改订单
    /// </summary>
    public class OrderModify : RestAPIBase
    {
        public OrderModify(long id, string instId, decimal amount, decimal price) : base(APIUrl.OrderModify, Http.Method_Post)
        {
            this.instId = instId;
            this.ordId = id.ToString();
            this.newSz = amount.ToString();
            this.newPx = price.ToString();
        }

        public string instId { get; set; }

        public string ordId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool cxlOnFail { get; set; }

        public string newSz { get; set; }

        public string newPx { get; set; }
    }
}
