using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Manager;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoinTrader.Forms.Control
{
    public partial class SwapInfoViewSmall : UserControl
    {
        public SwapInfoViewSmall()
        {
            InitializeComponent();
        }

        public long Id
        {
            get; private set;
        }

        private InstrumentSwap _instrument;

        public void SetId(long id)
        {
            this.Id = id;

            var position = PositionManager.Instance.GetPosition(this.Id);

            if (position != null)
            {
                this._instrument = InstrumentManager.SwapInstrument.GetInstrument(position.InstId);
                this.lblSide.Text = position.PosSideName;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var position = PositionManager.Instance.GetPosition(this.Id);

            if (position != null)
            {
                this.lblAvgPx.Text = position.AvgPx.ToString(_instrument.PriceFormat);
                this.lblLiqPx.Text = position.LiqPx.ToString(_instrument.PriceFormat);
                this.lblUpl.Text = String.Format("{0:0.00} ({1:P})", position.Upl, position.UplRatio);
                this.lblUpl.ForeColor = position.Upl >= 0 ? Color.Green : Color.Red;
                this.lblMargin.Text = String.Format("{0:0.00}", position.Margin);
                this.lblLever.Text = position.Lever + "x";
                this.lblAmount.Text = (position.Pos * _instrument.CtVal).ToString(_instrument.AmountFormat) + _instrument.CtValCcy;
            }
            else
            {
                //this.Parent = null;//自动移除？
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Parent = null;
        }

        private void Liquidate_Click(object sender, EventArgs e)
        {
            var win = new WinSwapLiquidate();
            win.SetId(this.Id);

            win.ShowDialog();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {

            var result = MessageBox.Show("确定全部平仓?", "平仓", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                PositionManager.Instance.RemovePosition(this.Id);
            }
        }

        private void btnMagrin_Click(object sender, EventArgs e)
        {
            var win = new WinMarginChange();
            win.SetId(this.Id);

            win.ShowDialog();
        }



        private void btnStop_Click(object sender, EventArgs e)
        {
            var win = new WinSwapStop();
            win.SetId(this.Id);
            win.ShowDialog();
        }
    }
}
