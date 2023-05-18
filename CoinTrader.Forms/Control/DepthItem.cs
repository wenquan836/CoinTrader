using System;
using System.Drawing;
using System.Windows.Forms;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Util;

namespace CoinTrader.Forms.Control
{
    public partial class DepthItem : UserControl
    {
        public DepthItem()
        {
            InitializeComponent();

            this.PriceDecimal = 2;
            this.AmountDecimal = 3;
        }

        private int _priceDecimal = 2;
        private string formatter = "0.00";

        public int PriceDecimal
        {
            get { return this._priceDecimal; }
            set
            {
                if (value == this._priceDecimal)
                    return;

                this.formatter = "0";

                this._priceDecimal = value;

                if(value>0)
                {
                    this.formatter = "0.".PadRight(value+2, '0');
                }
            }
        }

        public int AmountDecimal
        {
            get;set;
        }

        public void SetData(decimal price, decimal amount,int orders, DepthBookList side)
        {
            this.lblPrice.Text  = Math.Round(price,this.PriceDecimal).ToString(formatter);
            this.lblAmount.Text = StringUtil.ToShortNumber(amount, this.AmountDecimal);
            this.lblOrders.Text = orders.ToString();

            Color color = side == DepthBookList.ASK ? Color.FromArgb (126, 14, 1) : Color.FromArgb(24, 189, 7);

            this.lblPrice.ForeColor  = color;
            this.lblAmount.ForeColor = color;
        }
    }
}
