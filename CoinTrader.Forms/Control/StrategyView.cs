using System;
using System.Drawing;
using System.Windows.Forms;
using CoinTrader.Forms.Strategies;
using CoinTrader.Strategies;

namespace CoinTrader.Forms.Control
{

    public partial class StrategyView : UserControl
    {
        private StrategyBase _strategy = null;
        public StrategyBase Strategy
        {
            get
            {
                return this._strategy;
            }
            private  set
            {
                this._strategy = value;

                this.chkEnable.Enabled = value != null;
                string name = "";
                if (value != null)
                {
                    var type = value.GetType();
                    foreach (var attr in type.GetCustomAttributes(false))
                    {
                        if (attr is StrategyAttribute MonitorNameAttribute)
                        {
                           name = (attr as StrategyAttribute).Name;
                            break;
                        }
                    }
                    chkEnable.ForeColor =  Color.Black;
                    chkEnable.Checked = value.Enable;
                }
                else
                {
                    chkEnable.Checked = false;
                    chkEnable.ForeColor = Color.Gray;
                }

                
                this.chkEnable.Text = name;
            }
        }

        public StrategyView()
        {
            InitializeComponent();

            this.chkEnable.Enabled = false;
        }

        public void SetStrategy(StrategyBase strategy)
        {
            this.Strategy = strategy;
        }


        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            this.Strategy.Enable = chkEnable.Checked;
            chkEnable.ForeColor = chkEnable.Checked ? Color.Black : Color.Gray;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this._strategy != null)
            {
                this.lblExcuting.ForeColor = this._strategy.Executing ? Color.Red : Color.Black;
                this.lblMessage.Text = this._strategy.Message;
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            WinStrategyParam win = new WinStrategyParam();
            win.Show();
            win.SetStrategy(this.Strategy);
        }
    }
}
