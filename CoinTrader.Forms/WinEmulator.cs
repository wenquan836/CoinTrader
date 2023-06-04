using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.Forms.Control;
using CoinTrader.Forms.Strategies;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.VO;
using CoinTrader.Strategies;
using CoinTrader.Strategies.Runtime;
using CommonTools.Coroutines;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    public partial class WinEmulator : Form
    {

        private StrategyEmulatorRuntime runtime = null;
        private WaitForSeconds wait = new WaitForSeconds(0.033f);

        private ICoroutine coroutine = null;
        private bool isStarted =false;

        private MarketDataProvider dataProvider = null;
        private List<Candle> candles = new List<Candle>();
        private decimal tickSize = 0;
        private int speed = 1;
        private string instId;
        private StrategyGroup group;
        private Candle newCandle = new Candle();
        private InstrumentBase instrument = null;
        private Queue<ListViewItem> orderListItemPool = new Queue<ListViewItem>();
        private Queue<ListViewItem> recordListItemPool = new Queue<ListViewItem>();
        private List<TradeStrategyBase> strategyList = new List<TradeStrategyBase>();
        public WinEmulator()
        {
            InitializeComponent();
            cmbCandle.SelectedIndex = 0;
            cmbSpeed.SelectedIndex = 0;
            cmbFee.SelectedIndex = 0;
        }

        public void SetStrategyGroup(StrategyGroup group, string forInstId)
        {

            if (group == null || string.IsNullOrEmpty(forInstId))
            {
                return; 
            }

            this.Text = $"{forInstId}{group.name} 复盘测试";

            this.instId = forInstId;
            instrument = InstrumentManager.GetInstrument(forInstId);

            if(instrument == null)
            {
                btnStartStop.Enabled = false;
                MessageBox.Show("找不到交易对" + instId,"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblBaseBalanceStr.Text = instrument.BaseCcy;
            lblQuoteBalanceStr.Text = instrument.QuoteCcy;



            this.group = group;
            foreach (var type in group.strategies)
            {
                var strategy = Activator.CreateInstance(type) as TradeStrategyBase;
                strategy.IsEmulationMode = true;
                if (strategy.Init(instId))
                {
                    strategyList.Add(strategy);
                }
                else
                {
                    MessageBox.Show($"初始化{type.Name}失败", "");
                    this.Close();
                    return;
                }

                strategy.Enable = true;
                StrategyView view = new StrategyView();
                view.SetStrategy(strategy);
                flowLayoutPanel1.Controls.Add(view);
                runtime = strategy.Runtime as StrategyEmulatorRuntime;
            }

            if (runtime != null)
            {
                UpdateBalance();
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if(isStarted)
            {
                StopEmulation();
                return;
            }

            timer1.Enabled = true;

            if(dataProvider == null)
                dataProvider = DataProviderManager.Instance.GetProvider(instId);

            if (dataProvider == null) return;

            CandleGranularity granularity = CandleGranularity.Week1;
            switch (cmbCandle.SelectedIndex)
            {
                case 0:
                    granularity = CandleGranularity.D1;
                    break;
                case 1:
                    granularity = CandleGranularity.H4;
                    break;
                case 2:
                    granularity = CandleGranularity.H1;
                    break;
                case 3:
                    granularity = CandleGranularity.M15;
                    break;
                case 4:
                    granularity = CandleGranularity.M5;
                    break;
            }

            var candleProvider = dataProvider.LoadCandle(granularity);
            var instrument = InstrumentManager.GetInstrument(instId);
            tickSize = instrument.TickSize; //最小价格粒度
            candleProvider.CandleLoaded += CandleProvider_CandleLoaded;

            cmbCandle.Enabled = false;

            if (decimal.TryParse(txtFunds.Text, out decimal funds))
            {
                var balance = default(BalanceVO);
                balance.Avalible = funds;
                runtime.QuoteBalance = balance;
            }

            runtime.Fee = decimal.Parse(cmbFee.Text) * 0.01m;
            
            if(funds <= 0)
            {
                MessageBox.Show("初始资金无效， 请重新输入");
                return;
            }

            isStarted = true;
            btnStartStop.Text = "停止";
            candles.Clear();
            candleView1.SetCandleData(candles);
            txtFunds.ReadOnly = true;

            if (candleProvider.Loaded)
                StartEmulation(candleProvider);
        }

        private void StopEmulation()
        {
            isStarted = false;
            btnStartStop.Text = "开始";
            cmbCandle.Enabled = true;
            txtFunds.ReadOnly = false;
            timer1.Enabled = false;
            if (coroutine != null)
            {
                coroutine.Dispose();
                coroutine = null;
            }
        }
        private void StartEmulation(ICandleProvider provider)
        {
            provider.EachCandle((candle) =>
            {
                var c = new Candle();
                c.CopyFrom(candle);
                candles.Add(c);
                return false;
            });

            candles.Reverse();
            if (candles.Count > 0)
            {
                candleView1.SetCandleData(new List<Candle>());
            }

            coroutine = Coroutine.StartCoroutine(CandleStart());
        }
        private void UpdateRecordView()
        {
            var history = runtime.GetHistoryList();
            int showCount = 20;
            int index = 0;
            foreach (var order in history)
            {

                var item = lvHistory.Items.Count > index ? lvHistory.Items[index] : GetRecordListItemFromPool();
                item.Text = order.PublicId.ToString();
                item.SubItems[1].Text = order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                item.SubItems[2].Text = order.InstId;
                item.SubItems[3].Text = order.Side.ToString();
                item.SubItems[4].Text = order.Amount.ToString();
                item.SubItems[5].Text = order.Price.ToString();
                item.SubItems[6].Text = order.PriceAvg.ToString();
                item.SubItems[7].Text = order.FilledSize.ToString();
                item.SubItems[8].Text = order.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                item.SubItems[9].Text = order.Fee.ToString() + order.FeeCurrency;
                if (lvHistory.Items.Count <= index)
                    lvHistory.Items.Add(item);


                index++;
                if (showCount <= 0)
                    break;
            }


            for (int i = lvHistory.Items.Count - 1; i >= showCount; i--)
            {
                recordListItemPool.Enqueue(lvHistory.Items[i]);
                lvHistory.Items.RemoveAt(i);
            }
        }

        private ListViewItem GetRecordListItemFromPool()
        {
            if (recordListItemPool.Count == 0)
            {
                ListViewItem item = new ListViewItem();
                for (int i = 0; i < lvHistory.Columns.Count - 1; i++)
                {
                    item.SubItems.Add("");
                }
                return item;
            }
            return recordListItemPool.Dequeue();
        }
        private ListViewItem GetOrderListItemFromPool()
        {
            if(orderListItemPool.Count == 0)
            {
                ListViewItem item = new ListViewItem();
                for(int i = 0;i< lvOrders.Columns.Count-1;i++)
                {
                    item.SubItems.Add("");
                }
                return item;
            }
            return orderListItemPool.Dequeue();
        }
        private void UpdateOrderView()
        {
            int showCount = 0;
            runtime.EachBuyOrder(order => {
                var item = lvOrders.Items.Count > showCount ? lvOrders.Items[showCount] : GetOrderListItemFromPool();
                item.Text = order.InstId;
                item.SubItems[1].Text = "买入";
                item.SubItems[2].Text = order.Price.ToString(instrument.PriceFormat);
                item.SubItems[3].Text = (order.Price * order.AvailableAmount).ToString(instrument.PriceFormat);
                if(lvOrders.Items.Count <= showCount)
                    lvOrders.Items.Add(item);
                showCount ++;
            });
            runtime.EachSellOrder(order => {
                var item = lvOrders.Items.Count > showCount ? lvOrders.Items[showCount] : GetOrderListItemFromPool();
                item.Text = order.InstId;
                item.SubItems[1].Text = "卖出";
                item.SubItems[2].Text = order.Price.ToString(instrument.PriceFormat);
                item.SubItems[3].Text = (order.Price * order.AvailableAmount).ToString(instrument.PriceFormat);
                if (lvOrders.Items.Count <= showCount)
                    lvOrders.Items.Add(item);

                showCount++;
            });

            for (int i = lvOrders.Items.Count - 1; i >= showCount; i--)
            {
                orderListItemPool.Enqueue(lvOrders.Items[i]);
                lvOrders.Items.RemoveAt(i);
            }
        }

        private void UpdateBalance()
        {
            var baseBalance = runtime.BaseBalance;
            var quoteBalance = runtime.QuoteBalance;
            lblQuoteBalance.Text = $"可用:{quoteBalance.Avalible:0.00} 冻结:{quoteBalance.Frozen:0.00}";
            lblBaseBalance.Text = $"可用:{Math.Round( baseBalance.Avalible,instrument.MinSizeDigit)} 冻结:{ Math.Round( baseBalance.Frozen,instrument.MinSizeDigit)}";
        }

        private void CandleProvider_CandleLoaded(object sender, EventArgs e)
        {
            if (!isStarted)
                return;

            StartEmulation(sender as ICandleProvider);
        }

        IEnumerator<IYieldInstruction> CandleStart()
        {
            foreach(var s in candles)
            {
                if (!isStarted)
                    yield break;
                yield return Coroutine.StartCoroutine(TickStart(s));
            }
        }

        private Candle GenerateCandle(decimal openPrice, decimal closePrice)
        {
            Candle candle = new Candle();
            candle.Open = openPrice; 
            return candle;
        }

        IEnumerator<IYieldInstruction> TickStart(Candle candle)
        {
            int step = 0;
            decimal price = candle.Open, ask, bid;
            bool isUp = candle.Close > candle.Open;
            decimal tickStep;
            newCandle.Open = candle.Open;
            newCandle.Close = candle.Open;
            newCandle.High = candle.Open;
            newCandle.Low = candle.Open;
            newCandle.Time = candle.Time;
            newCandle.Timestamp = candle.Timestamp;

            while (step < 3)
            {
                tickStep = tickSize * speed * 10;

                switch (step)
                {
                    case 0:
                        price += isUp ? -tickStep : tickStep;
                        if (price <= candle.Low || price >= candle.High)
                        {
                            price = Math.Min(candle.High, Math.Max(candle.Low, price));
                            step ++;
                        }
                        break;
                    case 1:
                        price += isUp ? tickStep : -tickStep;
                        if (price <= candle.Low || price >= candle.High)
                        {
                            price = Math.Min(candle.High, Math.Max(candle.Low, price));
                            step++;
                        }
                        break;
                    case 2:
                        price += isUp ? -tickStep : tickStep;
                        if (isUp)
                        {
                            if (price < candle.Close)
                            {
                                price = candle.Close;
                                step = 3;
                            }
                        }
                        else
                        {
                            if (price > candle.Close)
                            {
                                price = candle.Close;
                                step = 3;
                            }
                        }
                        break;
                }

                ask = price + tickSize;
                bid = price;

                newCandle.Time = candle.Time;
                newCandle.Low = Math.Min(newCandle.Low, price);
                newCandle.High = Math.Max(newCandle.High, price);

                newCandle.Close = price;

                this.candleView1.UpdateLast(newCandle);

                if (runtime != null)
                    runtime.UpdatePrices(ask, bid, candle.Time);//TODO

                wait.Reset();
                if (!isStarted)
                    yield break;
                yield return wait;
            }
        }

        private void cmbSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbSpeed.SelectedIndex;
            this.speed = (int)Math.Pow(2, index);
        }

        private void WinEmulator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(strategyList != null)
            {
                foreach(var s in strategyList)
                {
                    s.Dispose();
                }
                strategyList = null;

                if(dataProvider != null)
                    DataProviderManager.Instance.ReleaseProvider(dataProvider);
                dataProvider = null;
            }

            runtime = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(isStarted)
            {
                UpdateOrderView();
                UpdateRecordView();
                UpdateBalance();
            }
        }

        private void lvHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
