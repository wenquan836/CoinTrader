using CoinTradeGecko.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeGecko.Okex.Behavior
{
    [BehaviorName(Name = "币币到资金转账")]
    public class CTC2OTCTransferBehavior: WalletTransferBehavior
    {
        public CTC2OTCTransferBehavior(CurrencyMarket market):base(market, WalletType.CTC, WalletType.OTC)
        {
            this.Enable = true;
        }
    }
}
