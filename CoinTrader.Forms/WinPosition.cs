
using CoinTrader.Common;
using CoinTrader.Common.Util;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Manager;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    public partial class WinPosition : Form
    {
        static Color colorSell;
        static Color colorBuy;
        public WinPosition()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.UpdateList();
        }

        public void UpdateList()
        {
            var instTable = InstrumentManager.SwapInstrument;

            decimal totalProfit = 0;
            decimal totalMargin = 0;

            var mgr = PositionManager.Instance;

            int index = 0;

            string[] columns = { "posSide", "pos", "avgPx", "upl", "lever", "liqPx", "margin", "mgnRatio", "mmr", "cTime", "mode", "interest" };


            mgr.EachPosition((Position pos) => 
            { 
                var color = pos.PosSide == PositionSide.Short ? colorSell : colorBuy;

                ListViewItem listItem;
                if (listView1.Items.Count > index)
                {
                    listItem = listView1.Items[index];
                    listItem.Text = pos.InstName;
                }
                else
                {
                    listItem = new ListViewItem(pos.InstName);

                    foreach (string s in columns)
                    {
                        var sitem = new ListViewItem.ListViewSubItem();
                        sitem.Name = s;// columns[i];
                        listItem.SubItems.Add(sitem);
                    }

                    listView1.Items.Add(listItem);
                }

                listItem.BackColor = color;
                listItem.Tag = pos.PosId;
                var liqPxItem = listItem.SubItems["liqPx"];

                //liqPxItem.BackColor = Math.Abs(pos.LiqPx - pos.MarkPx) / pos.LiqPx > 0.005m ? Color.Green : Color.Yellow;

                var inst = instTable.GetInstrument(pos.InstId);

                totalProfit += pos.Upl;
                totalMargin += pos.Margin;

                string[] values = new string[] {
                 pos.PosSideName,
                 pos.Pos.ToString(),
                 pos.AvgPx.ToString(inst.PriceFormat),
                 pos.Upl.ToString("0.00"),
                 pos.Lever.ToString(),
                 pos.LiqPx.ToString(inst.PriceFormat),
                 pos.Margin.ToString("0.00"),
                 pos.MgnMode == "isolated"?  pos.MgnRatio.ToString("P"):"-",
                 pos.MMR.ToString("0.00"),
                 DateUtil.UtcToLocalTime(pos.CTime).ToString("yyyy-MM-dd HH:mm:ss"),
                 pos.MarginModeName,
                 pos.Interest.ToString()
                };

                for (int i = 0; i < columns.Length; i++)
                {
                    var sitem = listItem.SubItems[columns[i]];
                    sitem.Text = values[i];
                    sitem.BackColor = color;
                }

                //listItem.SubItems[0].ForeColor = pos.PosSide == Okex.Const.PositionSide.Short ? Color.Red : Color.Green;

                index++;
            });

            for (int i = this.listView1.Items.Count - 1; i >= index; i--)
            {
                this.listView1.Items.RemoveAt(i);
            }

            this.lblTotalProfit.Text = totalProfit.ToString("0.00");
            this.lblMargin.Text = totalMargin.ToString("0.00");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                long id = (long)this.listView1.SelectedItems[0].Tag;

                this.swapInfoView1.SetId(id);
                this.swapInfoView1.Visible = true;
            }
        }

        private void WinPosition_Load(object sender, EventArgs e)
        {
            colorSell = Color.FromArgb(255, 220, 220);
            colorBuy = Color.FromArgb(220, 255, 220);
            this.UpdateList();
        }
    }
}
