using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CoinTrader.Forms.Strategies;
using CoinTrader.Forms.Chromium;
using CoinTrader.OKXCore.Manager;
using CoinTrader.Strategies;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.REST;
using CoinTrader.Forms.StrategiesRuntime;

namespace CoinTrader.Forms.Control
{
    public partial class SpotView : UserControl, IMarketView
    {
        Wallet wallet = null;
        MarketDataProvider dataProvider = null;
        private InstrumentSpot instrument;
        public string Currency
        {
            get;
            private set;
        }

        public string InstId
        {
            get;
            private set;
        }
        
        public decimal TotalAmount
        {
            get
            {
                decimal bid = dataProvider.Bid;
                decimal ctcTotalAmount = wallet.AvailableInTrading + wallet.FrozenInTrading;
                return bid * ctcTotalAmount;
            }
        }

        public SpotView()
        {
            InitializeComponent();
        }

        public void SetInstId(string instId)
        {
            instrument = InstrumentManager.SpotInstrument.GetInstrument(instId);
            if (instrument == null)
                return;
            this.InstId = instId;
            dataProvider = DataProviderManager.Instance.GetProvider(InstId);

            if (dataProvider == null)
                return;
            this.Currency = instrument.BaseCurrency;
            wallet = new Wallet(Currency);

            this.lblCurrency.Text = this.Currency;

            this.depthView1.SetPriceDecimal(instrument.TickSizeDigit);

            RefreshStrategies();

            ShowMonitorList();
            this.timer1.Enabled = true;
            this.timer1.Start();

            TradeHistoryManager.Instance.LoadHistory(instrument.InstrumentId);
        }

        public void RefreshStrategies()
        {
            var list = StrategyRunner.Instance.GetStrategiesByInstId(InstId);
            this.pnlBehavior.Controls.Clear();
            foreach (var strategy in list)
            {
                var view = new StrategyView();
                view.SetStrategy(this.InstId, strategy);
                this.pnlBehavior.Controls.Add(view);
            }
        }


        private void UpdatePrice()
        {
            decimal ask = dataProvider.Ask;
            decimal bid = dataProvider.Bid;

            string strAsk = ask.ToString(this.instrument.PriceFormat);
            string strBid = bid.ToString(this.instrument.PriceFormat);

            if (this.tabMain.SelectedTab == this.tabTrade)
            {
                this.tickView1.ShowTickerPrice(strAsk, strBid);
            }
            else if (this.tabMain.SelectedTab == this.tabQuick)
            {
                this.tickView2.ShowTickerPrice(strAsk, strBid);
            }
        }

        private void UpdateAmount()
        {
            string formatter = instrument.AmountFormat;

            decimal ctcTotalAmount = wallet.AvailableInTrading + wallet.FrozenInTrading;

            decimal price = dataProvider.Bid;
            decimal totalMoney = (ctcTotalAmount * price);
            if (this.tabMain.SelectedTab == this.tabTrade)
            {
                this.lblCTCAvalible.Text = wallet.AvailableInTrading.ToString(formatter);
                this.lblCTCHold.Text = wallet.FrozenInTrading.ToString(formatter);
                this.lblTotal.Text = totalMoney.ToString("0.00");
            }
            else if (this.tabMain.SelectedTab == this.tabQuick)
            {
                this.lblCTCAvalible2.Text = wallet.AvailableInTrading.ToString(formatter);
                this.lblCTCHold2.Text = wallet.FrozenInTrading.ToString(formatter);
                this.lblTotal2.Text = totalMoney.ToString("0.00");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdatePrice();
            this.lblMonitor.ForeColor = dataProvider.Effective ? Color.Red : Color.Black;
        }

        private List<MonitorBase> GetAllMonitor()
        {
            var list = new List<MonitorBase>();
            list.AddRange(dataProvider.GetAllMonitor());
            return list;
        }

        private void ShowMonitorList()
        {
            foreach (System.Windows.Forms.Control c in this.pnlMonitor.Controls)
            {
                c.Visible = false;
                (c as MonitorView).monitor = null;
            }

            int mindex = 0;
            foreach (var m in GetAllMonitor())
            {
                MonitorView mv;
                if (this.pnlMonitor.Controls.Count > mindex)
                {
                    mv = this.pnlMonitor.Controls[mindex] as MonitorView;
                    mv.Visible = true;
                }
                else
                {
                    mv = new MonitorView();
                    this.pnlMonitor.Controls.Add(mv);
                }

                mv.monitor = m;

                mindex++;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.Parent = null; 
            this.Parent.Controls.Remove(this);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.UpdatePrice();
            this.UpdateAmount();
        }


        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabMain.SelectedTab == this.tabDepth)
            {
                this.depthView1.SetProvider(this.dataProvider);
            }
            else
            {
                this.depthView1.SetProvider(null);
            }
        }

