using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTradeGecko.Okex;
using Newtonsoft.Json.Linq;

namespace CoinTradeGecko
{
    public partial class WinWithdrawal : Form
    {
        IWalletMonitor otcWallMonitor = null;
        ACTWalletMonitor actWallMonitor = null;

        public string Currency
        {
            get;set;
        }
        
        MonitorManager manager = null;

        public WinWithdrawal()
        {
            InitializeComponent();

            this.manager = new MonitorManager();

            string currency = Config.Instance.Anchor;

            this.Currency = currency;

            this.txtDestAccount.Text = Config.Instance.Account.GetLoginName();
            this.txtCurrency.Text = Config.Instance.Anchor;
            

            this.otcWallMonitor = new ACTWalletMonitor(currency);
            this.otcWallMonitor.OnData += OtcWallMonitor_OnData;
            this.manager.AddMonotor(otcWallMonitor);
            this.actWallMonitor = new ACTWalletMonitor(currency);
            this.actWallMonitor.OnData += ActWallMonitor_OnData;
            this.manager.AddMonotor(actWallMonitor);
        }

        private async void Withdrawal(decimal amount)
        {
            Okex_Rest_Api_Withdrawal api = new Okex_Rest_Api_Withdrawal();
            api.currency = this.Currency;
            api.amount = amount.ToString();
            api.fee = "0";
            api.destination = "3";
            api.to_address = txtDestAccount.Text;
            api.trade_pwd = txtPassword.Text;
            JToken result = await api.exec();
        }

        private async void TransferToAccout(decimal amount)
        {
            Okex_Rest_Api_Transfer api = new Okex_Rest_Api_Transfer(this.Currency, WalletType.OTC, WalletType.Account);

            api.amount = amount;
            JToken result = await api.exec();
        }

        private void ActWallMonitor_OnData(MonitorBase obj)
        {
            if(this.actWallMonitor.Availible > 0)
                this.Withdrawal(this.actWallMonitor.Availible);
        }

        private void OtcWallMonitor_OnData(MonitorBase obj)
        {
            if(this.otcWallMonitor.Availible > 0)
                this.TransferToAccout(this.otcWallMonitor.Availible);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.manager.Update(this.timer1.Interval);
        }
    }
}
