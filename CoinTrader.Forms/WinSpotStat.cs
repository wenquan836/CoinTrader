
using CoinTrader.OKXCore.VO;
using CoinTrader.Common;
using CoinTrader.Common.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Manager;
using CoinTrader.Common.Util;

namespace CoinTrader.Forms
{
    public partial class WinSpotStat : Form
    {

        private Pool<TradeOrder> pool = Pool<TradeOrder>.GetPool();
        public List<TradeOrder> orders = new List<TradeOrder>();
        string currency1, currency2;
        decimal priceBid = 0;
        InstrumentSpot instrument = null;

        public WinSpotStat()
        {
            InitializeComponent();

            this.cmbResyncDays.SelectedIndex = 0;
        }

        private void WinCTCStat_Load(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            this.dtpStart.Value = now.AddMonths(-1);
            this.dtpEnd.Value = now;

            this.timer1.Enabled = true;
        }

        public void SetCurrency(string instId, decimal price)
        {
         
            this.priceBid = price;
            this.timer1.Enabled = true;

            instrument = InstrumentManager.SpotInstrument.GetInstrument(instId);
            currency2 = instrument.QuoteCurrency;
            currency1 = instrument.BaseCurrency;
            TradeHistoryManager.Instance.LoadHistory(instrument.InstrumentId);
        }


        int pageSize = 100;
        bool isLoading = false;

        private void LoadData()//(DateTime start, DateTime end)
        {
            isLoading = true;
            var pool = Pool<TradeOrder>.GetPool();
            pool.Put(this.orders);
            this.orders.Clear();
            var startTime = dtpStart.Value.Date;
            var endTime = dtpEnd.Value.Date.AddDays(1);
            this.orders = TradeHistoryManager.Instance.GetHistoryOrders(instrument.InstrumentId, startTime,endTime);

            this.cmbPage.Items.Clear();

            int pageCount = orders.Count / pageSize + (orders.Count % pageSize > 0 ? 1 : 0);

            for(int i = 1; i <= pageCount;i++)
            {
                this.cmbPage.Items.Add(i);
            }

            if(orders.Count > 0)
            {
                this.cmbPage.SelectedIndex = 0;
            }

            this.Stat(this.orders);
            this.ShowData();

            isLoading = false;
        }

        private void Stat(IList<TradeOrder> orders)
        {
            BalanceVO balance = AssetsManager.Instance.GetBalance(BalanceType.Trading, this.currency1);

            IEnumerable<TradeOrder> historyList = orders.Reverse();
            decimal totalBuyAmount = 0;
            decimal totalSellAmount = 0;
            decimal totalBuyMoney = 0;
            decimal totalSellMoney = 0;
            decimal balanceMoney = priceBid * (balance.Avalible + balance.Frozen);
            int maxBuyTimes = 0;  //最大连续买入次数
            int maxSellTimes = 0; //最大连续卖出次数

            decimal curBuyAmount = 0; //最大连续买入额度
            decimal maxBuyAmount = 0; //最大连续买入额度

            int buyTimes = 0;
            int sellTimes = 0;

            decimal totalFee = 0;
            int totalSellOrder = 0;
            int totalBuyOrder = 0;

            

            foreach (TradeOrder o in historyList)
            {
                decimal amount = o.FilledSize * o.PriceAvg;
                switch (o.Side)
                {
                    case OrderSide.Buy:
                        totalBuyAmount += o.FilledSize;
                        totalBuyMoney += amount;
                        totalBuyOrder++;
                        buyTimes++;
                        sellTimes--;
                        curBuyAmount += amount;
                        break;
                    case OrderSide.Sell:
                        totalSellAmount += o.FilledSize;
                        totalSellMoney += amount;
                        totalSellOrder++;
                        sellTimes++;
                        buyTimes--;
                        curBuyAmount -= amount;
                        break;
                }

                buyTimes = Math.Max(0, buyTimes);
                sellTimes = Math.Max(0, sellTimes);
                curBuyAmount = Math.Max(0, curBuyAmount);

                maxBuyTimes = Math.Max(maxBuyTimes, buyTimes);
                maxSellTimes = Math.Max(maxSellTimes, sellTimes);
                maxBuyAmount = Math.Max(maxBuyAmount, curBuyAmount);


                decimal fee = o.Fee;

                if (string.Compare(o.FeeCurrency, this.currency1, true) == 0)
                {
                    fee = o.Fee * o.PriceAvg;
                }

                totalFee += fee;
            }

            this.lblMaxBuy.Text = maxBuyAmount.ToString("0.00") + string.Format("({0}单)", maxBuyTimes);
            this.lblTotalBuy.Text = totalBuyMoney.ToString("0.00") + string.Format("({0}单)", totalBuyOrder);
            this.lblTotalSell.Text = totalSellMoney.ToString("0.00") + string.Format("({0}单)", totalSellOrder);
            this.lblTotalProfit.Text = (totalSellMoney - totalBuyMoney + balanceMoney + totalFee).ToString("0.00");
            this.lblBalance.Text = balanceMoney.ToString("0.00");
            this.lblFee.Text = totalFee.ToString("0.00");

            this.lblBuyCount.Text = Math.Round(totalBuyAmount, instrument.MinSizeDigit).ToString();
            this.lblSellCount.Text = Math.Round(totalSellAmount, instrument.MinSizeDigit).ToString();


            totalBuyAmount = Math.Max(totalBuyAmount, instrument.MinSize);
            totalSellAmount = Math.Max(totalSellAmount, instrument.MinSize);
           

            this.lblBuyAvg.Text = (totalBuyMoney / totalBuyAmount).ToString(instrument.PriceFormat);
            this.lblSellAvg.Text = (totalSellMoney / totalSellAmount).ToString(instrument.PriceFormat);

        }
        
