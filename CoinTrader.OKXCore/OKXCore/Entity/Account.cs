using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Entity
{
    public class Account
    {
        public long UId { get; set; }

        public int AcctLv { get; set; }

        /// <summary>
        /// 仓位模式，逐仓、全仓
        /// </summary>
        public string PosMode { get; set; }
        public bool AutoLoan { get; set; }
        public string GreeksType{ get; set; }

        public string Level { get; set; }

        public string LevelTmp { get; set; }

        public string MgnIsoMode { get; set; }

        public string CtIsoMode { get; set; }

        /// <summary>
        /// 是否开启合约交易
        /// </summary>
        public bool SwapOpened => this.AcctLv > 1;
 

        public void ParseFromJson(JToken data)
        {
            this.UId = data.Value<long>("uid");
            this.AcctLv = data.Value<int>("acctLv");
            this.AutoLoan = data.Value<bool>("autoLoan");

            this.GreeksType = data.Value<string>("greeksType");
            this.Level = data.Value<string>("level");
            this.LevelTmp = data.Value<string>("levelTmp");
            this.PosMode = data.Value<string>("posMode");
            this.MgnIsoMode = data.Value<string>("mgnIsoMode");
            this.CtIsoMode = data.Value<string>("ctIsoMode");

        }
    }
}
