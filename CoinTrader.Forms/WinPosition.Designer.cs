namespace CoinTrader.Forms
{
    partial class WinPosition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.swapInfoView1 = new CoinTrader.Forms.Control.SwapInfoView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMargin = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.instId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.posSide = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.avgPx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.upl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lever = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.liqPx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.margin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mgnRatio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mmr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.interest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.swapInfoView1);
            this.groupBox1.Location = new System.Drawing.Point(1694, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 1124);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // swapInfoView1
            // 
            this.swapInfoView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.swapInfoView1.Location = new System.Drawing.Point(6, 12);
            this.swapInfoView1.Name = "swapInfoView1";
            this.swapInfoView1.Size = new System.Drawing.Size(606, 261);
            this.swapInfoView1.TabIndex = 0;
            this.swapInfoView1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblMargin);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblTotalProfit);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(0, 957);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1688, 167);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // lblMargin
            // 
            this.lblMargin.AutoSize = true;
            this.lblMargin.Location = new System.Drawing.Point(153, 106);
            this.lblMargin.Name = "lblMargin";
            this.lblMargin.Size = new System.Drawing.Size(21, 21);
            this.lblMargin.TabIndex = 1;
            this.lblMargin.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "总保证金";
            // 
            // lblTotalProfit
            // 
            this.lblTotalProfit.AutoSize = true;
            this.lblTotalProfit.Location = new System.Drawing.Point(153, 55);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(21, 21);
            this.lblTotalProfit.TabIndex = 1;
            this.lblTotalProfit.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "总盈利";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.instId,
            this.posSide,
            this.pos,
            this.avgPx,
            this.upl,
            this.lever,
            this.liqPx,
            this.margin,
            this.mgnRatio,
            this.mmr,
            this.cTime,
            this.mode,
            this.interest});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1688, 951);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // instId
            // 
            this.instId.Text = "币种";
            this.instId.Width = 160;
            // 
            // posSide
            // 
            this.posSide.Text = "方向";
            this.posSide.Width = 80;
            // 
            // pos
            // 
            this.pos.Text = "数量";
            this.pos.Width = 97;
            // 
            // avgPx
            // 
            this.avgPx.Text = "开仓均价";
            this.avgPx.Width = 121;
            // 
            // upl
            // 
            this.upl.Text = "收益";
            this.upl.Width = 108;
            // 
            // lever
            // 
            this.lever.Text = "杠杆倍数";
            this.lever.Width = 123;
            // 
            // liqPx
            // 
            this.liqPx.Text = "预估强平价";
            this.liqPx.Width = 125;
            // 
            // margin
            // 
            this.margin.Text = "保证金";
            this.margin.Width = 129;
            // 
            // mgnRatio
            // 
            this.mgnRatio.Text = "保证金率";
            this.mgnRatio.Width = 150;
            // 
            // mmr
            // 
            this.mmr.Text = "维持保证金";
            this.mmr.Width = 130;
            // 
            // cTime
            // 
            this.cTime.Text = "建立时间";
            this.cTime.Width = 226;
            // 
            // mode
            // 
            this.mode.Text = "模式";
            this.mode.Width = 69;
            // 
            // interest
            // 
            this.interest.Text = "利息";
            this.interest.Width = 159;
            // 
            // WinPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2308, 1124);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "WinPosition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合约持仓管理";
            this.Load += new System.EventHandler(this.WinPosition_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Control.SwapInfoView swapInfoView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader instId;
        private System.Windows.Forms.ColumnHeader posSide;
        private System.Windows.Forms.ColumnHeader pos;
        private System.Windows.Forms.ColumnHeader avgPx;
        private System.Windows.Forms.ColumnHeader upl;
        private System.Windows.Forms.ColumnHeader lever;
        private System.Windows.Forms.ColumnHeader liqPx;
        private System.Windows.Forms.ColumnHeader margin;
        private System.Windows.Forms.ColumnHeader mgnRatio;
        private System.Windows.Forms.ColumnHeader mmr;
        private System.Windows.Forms.ColumnHeader cTime;
        private System.Windows.Forms.ColumnHeader mode;
        private System.Windows.Forms.ColumnHeader interest;
        private System.Windows.Forms.Label lblMargin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalProfit;
        private System.Windows.Forms.Label label1;
    }
}