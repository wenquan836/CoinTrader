using CoinTrader.Common;
using CoinTrader.OKXCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 资金划转
    /// </summary>
    public class Transfer : RestAPIBase
    {
        public Transfer(string currency, BalanceType from, BalanceType to) : base(APIUrl.Transfer, Http.Method_Post)
        {
            this.ccy = currency.ToUpper();
            this.from = (int)from;
            this.to = (int)to;
            this.amt = amt;
        }

        public string ccy { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public decimal amt { get; set; }

        protected override void OnReceiveData(APIResult data)
        {
            base.OnReceiveData(data);
        }
    }
}