        /// <summary>
        /// 从币币市场买入,以盘口价直接买入
        /// </summary>
        /// <param name="amount">数量</param>
        protected async void Buy(decimal amount)
        {
            string instId = InstId;
            CreateOrder api;
            amount = amount * dataProvider.Ask;//转为USDT数量
            decimal quoteCcy = AssetsManager.Instance.GetBalance(BalanceType.Trading, BalanceAmountType.Available, instrument.QuoteCcy);

            if (quoteCcy >= amount)
            {
                api = new SpotBuyImmediately(instId, amount);
                var result = await api.Exec();

                if (result.code != 0)
                {
                   //var msg =  result.message;  
                }
            }
        }

        /// <summary>
        /// 币币市场抛出,直接吃单卖出
        /// </summary>
        /// <param name="amount">数量</param>
        protected async void Sell(decimal amount)
        {
            var avalible = AssetsManager.Instance.GetBalance(BalanceType.Trading, BalanceAmountType.Available, Currency);
            amount = Math.Min(amount, avalible);

            string instId = InstId;

            if (amount > this.instrument.MinSize)
            {
                CreateOrder api = new SpotSellImmediately(instId, amount);
                var result = await api.Exec();
            }
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            var win = WindowManager.Instance.OpenWindow<WinSpotStat>();
            var price = dataProvider.Bid;
            win.SetCurrency(this.InstId, price);
            win.Show();
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            decimal buyAmount;
            if (decimal.TryParse(txtBuyAmount.Text, out buyAmount))
            {
                if (buyAmount > 0)
                {
                    decimal size = buyAmount / dataProvider.Ask;
                    if (size >= instrument.MinSize)
                    {
                        Buy(size);
                    }
                    else
                    {
                        decimal minAmount = instrument.MinSize * dataProvider.Bid;
                        WinMessage.Show(MessageType.Error, string.Format("买入金额不能小于 {0:0.00}", minAmount));
                    }
                }
            }
        }
        private void btnSell_Click(object sender, EventArgs e)
        {
            decimal sellAmount;
            if (decimal.TryParse(txtSellAmount.Text, out sellAmount))
            {
                if (sellAmount > 0)
                {
                    decimal size = sellAmount / dataProvider.Bid;

                    if (size >= instrument.MinSize)
                    {
                        Sell(size);
                    }
                    else
                    {
                        decimal minAmount = instrument.MinSize * dataProvider.Bid;
                        WinMessage.Show(MessageType.Error, string.Format("卖出金额不能小于 {0:0.00}", minAmount));
                    }
                }
            }
        }
        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要全部卖出吗？", "清仓", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

            }
        }

        private void btnAllIn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要全部买入吗？", "满仓", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

            }
        }

        private void lblCurrency_Click(object sender, EventArgs e)
        {
            var chartWindow = WindowManager.Instance.OpenWindow<WebTradeView>();
            chartWindow.InstId = instrument.InstrumentId;

            chartWindow.Show();
        }

        private void SpotView_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent == null)
            {
                StrategyRunner.Instance.StopStrategiesByInstId(InstId);
            }
        }
    }
}
