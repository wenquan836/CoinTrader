namespace CoinTrader.Forms.Control
{
    partial class TickView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label17 = new System.Windows.Forms.Label();
            this.lblAskPrice = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBidPrice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlChart = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 45);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 21);
            this.label17.TabIndex = 51;
            this.label17.Text = "买入";
            this.label17.Click += new System.EventHandler(this.TickView_Click);
            // 
            // lblAskPrice
            // 
            this.lblAskPrice.AutoSize = true;
            this.lblAskPrice.Location = new System.Drawing.Point(68, 6);
            this.lblAskPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAskPrice.Name = "lblAskPrice";
            this.lblAskPrice.Size = new System.Drawing.Size(21, 21);
            this.lblAskPrice.TabIndex = 53;
            this.lblAskPrice.Text = "0";
            this.lblAskPrice.Click += new System.EventHandler(this.TickView_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 21);
            this.label7.TabIndex = 50;
            this.label7.Text = "卖出";
            this.label7.Click += new System.EventHandler(this.TickView_Click);
            // 
            // lblBidPrice
            // 
            this.lblBidPrice.AutoSize = true;
            this.lblBidPrice.Location = new System.Drawing.Point(68, 45);
            this.lblBidPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBidPrice.Name = "lblBidPrice";
            this.lblBidPrice.Size = new System.Drawing.Size(21, 21);
            this.lblBidPrice.TabIndex = 52;
            this.lblBidPrice.Text = "0";
            this.lblBidPrice.Click += new System.EventHandler(this.TickView_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(4, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 21);
            this.label2.TabIndex = 54;
            this.label2.Text = "_____________________";
            this.label2.Click += new System.EventHandler(this.TickView_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnlChart
            // 
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(0, 0);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(261, 72);
            this.pnlChart.TabIndex = 55;
            this.pnlChart.Visible = false;
            this.pnlChart.Click += new System.EventHandler(this.TickView_Click);
            // 
            // TickView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lblAskPrice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblBidPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlChart);
            this.Name = "TickView";
            this.Size = new System.Drawing.Size(261, 72);
            this.Click += new System.EventHandler(this.TickView_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblAskPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBidPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlChart;
    }
}
