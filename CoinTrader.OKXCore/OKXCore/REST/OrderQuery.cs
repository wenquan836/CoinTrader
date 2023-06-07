using CoinTrader.Common;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 挂单查询
    /// </summary>
    public class OrderQuery : RestAPIBase
    {
        public OrderQuery(string instId, long orderId) : base(APIUrl.OrderQuery, Http.Method_Get)
        {
            this.instId = instId;
            this.ordId = orderId.ToString();
        }
        public string instId { get; set; }
        public string ordId { get; set; }
    }
}
