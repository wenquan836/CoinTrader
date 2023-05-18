using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTrader.Forms.Control
{
    public partial class TickView : UserControl
    {

        private decimal ask;
        private decimal bid;
        public TickView()
        {
            InitializeComponent();
        }

        public void SetInstrumentId()
        {

        }

        public void ShowTickerPrice(decimal ask, decimal bid)
        {
            Color colorUp = Color.Green;
            Color colorDown = Color.Red;

            bool priceChanged = ask != this.ask || bid != this.bid;
            Color priceColor = ask > this.ask ? colorUp : colorDown;

            
            this.ask = ask;
            this.bid = bid;

            this.lblAskPrice.Text = ask.ToString();
            this.lblBidPrice.Text = bid.ToString();

            if (priceChanged)
            {
                this.lblAskPrice.ForeColor = priceColor;
                this.lblBidPrice.ForeColor = priceColor;
            }
        }

        public void ShowTickerPrice(string ask, string bid)
        {
            decimal numAsk = decimal.Parse(ask);
            decimal numBid = decimal.Parse(bid);

            this.ShowTickerPrice(numAsk,numBid);
        }
    }
}