        static Queue<ListViewItem> listPool = new Queue<ListViewItem>();
        private void ShowData()
        {
            int pageIndex = cmbPage.SelectedIndex;
            
            foreach(ListViewItem item in listView1.Items)
            {
                listPool.Enqueue(item);
            }

            this.listView1.Items.Clear();
            List<ListViewItem> items = new List<ListViewItem>();
            string[] columns = { "datetime", "currency", "side", "size", "price", "fillSize", "priceAvg","update","fee" };

            
            for(int index = 0;index < orders.Count;index ++)
            {
                TradeOrder o = this.orders[index];
                if (index < pageIndex * pageSize || index >= pageSize * (pageIndex + 1))
                {
                    continue;
                }

                ListViewItem item = null;

                if (listPool.Count > 0)
                {
                    item = listPool.Dequeue();
                    item.Text = o.PublicId.ToString();
                    item.Checked = false;
                    item.Selected = false;
                    this.listView1.Items.Add(item);
                }
                else
                {
                   item = this.listView1.Items.Add(o.PublicId.ToString());

                    for (int i = 0; i < columns.Length; i++)
                    {
                        var s = new ListViewItem.ListViewSubItem();
                        s.Name = columns[i];// columns[i];
                        item.SubItems.Add(s);
                    }
                }

                item.SubItems["datetime"].Text = DateUtil.UtcToLocalTime( o.CreatedDate).ToString("yyyy-MM-dd HH:mm:ss");
                item.SubItems["currency"].Text = o.InstId;
                item.SubItems["side"].Text = o.Side == OrderSide.Buy ? "买入" : "卖出"; 
                item.SubItems["size"].Text = o.Size.ToString();
                item.SubItems["price"].Text = o.Price.ToString();
                item.SubItems["fillSize"].Text = o.FilledSize.ToString();
                item.SubItems["priceAvg"].Text = o.PriceAvg.ToString();
                item.SubItems["update"].Text = DateUtil.UtcToLocalTime(o.UpdateTime).ToString("yyyy-MM-dd HH:mm:ss");

                string strFee = "";
                if( string.Compare(o.FeeCurrency,currency1, true) == 0)
                {
                    strFee = string.Format("{0}{1} ≈ {2:0.00}{3}",o.Fee,o.FeeCurrency,(o.Fee * o.PriceAvg),currency2);
                }
                else
                {
                    strFee = string.Format("{0}{1}", o.Fee, o.FeeCurrency);
                }

                item.SubItems["fee"].Text = strFee;

                item.Tag = o;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!TradeHistoryManager.Instance.IsLoading(instrument.InstrumentId))
            {
                this.LoadData();
                this.timer1.Enabled = false;
                this.lblWaiting.Visible = false;
                panel2.Enabled = true;
                /*
                btnRefresh.Enabled = true;
                btnResync.Enabled = true;
                cmbPage.Enabled = true;
                */
            }
            else
            {
                panel2.Enabled = false;
                this.lblWaiting.Visible = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void btnSelOnly_Click(object sender, EventArgs e)
        {
            List<TradeOrder> orders = new List<TradeOrder>();

            foreach(ListViewItem item  in this.listView1.Items)
            {
                if(item.Checked)
                {
                    orders.Add(item.Tag as TradeOrder);
                }
            }

            this.Stat(orders);
        }

        private void btnResync_Click(object sender, EventArgs e)
        {
            int days = 0;

            switch (cmbResyncDays.SelectedIndex)
            {
                case 0:
                    days = 3;
                    break;
                case 1:
                    days = 7;
                    break;
                case 2:
                    days = 14;
                    break;
                case 3:
                    days = 30;
                    break;
                case 4:
                    days = 60;
                    break;
            }

            this.timer1.Enabled = true;
            TradeHistoryManager.Instance.ResyncSpotHistory(instrument.InstrumentId, days);
            if (!TradeHistoryManager.Instance.IsLoading(instrument.InstrumentId))
            {
                this.LoadData();
                this.timer1.Enabled = false;
            }
        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                this.ShowData();
            }
        }

        private void WinCTCStat_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                listPool.Enqueue(item);
            }

            this.listView1.Items.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.listView1.SelectedItems.Count > 0)
            {
                var order = (TradeOrder)this.listView1.SelectedItems[0].Tag;
                this.txtId.Text = order.PublicId.ToString();
            }
        }

        private void btnFromId_Click(object sender, EventArgs e)
        {
            long id = 0;
            if(long.TryParse(txtId.Text, out id))
            {
                List<TradeOrder> orderList = new List<TradeOrder>();

                foreach(var order in this.orders)
                {
                    if(order.PublicId >= id)
                    {
                        orderList.Add(order);
                    }
                }

                this.Stat(orderList);
            }
            else
            {
                WinMessage.Show(MessageType.Alert, "请选中要开始统计的项目");
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.pool.Put(this.orders);
            base.OnClosing(e);
        }
    }
}
