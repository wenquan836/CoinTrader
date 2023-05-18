
using CoinTrader.Common.Classes;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Manager;
using System;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    public partial class WinMarginChange : Form
    {
        public WinMarginChange()
        {
            InitializeComponent();
        }

        private long id;
        public void SetId(long id)
        {
            this.id = id;

            Position pos = PositionManager.Instance.GetPosition(id);

            Wallet usdxWallet = USDXWallet.Instance;

            this.txtAmount.Range = new Range<decimal>( 1, usdxWallet.AvailableInTrading);
            this.txtAmount.NumberType = Control.NumberType.Decimal;

            if(pos != null)
            {
                this.lblMargin.Text = String.Format("{0:0.00} ({1:P})", pos.Margin,pos.MgnRatio);
                this.lblMMR.Text = String.Format("{0:0.00}",pos.MMR);
                this.lblUsdxAmount.Text = Math.Floor(usdxWallet.AvailableInTrading).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal amount = decimal.Parse(this.txtAmount.Text);

            PositionManager.Instance.ChangeMarginBalance(id, amount);

            this.Close();
        }
    }
}
