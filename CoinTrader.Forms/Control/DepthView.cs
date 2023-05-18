using System;
using System.Windows.Forms;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;

namespace CoinTrader.Forms.Control
{
    public partial class DepthView : UserControl
    {
        private IDepthProvider provider = null;
        private int PriceDecimal = 2;
        public DepthView()
        {
            InitializeComponent();
        }

        public void SetProvider(IDepthProvider provider)
        {
            this.provider = provider;
        }

        public void SetPriceDecimal(int priceDecimal)
        {
            this.PriceDecimal = priceDecimal;
        }
       
        private void ShowDepthBook(DepthBookList side, Panel panel)
        {
            if (this.provider == null) return;


            int index = 0;
            var controls = panel.Controls;
            
            this.provider.EachDepthBook(side, (deep) => {
                DepthItem v = null;

                if (controls.Count > index)
                {
                    v = controls[index] as DepthItem;
                    v.Visible = true;
                }
                else
                {
                    v = new DepthItem();
                    controls.Add(v);
                }
                v.PriceDecimal = this.PriceDecimal;
                v.SetData(deep.Price, deep.Total, (int)deep.Orders, side);
                index++;
            });

            for (var i = index; i < controls.Count; i++)
            {
                controls[i].Visible = false;
            }

            this.flpSell.VerticalScroll.Value = this.flpSell.VerticalScroll.Maximum;
        }

        private void UpdateDepthBook()
        {
            if(this.provider != null)
            {
                this.ShowDepthBook(DepthBookList.ASK, this.flpSell);
                this.ShowDepthBook(DepthBookList.BID, this.flpBuy);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this.provider != null)
                this.UpdateDepthBook();
        }
    }
}
