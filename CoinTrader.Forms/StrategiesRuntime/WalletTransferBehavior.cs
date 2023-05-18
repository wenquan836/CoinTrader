using CoinTradeGecko.Monitor;
using CoinTradeGecko.Okex.Behavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeGecko.Okex
{

    [BehaviorName(Name = "资金划转器")]
    public class WalletTransferBehavior : BehaviorBase
    {

        [BehaviorParameter(Name = "保留不划转额度", Min = 0)]
        public decimal KeepAmount { get; set; }

        private Okex_Rest_Api_Transfer restApi = null;
        private CurrencyMarket market = null;

        WalletType from;
        WalletType to;

        public WalletTransferBehavior(CurrencyMarket market, WalletType from, WalletType to)
        {
            if (market == null)
            {
                throw new Exception("无效的账户监视器");
            }

            this.from = from;
            this.to = to;

            market.OnAmountChanged += FromMonitor_OnData;
            this.market = market;
            
            this.restApi = new Okex_Rest_Api_Transfer(market.Currency, from, to);
            this.restApi.OnData += RestApi_OnData;
        }
        private void RestApi_OnData(Newtonsoft.Json.Linq.JToken obj)
        {
            //todo
            this.Excuting = false;
        }

        private void FromMonitor_OnData()
        {
            if (!this.Excuting && this.Enable)
            {

                decimal avalible = 0;

                switch(this.from)
                {
                    case WalletType.CTC:
                        avalible = market.AvalibleInCtcMarket;
                        break;
                    //case WalletType.OTC:
                    //    avalible = market.AvalibleInOtcMarket;
                    //    break;
                    case WalletType.Account:
                        avalible = market.AvalibleInAccount;
                        break;
                }

                if (avalible > this.KeepAmount)
                {
                    this.Excuting = true;
                    this.restApi.amount = avalible - this.KeepAmount;
                    this.restApi.execAsync();
                }
            }
        }
    }
}
