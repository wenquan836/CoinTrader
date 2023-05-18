using System;
using System.Drawing;
using System.Windows.Forms;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Manager;
using CoinTrader.Common.Util;

namespace CoinTrader.Forms
{
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
        }

        public event Action<long> OnCancelled;

        private void OrderView_Load(object sender, EventArgs e)
        {

        }

        public string InstrumentId
        {
            get;set;
        }

        public long OrderID
        {
            get;
            private set;
        }

        public void SetOrder(OrderBase order)
        {
            if (order == null)
            {
                this.OrderID = 0;
                return;
            }

            this.InstrumentId = order.InstId;
            this.OrderID = order.PublicId;
            
            this.lblSide.Text = order.Side == OrderSide.Buy ? "买入" : "卖出";
            this.lblSide.ForeColor = order.Side == OrderSide.Buy ? Color.Green : Color.Red;
            this.lblInfo.Text = $"{order.InstId} 单价 {order.Price} 金额 {StringUtil.ToShortNumber(order.AvailableAmount * order.Price, 2)}";
            this.btnOperate.Enabled = true;
            this.InstrumentId = order.InstId;
        }

        private void DoCancel(long orderId)
        {
            TradeOrderManager.Instance.CancelOrderAsync(this.InstrumentId, orderId);
        }

        private void btnOperate_Click(object sender, EventArgs e)
        {
            if(this.OrderID != 0)
            {
                this.DoCancel(this.OrderID);
                this.btnOperate.Enabled = false;   
            }
        }
    }
}
