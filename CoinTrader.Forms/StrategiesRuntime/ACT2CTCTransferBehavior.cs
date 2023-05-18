using CoinTradeGecko.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeGecko.Okex.Behavior
{

    [BehaviorName(Name = "资金到币币转账")]
    public class ACT2CTCTransferBehavior: WalletTransferBehavior
    {
        public ACT2CTCTransferBehavior(CurrencyMarket market) : base(market, WalletType.Account, WalletType.CTC)
        {
            this.Enable = true;
        }
    }
}
