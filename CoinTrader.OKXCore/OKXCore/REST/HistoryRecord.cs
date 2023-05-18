using CoinTrader.Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 获取交易历史订单数据的
    /// </summary>
    public class HistoryRecord : RestAPIBase
    {
        public HistoryRecord(string instrument_id, CTCHistoryOrderState state, long beforeOrderId = 0, int size = 20) :
            base(APIUrl.History)
        {
            this.instType = "SPOT";
            this.instId = instrument_id;
            //this.after = afterOrderId.ToString();
            this.before = beforeOrderId.ToString();
            this.limit = size.ToString();
            //this.state = "filled";// state.ToString();

        }

        public string after { get; set; }
        public string before { get; set; }
        public string instId { get; set; }
        public string instType { get; set; }
        public string state { get; set; }
        public string limit { get; set; }
    }
}
