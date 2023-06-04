
using CoinTrader.Common.Classes;
using CoinTrader.Common.Util;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.Monitor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CoinTrader.Forms
{

    struct ChartPoint
    {
        public int X;
        public float Y;
    }
    public partial class WinCross : Form
    {
        MarketDataProvider provider1 = null;
        MarketDataProvider provider2 = null;

        CandleMonitor candleMonitor1 = null;
        CandleMonitor candleMonitor2 = null;
        bool swapped = false;

        Point leftCmbPoint;
        Point rightCmbPoint;

        List<Candle> candleList1 = new List<Candle>();
        List<Candle> candleList2 = new List<Candle>();
        List<Candle> candleList3 = new List<Candle>();

        CandleGranularity granularity = CandleGranularity.D1;

        int waitCandleLoadCount = 2;

        Dictionary<string, MarketDataProvider> cachedProviders = new Dictionary<string, MarketDataProvider>();

        string[] MainCoins = { "BTC", "ETH"};
        public WinCross()
        {
            InitializeComponent();
        }

        private void WinCross_Load(object sender, EventArgs e)
        {
            IList<EnumField> fileds = EnumField.GetEnumFields(typeof(CandleGranularity));
            this.cmbGranularity.Items.AddRange(fileds.ToArray());

            string d1 = granularity.ToString();

            for(int i = 0;i< fileds.Count; i++)
            {
                if (fileds[i].ValueName == d1)
                {
                    this.cmbGranularity.SelectedIndex = i;
                    break;
                }
            }

             
            for (int i = 0; i < MainCoins.Length; i++ )
            {
                var inst = string.Format("{0}-{1}", MainCoins[i], Config.Instance.UsdCoin);
                MainCoins[i] = inst;
            }

            cmbCoin1.Items.AddRange(MainCoins);
            var instList = InstrumentManager.SpotInstrument.GetAllInstrument();

            foreach (var inst in instList)
            {
                if (!MainCoins.Contains(inst.Value.InstrumentId))
                {
                    if (string.Compare(Config.Instance.UsdCoin, inst.Value.QuoteCcy, true) == 0)
                    {
                        cmbCoin2.Items.Add(inst.Value.InstrumentId);
                    }
                }
            }

            if (cmbCoin2.Items.Count > 0) cmbCoin2.SelectedIndex = 0;
            if (cmbCoin1.Items.Count > 0) cmbCoin1.SelectedIndex = 0;

            leftCmbPoint = cmbCoin1.Location;
            rightCmbPoint = cmbCoin2.Location;
        }

        List<ChartPoint> ampList = new List<ChartPoint>(300);
        private void Analysis(IList<Candle> candles)
        {
            int upCount = 0;
            int downCount = 0;
            double maxUpAmp = 0;
            double maxDownAmp = 0;
            double avgUpAmp = 0;
            double avgDownAmp = 0;
            double totalUpAmp = 0;
            double totalDownAmp = 0;
            float upRate = 0;
            float downRate = 0;

            ampList.Clear();

            int index = 0;
            var vec2 = default(ChartPoint);
            var count = candles.Count;
            foreach (var candle in candles)
            {
                var amp = Convert.ToDouble((candle.High - candle.Low) / candle.Low);
                vec2.X = count - index;
                if (candle.Close > candle.Open)
                {
                    totalUpAmp += amp;
                    maxUpAmp = Math.Max(amp, maxUpAmp);
                    upCount++;
                    vec2.Y = (float) amp;
                   
                }
                else if (candle.Close < candle.Open)
                {
                    totalDownAmp += amp;
                    maxDownAmp = Math.Max(amp, maxDownAmp);
                    downCount++;
                    vec2.Y = (float) -amp;
                }
                ampList.Add(vec2);

                index++;
            }

            if (totalUpAmp > 0)
            {
                avgUpAmp = totalUpAmp / upCount;
            }

            if (totalDownAmp > 0)
            {
                avgDownAmp = totalDownAmp / downCount;
            }

            var total = upCount + downCount;
            if (total > 0)
            {
                upRate = 1.0f * upCount / total;
                downRate = 1.0f * downCount / total;
            }

            Action action = () =>
            {
                label2.Text = $"上涨数:{upCount}";
                label3.Text = $"下跌数:{downCount}";
                label4.Text = $"上涨概率:{upRate:p}";
                label5.Text = $"下跌概率:{downRate:p}";
                label6.Text = $"平均上涨振幅:{avgUpAmp:p}";
                label7.Text = $"平均下跌振幅:{avgDownAmp:p}";
                label8.Text = $"最大上涨振幅:{maxUpAmp:p}";
                label9.Text = $"最大下跌振幅:{maxDownAmp:p}";

                var points = this.chart1.Series[0].Points;
                points.Clear();
                foreach (var p in ampList)
                {
                    points.AddXY(p.X,p.Y);
                }
            };

            if(this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private void cmbCoin1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OnChangeProvider()
        {
            if (provider1 != null)
            {
                DataProviderManager.Instance.ReleaseProvider(provider1);
            }

            if(provider2 != null)
            {
                DataProviderManager.Instance.ReleaseProvider(provider2);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var coin1 = this.cmbCoin1.Text;
            var coin2 = this.cmbCoin2.Text;

            if(string.IsNullOrEmpty(coin1) || string.IsNullOrEmpty(coin2))
            { return; }

            waitCandleLoadCount = 2;

            if (!cachedProviders.TryGetValue(coin1, out provider1))
            {
                provider1 = DataProviderManager.Instance.GetProvider(coin1);
                cachedProviders.Add(coin1, provider1);
            }

            if (!cachedProviders.TryGetValue(coin2, out provider2))
            {
                provider2 = DataProviderManager.Instance.GetProvider(coin2);
                cachedProviders.Add(coin2, provider2);
            }

            if (candleMonitor1 != null) candleMonitor1.CandleLoaded -= this.OnCandleLoadded;
            if(candleMonitor2 != null) candleMonitor2.CandleLoaded -= this.OnCandleLoadded;

            candleMonitor1 = provider1.LoadCandle(granularity);
            candleMonitor1.CandleLoaded += OnCandleLoadded;
            if (candleMonitor1.Loaded) waitCandleLoadCount--;

            candleMonitor2 = provider2.LoadCandle(granularity);
            if (candleMonitor2.Loaded) waitCandleLoadCount--;
            candleMonitor2.CandleLoaded += OnCandleLoadded;

            TryBlendCandle();
        }

        private void OnCandleLoadded(object sender, EventArgs e)
        {
            waitCandleLoadCount--;
            TryBlendCandle();
        }

        private void LoadMoreData()
        {

        }

        private void TryBlendCandle()
        {
            var pool = Pool<Candle>.GetPool();

            pool.Put(candleList1); candleList1.Clear();
            pool.Put(candleList2); candleList2.Clear();
            pool.Put(candleList3); candleList3.Clear();

            if (waitCandleLoadCount == 0)
            {
                candleMonitor1.EachCandle((c) =>
                {
                    var candle = pool.Get();
                    c.CopyTo(candle);
                    if (swapped)
                        candleList2.Add(candle);
                    else
                        candleList1.Add(candle);
                    return false;
                });

                candleMonitor2.EachCandle((c) =>
                {
                    var candle = pool.Get();
                    c.CopyTo(candle);
                    if (swapped)
                        candleList1.Add(candle);
                    else
                        candleList2.Add(candle);
                    return false;
                });

                foreach (var candle1 in candleList1)
                {
                    while (candleList2.Count > 0)
                    {
                        int i = 0;
                        var candle2 = candleList2[i];

                        if (candle2.Time < candle1.Time)
                            break;
                        else if (candle2.Time == candle1.Time)
                        {
                            var candle3 = pool.Get();
                            candle3.Time    = candle1.Time;
                            candle3.Open    = candle1.Open / candle2.Open;
                            candle3.Close   = candle1.Close / candle2.Close;
                            candle3.Low     = candle1.Low / candle2.Low;
                            candle3.High    = candle1.High / candle2.High;
                            candle3.Volume  = candle2.Volume;

                            if(candle3.High < candle3.Low)
                            {
                                var a = candle3.High;
                                candle3.High = candle3.Low;
                                candle3.Low = a;
                            }

                            candleList3.Add(candle3);
                            
                            //candle3.
                        }

                        pool.Put(candle2);
                        candleList2.RemoveAt(i);
                    }
                }

                Analysis(candleList3);

                if(candleList3.Count > 0)
                {
                    var p = candleList3[0].Open;
                    var d = p >1.0m? 4 - Math.Log10((double)p): Math.Round( 2 - Math.Log10((double)p));

                    this.candleView1.SetPriceVolumePrecision((int)d, 1);
                }
                this.candleView1.SetCandleData(candleList3);

            }
        }
        private void WinCross_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (candleMonitor1 != null) candleMonitor1.CandleLoaded -= this.OnCandleLoadded;
            if (candleMonitor2 != null) candleMonitor2.CandleLoaded -= this.OnCandleLoadded;

            foreach (var kv in cachedProviders)
            {
                DataProviderManager.Instance.ReleaseProvider(kv.Value);
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            swapped = !swapped;
            if(swapped)
            {
                cmbCoin2.Location = leftCmbPoint; 
                cmbCoin1.Location = rightCmbPoint; 
            }
            else
            {
                cmbCoin2.Location = rightCmbPoint;
                cmbCoin1.Location = leftCmbPoint;
            }

            btnRefresh_Click(null,null); 
        }

        private void cmbGranularity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cmbGranularity.SelectedItem as EnumField;   
            granularity = (CandleGranularity)Enum.Parse(typeof(CandleGranularity), item.ValueName);
            btnRefresh_Click(null, null);
        }
    }
}
