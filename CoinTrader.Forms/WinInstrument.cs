
using CoinTrader.OKXCore.Manager;
using System;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    public partial class WinInstrument : Form
    {
        public WinInstrument()
        {
            InitializeComponent();
        }

        private void WinInstrument_Load(object sender, EventArgs e)
        {
            var list = InstrumentManager.SwapInstrument.GetAllInstrument();

            string[] columns = new string[] {"category","ctVal","ctMult","lever","minSz","tickSz","lotSz"}; 

            foreach(var item in list)
            {
                var instrument = item.Value;

                var listItem = new ListViewItem(instrument.InstrumentId);

                this.listView1.Items.Add(listItem);

                foreach(string s in columns)
                {
                    var sitem = new ListViewItem.ListViewSubItem();
                    sitem.Name = s;// columns[i];
                    listItem.SubItems.Add(sitem);
                }


                var values = new string[] {
                    instrument.Category.ToString()
                    ,instrument.CtVal.ToString() + instrument.CtValCcy
                    
                ,instrument.CtMult.ToString()
                ,instrument.Lever.ToString()
                 ,instrument.MinSize.ToString()
                ,instrument.TickSize.ToString()
                ,instrument.LotSz.ToString()
               
                };


                for(var i = 0; i < columns.Length;i++)
                {
                    listItem.SubItems[columns[i]].Text = values[i];
                }
                
                
            }
        }
    }
}
