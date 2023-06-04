using CefSharp;
using CefSharp.WinForms;
using CoinTrader.Common;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.Common.Util;
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

namespace CoinTrader.Forms.Control
{
    public partial class CandleView : UserControl
    {

        IList<Candle> candles;
        bool webPageLoadded = false;
        private ChromiumWebBrowser webView = null;


        public CandleView()
        {
            InitializeComponent();
        }

        private void CandleView_Load(object sender, EventArgs e)
        {
            try
            {
                webView = new ChromiumWebBrowser();

                string webPath = "file://" + Path.Combine(Application.StartupPath, "Res/Html/TradeView.html");
                webView.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;

                webView.Dock = DockStyle.Fill;
                this.Controls.Add(webView);
                webView.FrameLoadEnd += ChromiumWebBrowser1_FrameLoadEnd;
                webView.Load(webPath);

                ParentForm.FormClosing += ParentForm_FormClosing;
                ParentForm.FormClosed += ParentForm_FormClosed;
            }
            catch
            {

            }
        }

        private void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (webView != null)
            {
                webView.Dispose();
                webView = null;
            }
        }

        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ChromiumWebBrowser1_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            var jo = webView.JavascriptObjectRepository;
            jo.Register("app",new JsCandleProvider(),false);

            webPageLoadded = true;
            SetToWeb();
        }

        /// <summary>
        /// 添加更多数据
        /// </summary>
        /// <param name="data"></param>
        public void ApplyMoreData(IList<Candle> cnadles, bool hasMore)
        {
            StringBuilder sb = new StringBuilder();

            GenarateDataJsCode(candles, true, ref sb);
            sb.Insert(0, "applyMoreData(");
            sb.AppendFormat(",{0})",hasMore);
            ExecuteJavascript(sb.ToString());
            this.candles.Clear();
            this.candles = null;
        }


        public void SetCandleData(IList<Candle> candleData)
        {
            this.candles = candleData;
            SetToWeb();
        }

        /// <summary>
        /// 更新最后的K线价格
        /// </summary>
        /// <param name="last"></param>
        public void UpdateLast(Candle last)
        {
            TimeZone zone = TimeZone.CurrentTimeZone;
            string s = string.Format("{{open:{0},close:{1},low:{2},high:{3},timestamp:{4},volume:{5}}}", last.Open, last.Close, last.Low, last.High, DateUtil.GetTimestampMS(zone.ToLocalTime(last.Time)), last.Volume);
            s = string.Format("updateData({0})", s);
            ExecuteJavascript(s);
        }

        public void SetPriceVolumePrecision(int pricePrec, int volPrec)
        {
            string code = string.Format("setPriceVolumePrecision({0}, {1});", pricePrec, volPrec);
            ExecuteJavascript(code);
        }

        private void GenarateDataJsCode(IList<Candle> candles, bool reverse,ref StringBuilder sb)
        {
            TimeZone zone = TimeZone.CurrentTimeZone;
            IEnumerable<Candle> list =  reverse ? candles.Reverse(): candles;

            foreach (var candle in list)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.AppendFormat("{{open:{0},close:{1},low:{2},high:{3},timestamp:{4},volume:{5}}}", candle.Open, candle.Close, candle.Low, candle.High, DateUtil.GetTimestampMS(zone.ToLocalTime(candle.Time)), candle.Volume);
            }

            sb.Insert(0, "[");
            sb.Append("]");
        }

        private void SetToWeb()
        {
            if (this.candles == null || !webPageLoadded)
                return;

            StringBuilder sb = new StringBuilder();

            GenarateDataJsCode(candles,true, ref sb);
            sb.Insert(0,"loadData(");
            sb.Append(");");
            ExecuteJavascript(sb.ToString());
            this.candles = null;
        }

         
        private void ExecuteJavascript(string code)
        {
            Action<string> invokeAction = async (js) => {

                if (webView == null)
                    return;

                var frame = webView.GetBrowser().MainFrame;

                if (frame == null)
                    return;

                var ret = await frame.EvaluateScriptAsync(js);
            };

            if(this.InvokeRequired)
            {
                BeginInvoke(invokeAction, code);
            }
            else
            {
                invokeAction(code);
            }
        }
        private void CandleView_ParentChanged(object sender, EventArgs e)
        {
            if(Parent == null)
            {
                
            }
        }
    }

    public class JsCandleProvider
    {
        public void LoadMore(long timestamp)
        {
            Console.Write("app int");
        }
    }

}
