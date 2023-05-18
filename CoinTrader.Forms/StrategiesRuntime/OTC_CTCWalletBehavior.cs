using CoinTradeGecko.Monitor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTradeGecko.Okex.Behavior
{
    [BehaviorName(Name = "币币自动补货")]
    public class OTC_CTCWalletBehavior: BehaviorBase
    {
        OTCWalletMonitor otcWalletMonitor;
        CTCWalletRESTMonitor ctcWalletMonitor;
        CTCWalletRESTUSDTMonitor ctcUsdtWalletMonitor;
        CTCTickerMonitor ctcTicketMonitor;

        [BehaviorParameter(Name ="立即成交")]
        public bool CTCBuyNow { get; set; }
        
        private decimal _keepAmount = 0;

        [BehaviorParameter(Name ="持仓数量")]
        public decimal KeepAmount { get { return _keepAmount; } set { _keepAmount = value; } }

        public OTC_CTCWalletBehavior(OTCWalletMonitor otcWalletMonitor, CTCWalletRESTMonitor ctcWalletMonitor, CTCWalletRESTUSDTMonitor ctcUsdtWalletMonitor,CTCTickerMonitor tickerMonitor)
        {
            this.otcWalletMonitor       = otcWalletMonitor;
            this.ctcWalletMonitor       = ctcWalletMonitor;
            this.ctcUsdtWalletMonitor   = ctcUsdtWalletMonitor;
            this.ctcTicketMonitor       = tickerMonitor;

            otcWalletMonitor.OnData     += OtcWalletMonitor_OnData;
            ctcWalletMonitor.OnData     += CtcWalletMonitor_OnData;
            ctcUsdtWalletMonitor.OnData += CtcUsdtWalletMonitor_OnData;
        }


        private void AutoBuy()
        {
            if (!this.Enable)
                return;

            if (this.Excuting)
                return;

            if( this.ctcTicketMonitor.Effective && this.ctcUsdtWalletMonitor.Effective && this.otcWalletMonitor.Effective && this.ctcUsdtWalletMonitor.Effective)
            {
                var total = this.otcWalletMonitor.Availible + this.otcWalletMonitor.Hold + this.ctcWalletMonitor.Availible + this.ctcWalletMonitor.Hold;

                if(total < this.KeepAmount && this.KeepAmount > 0)
                {
                    decimal needUsdt = this.KeepAmount * ctcTicketMonitor.Ask;
                    if(ctcUsdtWalletMonitor.Availible > needUsdt)
                    {
                        this.Excuting = true;
                        this.BuyFromCTCMarket(this.KeepAmount);
                    }
                }
            }
        }

        async void BuyFromCTCMarket(decimal amount)
        {

            string currency = this.otcWalletMonitor.Currency;
            Okex_Rest_Api_CTCOrder api;
            //币币市场挂单
            if (CTCBuyNow)//马上成交、否则是挂单方式
            {
                decimal notional = amount * this.ctcTicketMonitor.Ask;
                notional = Math.Min(ctcUsdtWalletMonitor.Availible, notional);
                api = new Okex_Rest_Api_CTCBuyNow(currency, notional);
            }
            else
            {
                api = new Okex_Rest_Api_CTCOrder(currency, OrderOparete.Buy);
                api.price = this.ctcTicketMonitor.Ask.ToString();
                api.size = amount;
            }


            var result = await api.exec();

            if (result["code"].Value<int>() != 0)
            {
                Logger.Instance.LogError("币币下单失败 " + result.ToString());
            }
            this.Excuting = false;
        }
        private void CtcUsdtWalletMonitor_OnData(MonitorBase obj)
        {
            AutoBuy();
        }

        private void CtcWalletMonitor_OnData(MonitorBase obj)
        {
            AutoBuy();
        }

        private void OtcWalletMonitor_OnData(MonitorBase obj)
        {
            AutoBuy();
        }
    }
}
