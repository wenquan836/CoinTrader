using CefSharp.WinForms;

using CoinTrader.Common;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.Common.Util;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTrader.Forms.Chromium
{
    //    <!-- Github https://github.com/liihuu/KLineChart-->
    public partial class WebTradeView : Form
    {
        private bool isLoaded = false;

        MarketDataProvider  provider = null;
        CandleGranularity   granularity = CandleGranularity.H1;
        ICandleProvider     candleProvider = null;
        bool isLoadCandle = false;
        private string instId = "";

        public string InstId
        {
            set
            {
                if (string.Compare(value, instId, true) == 0)
                    return;

                instId = value;
                this.Text = instId.ToUpper();
                this.LoadChartData();
            }
        }

        ChromiumWebBrowser m_chromeBrowser = null;

        public WebTradeView()
        {
            InitializeComponent();

            m_chromeBrowser = new ChromiumWebBrowser();

            this.panel1.Controls.Add(m_chromeBrowser);

            string webPath = "file://" + Path.Combine(Application.StartupPath, "Res/Html/TradeView.html");
            //m_chromeBrowser.LoadingStateChanged += M_chromeBrowser_LoadingStateChanged;
            m_chromeBrowser.FrameLoadEnd += M_chromeBrowser_FrameLoadEnd; ;
            m_chromeBrowser.Dock = DockStyle.Fill;
            m_chromeBrowser.Load(webPath);
        }

        private void M_chromeBrowser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            //var host = m_chromeBrowser.GetBrowser().GetHost();


            isLoaded = true;

            if (!string.IsNullOrEmpty(this.instId))
            {
                LoadChartData();
            }
        }

        private void Clean()
        {
            if (this.candleProvider != null)
            {
                this.candleProvider.CandleLoaded -= CandleProvider_CandleLoaded;
                if(isLoadCandle)
                    provider.UnloadCandle(granularity);
                this.candleProvider = null;
            }

            if (this.provider != null)
            {
                this.provider.OnTick -= this.Provider_OnTick;
                DataProviderManager.Instance.ReleaseProvider(this.provider);
                this.provider = null;
            }
        }

        private  void LoadChartData()
        {
            if (!isLoaded)
                return;

            this.Clean();

            var provider = DataProviderManager.Instance.GetProvider(this.instId);

            if (provider != null)
            {
                this.provider = provider;
                provider.OnTick += Provider_OnTick;
                ICandleProvider candleProvider = null;

                var fileds = Enum.GetValues(typeof(CandleGranularity));

                foreach (var f in fileds)
                {
                    CandleGranularity cg = (CandleGranularity)f;
                    candleProvider = provider.GetCandleProvider(cg);

                    if (candleProvider != null)
                    {
                        granularity = cg;
                        break;
                    }
                }

                if (candleProvider == null)
                {
                    candleProvider = provider.LoadCandle(granularity);
                    this.isLoadCandle = true;
                }

                if (candleProvider != null)
                {
                    this.candleProvider = candleProvider;

                    if (candleProvider.Loaded)
                    {
                        var inst = InstrumentManager.GetInstrument(provider.InstrumentId);

                        SetToWeb(); 
                        if (inst != null)
                            SetPriceVolumePrecision(inst.TickSizeDigit, inst.MinSizeDigit);
                    }
                    else
                    {
                        candleProvider.CandleLoaded += CandleProvider_CandleLoaded;
                    }
                }
            }
        }

        private async void ExecuteJavascript(string code)
        {
            if (m_chromeBrowser == null)
                return;

            var frame = m_chromeBrowser.GetBrowser().MainFrame;

            if (frame == null) 
                return ;

            var ret = await frame.EvaluateScriptAsync(code);

            //return ret.Success;
        }

        private void SetPriceVolumePrecision(int pricePrec, int volPrec)
        {
            string code = string.Format("setPriceVolumePrecision({0}, {1});", pricePrec, volPrec);
            ExecuteJavascript(code);
        }

        private void CandleProvider_CandleLoaded(object sender, EventArgs e)
        {
            if(this.InvokeRequired)
            {
                Action act = SetToWeb;
                BeginInvoke(act);
                return;
            }

            this.SetToWeb();
        }

        private void SetToWeb()
        {
            StringBuilder sb = new StringBuilder();

            TimeZone zone = TimeZone.CurrentTimeZone;

            candleProvider.EachCandle((candle) =>
            {
                if (sb.Length > 0) sb.Append(",");
               
                sb.AppendFormat("{{open:{0},close:{1},low:{2},high:{3},timestamp:{4},volume:{5}}}", candle.Open,candle.Close,candle.Low,candle.High, DateUtil.GetTimestampMS(zone.ToLocalTime(candle.Time)), candle.Volume);

                return false;
            });

            sb.Insert(0, "loadData([");
            sb.Append("].reverse());");

            ExecuteJavascript(sb.ToString());
        }

        private void UpdateLast()
        {
            TimeZone zone = TimeZone.CurrentTimeZone;
            string s = "";
            candleProvider.EachCandle((candle) =>
            {
                s = string.Format("{{open:{0},close:{1},low:{2},high:{3},timestamp:{4},volume:{5}}}", candle.Open, candle.Close, candle.Low, candle.High, DateUtil.GetTimestampMS(zone.ToLocalTime(candle.Time)), candle.Volume);
                return true;
            });

            if (s.Length > 0)
            {
                s = string.Format("updateData({0})", s);
                ExecuteJavascript(s);
            }
        }

        bool priceChangeFlag = false;
        private void Provider_OnTick(decimal arg1, decimal arg2)
        {
            priceChangeFlag = true;
        }

        private void WebTradeView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Clean();
            if (m_chromeBrowser != null)
                m_chromeBrowser.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(priceChangeFlag)
            {
                UpdateLast();
                priceChangeFlag =false;
            }
        }
    }
}
