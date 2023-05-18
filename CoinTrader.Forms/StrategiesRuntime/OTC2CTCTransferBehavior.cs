using CoinTradeGecko.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeGecko.Okex.Behavior
{
    [BehaviorName(Name = "资金到币币转账")]
    public class OTC2CTCTransferBehavior:WalletTransferBehavior
    {
        public OTC2CTCTransferBehavior(CurrencyMarket market):base(market, WalletType.OTC, WalletType.CTC)
        {
            this.Enable = true;
        }
    }
}
