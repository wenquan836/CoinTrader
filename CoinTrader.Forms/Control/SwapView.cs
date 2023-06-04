using CoinTrader.Forms.Chromium;
using CoinTrader.Forms.Strategies;
using CoinTrader.Forms.StrategiesRuntime;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;
using CoinTrader.Strategies;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoinTrader.Forms.Control
{
    public partial class SwapView : UserControl, IMarketView
    {
        MarketDataProvider dataProvider = null;
        private InstrumentSwap instrument = null;
        private string instId = "";
        public string InstId
        {
            get
            {
                return instId;
            }
        }
        public decimal TotalAmount
        {
            get
            {
                var postions = PositionManager.Instance.GetPositions(this.instId);
                decimal amount = 0;
                foreach(var p in postions)
                {
                    amount += p.Upl + p.Margin;
                }

                return amount;
            }
        }

        public SwapView()
        {
            InitializeComponent();
        }

        public void SetInstId(string instId)
        {
            dataProvider = DataProviderManager.Instance.GetProvider(instId);
            this.instrument = InstrumentManager.SwapInstrument.GetInstrument(instId);
            this.lblInstrument.Text = instId;
            this.instId = instId;
            this.lblMinSize.Text = instrument.MinSize.ToString();
            this.lblMinAmount.Text = instrument.CtVal.ToString();
            this.lblLever.Text = instrument.Lever.ToString();
            this.lblFee.Text = instrument.Category.ToString();
            RefreshStrategies();
        }

        public void RefreshStrategies()
        {
            var list = StrategyRunner.Instance.GetStrategiesByInstId(InstId);
            this.pnlBehavior.Controls.Clear();
            foreach (var strategy in list)
            {
                var view = new StrategyView();
                view.SetStrategy(strategy);
                this.pnlBehavior.Controls.Add(view);
            }
        }

        private void ShowMonitorList()
        {
            foreach (System.Windows.Forms.Control c in this.pnlMonitor.Controls)
            {
                c.Visible = false;
                (c as MonitorView).monitor = null;
            }

            int mindex = 0;
            foreach (var m in dataProvider.GetAllMonitor())
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

        private void Clean()
        {
            if (dataProvider != null)
            {
                DataProviderManager.Instance.ReleaseProvider(dataProvider);
                this.dataProvider = null;
            }
            StrategyRunner.Instance.StopStrategiesByInstId(InstId);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (this.Parent == null)
            {
                this.Clean();
            }

            base.OnParentChanged(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Parent = null;
        }

        private void HidePosition()
        {
            var controls = this.pnlPosition.Controls;

            for(int i = controls.Count - 1; i>0;i-- )
            {
                controls[i].Parent = null;
            }

            this.timerPosition.Enabled = false;
        }
        private void ShowPosition()
        {
            this.timerPosition.Enabled = true;
            var postions = PositionManager.Instance.GetPositions(this.instId);
            var controls = this.pnlPosition.Controls;
            int mindex = 1;
            foreach (var pos in postions)
            {
                SwapInfoView pv;
                if (this.pnlPosition.Controls.Count > mindex)
                {
                    var control = this.pnlPosition.Controls[mindex];
                    pv = control as SwapInfoView;
                    pv.Visible = true;
                }
                else
                {
                    pv = new SwapInfoView();
                    this.pnlPosition.Controls.Add(pv);
                }

                pv.SetId(pos.PosId);
                mindex++;
            }

            for (int i = mindex; i<controls.Count;i++)
            {
                var c = controls[i];
                c.Visible = false;
            }

            this.pnlEmpty.Visible = postions.Count == 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.dataProvider != null)
            {
                this.tickView1.ShowTickerPrice(dataProvider.Ask, dataProvider.Bid);
            }

            this.lblMonitor.ForeColor = this.dataProvider != null && this.dataProvider.Effective ? Color.Red : Color.Black;

            lblPostion.Visible = PositionManager.Instance.HasPosition(this.instId);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == this.tabPage3)
            {
                this.ShowMonitorList();
                this.HidePosition();
            }
            else if (this.tabControl1.SelectedTab == this.tabPage2)
            {
                this.ShowPosition();
                this.pnlMonitor.Controls.Clear();
            }
            else if (this.tabControl1.SelectedTab == this.tabPage1)
            {
                this.HidePosition();
                this.pnlMonitor.Controls.Clear();
            }
        }

        private void lblInstrument_Click(object sender, EventArgs e)
        {
            var chartWindow = WindowManager.Instance.OpenWindow<WebTradeView>();
            chartWindow.InstId = this.InstId; 

            chartWindow.Show();
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            WinSwapStat win = new WinSwapStat();
            win.SetCurrency(this.InstId);
            win.Show();
        }

        private void timerPosition_Tick(object sender, EventArgs e)
        {
            this.ShowPosition();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        
        private void btnBuy_Click(object sender, EventArgs e)
        {
            this.DoTrade(PositionType.Long);
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            this.DoTrade(PositionType.Short);
        }

        private void DoTrade(PositionType side)
        {
            /*
            decimal amount;
            if(decimal.TryParse(this.txtNum.Text,out amount))
            {
               if( PositionManager.Instance.CreatePosition(this.instId, side, amount,true)>0)
                {
                    PureMVC.SendNotification(CoreEvent.UIPopInfo, "下单成功");
                }
            }
            */
        }
        
        private void txtNum_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
