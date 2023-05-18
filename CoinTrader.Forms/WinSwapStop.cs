
using System;
using System.Windows.Forms;
using CoinTrader.OKXCore.Event;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Manager;

namespace CoinTrader.Forms
{
    public partial class WinSwapStop : Form
    {
        public WinSwapStop()
        {
            InitializeComponent();
        }

        private long id = 0;
        InstrumentSwap instrument = null;
        public void SetId(long posId)
        {
            this.id = posId;
            Position pos = PositionManager.Instance.GetPosition(id);

            
            if(pos != null)
            {
                this.Text += pos.PosSideName + pos.InstName;
                InstrumentSwap inst = InstrumentManager.SwapInstrument.GetInstrument(pos.InstId);
                instrument = inst;
                this.lblAmount.Text = (inst.CtVal * pos.AvailPos).ToString(inst.AmountFormat) + inst.CtValCcy;
                this.lblMinSize.Text = inst.CtVal.ToString() + inst.CtValCcy;
                this.txtAmount.Text = inst.CtVal.ToString();
                this.tbSize.Minimum = (int)inst.MinSize;
                this.tbSize.Maximum = (int)pos.AvailPos;
            }
        }


        private bool SetStop()
        {
            Position pos = PositionManager.Instance.GetPosition(id);

            if (pos != null)
            {
                bool stopLose = rdoStopLose.Checked;
                decimal price = 0;

                if (decimal.TryParse(txtPrice.Text, out price))
                {
                    bool priceOk = true;
                    if (pos.PosSide == PositionSide.Short ) // 空头仓位
                    {
                        if (stopLose && price < pos.MarkPx)//止损
                        {
                            PureMVC.SendNotification(CoreEvent.UIPopError, "止损委托价格不能大于标记价格");
                            priceOk = false;
                        }
                        else if(!stopLose && price > pos.MarkPx)
                        {
                            PureMVC.SendNotification(CoreEvent.UIPopError, "止盈委托价格不能小于标记价格");
                            priceOk = false;
                        }
                    }
                    else if (pos.PosSide == PositionSide.Long)
                    {
                        if (stopLose && price > pos.MarkPx)
                        {
                            PureMVC.SendNotification(CoreEvent.UIPopError, "止损委托价格不能小于标记价格");
                            priceOk = false;
                        }
                        else if (!stopLose && price < pos.MarkPx)
                        {
                            PureMVC.SendNotification(CoreEvent.UIPopError, "止盈委托价格不能大于标记价格");
                            priceOk = false;
                        }
                    }

                    if(!priceOk)
                    {
                        return false;
                    }
                }
                else
                {
                    PureMVC.SendNotification(CoreEvent.UIPopError, "请输入委托仓价格");
                    return false;
                }


                decimal amount = decimal.Parse(this.txtAmount.Text);
                if (stopLose)
                    PositionManager.Instance.SetStopLoss(this.id, amount, price);
                else
                    PositionManager.Instance.SetTakeProfit(this.id, amount, price);
            }

            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.SetStop())
                this.Close();
        }

        private void WinSwapLiquidate_Load(object sender, EventArgs e)
        {

        }

        private void tbSize_MouseUp(object sender, MouseEventArgs e)
        {
            this.txtAmount.Text = (this.instrument.CtVal * this.tbSize.Value).ToString(instrument.AmountFormat);
        }

        private void tbSize_MouseMove(object sender, MouseEventArgs e)
        {
            this.txtAmount.Text = (this.instrument.CtVal * this.tbSize.Value).ToString(instrument.AmountFormat);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if(decimal.TryParse(txtAmount.Text, out var val))
            {
                val = Math.Max(val /instrument.CtVal, instrument.MinSize);
                val = Math.Min( tbSize.Maximum, val );

                tbSize.Value = (int)Math.Round(val);
            }
        }

        private void rdoLimit_CheckedChanged(object sender, EventArgs e)
        {
 
        }
    }
}
