using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoinTrader.Forms.Control
{
    public partial class TickView : UserControl
    {
        private const int PointCount = 20;
        private List<decimal> ticks = new List<decimal>();
        private Graphics graphics = null;
        private Font chartFont = null;
        private RectangleF rectDot = new Rectangle(0,0,4,4);
        private Pen curvePen = null;
        private Pen gridPen = null;
        private PointF[] points;
        private bool showChart = false;
        private decimal ask;
        private decimal bid;

        private int sizeTotal = 0;
        private int eraseTotal = 0;
        public TickView()
        {
            InitializeComponent();
            this.pnlChart.Paint += PnlChart_Paint;
        }



        public void SetInstrumentId()
        {

        }

        private void ReleaseGraphics()
        {
            curvePen.Dispose();
            chartFont.Dispose();
            graphics.Dispose();
            gridPen.Dispose();

            curvePen = null;
            chartFont = null;
            graphics = null;
            gridPen = null;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent == null && graphics != null)
            {
                ReleaseGraphics();
            }
            base.OnParentChanged(e);
        }

        private void PnlChart_Paint(object sender, PaintEventArgs e)
        {
            if(this.showChart)
            {
                this.DrawChart();
            }
        }

        private double lerp(double a,double b, double t)
        {
            return (b-a) * t + a;
        }
  

        private void DrawChart()
        {
            if (graphics == null)
            {
                graphics = Graphics.FromHwnd(this.pnlChart.Handle);
                curvePen = new Pen(Color.FromArgb(0, 255, 0));
                chartFont = new Font("宋体", 9);
                gridPen = new Pen(Color.Green, 1);
                gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            }

            graphics.Clear( Color.Black);

            #region Draw grid
            float GridSize = this.Width * (1.0f / 10.0f);

            float yOffset = (Height % GridSize) * 0.5f;

            for (var y = yOffset; y < this.Height; y += GridSize)
            {
                graphics.DrawLine(gridPen, 0, y, Width, y);
            }

            for (var x = GridSize; x < this.Width; x += GridSize)
            {
                graphics.DrawLine(gridPen, x, 0, x, Height);
            }

            graphics.DrawRectangle(curvePen, 0, 0, Width - 1, Height - 1);
            #endregion

            #region Draw price curve

            if (ticks.Count > 1)
            {
                decimal max = 0;
                decimal min = decimal.MaxValue;
                decimal total = 0;
                decimal avg;
                foreach (var p in ticks)
                {
                    total += p;
                    max = Math.Max(p, max);
                    min = Math.Min(p, min);
                }

                avg = total / ticks.Count;

                var maxAmp = Math.Max(0.002f, (float)(max / min) - 1.0f);

                const int padding = 1;
                points = new PointF[ticks.Count];
                var point = default(PointF);
                var amp = 0.0f;
                for (int i = 0; i < ticks.Count; i++)
                {
                    point.X = this.Width * (1.0f / PointCount) * i;
                    amp = (float)((ticks[i] / min) - 1.0m) / maxAmp;
                    point.Y = (this.Height - padding * 2) * (1.0f - amp) + padding;
                    points[i] = point;
                }

                rectDot.X = points[points.Length - 1].X - rectDot.Width * 0.5f;
                rectDot.Y = points[points.Length - 1].Y - rectDot.Height * 0.5f;
                graphics.DrawCurve(curvePen, points);
                graphics.FillEllipse(Brushes.Red, rectDot);
            }

            #endregion

            graphics.DrawString(bid.ToString(), chartFont, Brushes.Yellow, default(PointF));
            graphics.Save();
        }

        public void ShowTickerPrice(decimal ask, decimal bid)
        {
            Color colorUp = Color.Green;
            Color colorDown = Color.Red;

            bool priceChanged = ask != this.ask || bid != this.bid;
            Color priceColor = ask > this.ask ? colorUp : colorDown;

            this.ask = ask;
            this.bid = bid;

            if (!showChart)
            {
                this.lblAskPrice.Text = ask.ToString();
                this.lblBidPrice.Text = bid.ToString();

                if (priceChanged)
                {
                    this.lblAskPrice.ForeColor = priceColor;
                    this.lblBidPrice.ForeColor = priceColor;
                }
            }
        }

        public void ShowTickerPrice(string ask, string bid)
        {
            decimal numAsk = decimal.Parse(ask);
            decimal numBid = decimal.Parse(bid);

            this.ShowTickerPrice(numAsk,numBid);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ticks.Count >= PointCount)
                ticks.RemoveAt(0);

            ticks.Add(bid);
            DrawChart();
        }

        private void TickView_Click(object sender, EventArgs e)
        {
            this.showChart = !this.showChart;
            this.label17.Visible =  
            this.label2.Visible =  
            this.label7.Visible =  
            this.lblAskPrice.Visible =  
            this.lblBidPrice.Visible = !this.showChart;

            this.pnlChart.Visible = showChart;
            this.timer1.Enabled = showChart;

            if(!showChart && graphics != null)
            {
                ReleaseGraphics();
            }

            if(showChart)
            {
                this.DrawChart();
            }
        }
    }
}
