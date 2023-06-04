namespace CoinTrader.Forms
{
    partial class WinEmulator
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
            this.cmbCandle = new System.Windows.Forms.ComboBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSpeed = new System.Windows.Forms.ComboBox();
            this.lblStartQuote = new System.Windows.Forms.Label();
            this.txtFunds = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvHistory = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.datetime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.currency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.side = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fillSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceAvg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.update = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBaseBalance = new System.Windows.Forms.Label();
            this.lblBaseBalanceStr = new System.Windows.Forms.Label();
            this.lblQuoteBalance = new System.Windows.Forms.Label();
            this.lblQuoteBalanceStr = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvOrders = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFee = new System.Windows.Forms.ComboBox();
            this.candleView1 = new CoinTrader.Forms.Control.CandleView();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbCandle
            // 
            this.cmbCandle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCandle.FormattingEnabled = true;
            this.cmbCandle.Items.AddRange(new object[] {
            "1日",
            "4小时",
            "1小时",
            "15分钟",
            "5分钟"});
            this.cmbCandle.Location = new System.Drawing.Point(114, 351);
            this.cmbCandle.Name = "cmbCandle";
            this.cmbCandle.Size = new System.Drawing.Size(295, 29);
            this.cmbCandle.TabIndex = 1;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(230, 506);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(197, 69);
            this.btnStartStop.TabIndex = 2;
            this.btnStartStop.Text = "开始";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 355);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "K线";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 450);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "速度";
            // 
            // cmbSpeed
            // 
            this.cmbSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpeed.FormattingEnabled = true;
            this.cmbSpeed.Items.AddRange(new object[] {
            "1x",
            "2x",
            "4x",
            "8x",
            "16x",
            "32x"});
            this.cmbSpeed.Location = new System.Drawing.Point(114, 446);
            this.cmbSpeed.Name = "cmbSpeed";
            this.cmbSpeed.Size = new System.Drawing.Size(121, 29);
            this.cmbSpeed.TabIndex = 7;
            this.cmbSpeed.SelectedIndexChanged += new System.EventHandler(this.cmbSpeed_SelectedIndexChanged);
            // 
            // lblStartQuote
            // 
            this.lblStartQuote.AutoSize = true;
            this.lblStartQuote.Location = new System.Drawing.Point(13, 299);
            this.lblStartQuote.Name = "lblStartQuote";
            this.lblStartQuote.Size = new System.Drawing.Size(94, 21);
            this.lblStartQuote.TabIndex = 8;
            this.lblStartQuote.Text = "起始资金";
            // 
            // txtFunds
            // 
            this.txtFunds.Location = new System.Drawing.Point(114, 294);
            this.txtFunds.Name = "txtFunds";
            this.txtFunds.Size = new System.Drawing.Size(294, 31);
            this.txtFunds.TabIndex = 9;
            this.txtFunds.Text = "10000";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(69, 11);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(541, 165);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lvHistory);
            this.groupBox1.Location = new System.Drawing.Point(0, 607);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1262, 397);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "交易记录";
            // 
            // lvHistory
            // 
            this.lvHistory.CheckBoxes = true;
            this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.datetime,
            this.currency,
            this.side,
            this.size,
            this.price,
            this.fillSize,
            this.priceAvg,
            this.update,
            this.fee});
            this.lvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvHistory.FullRowSelect = true;
            this.lvHistory.HideSelection = false;
            this.lvHistory.Location = new System.Drawing.Point(3, 27);
            this.lvHistory.Margin = new System.Windows.Forms.Padding(4);
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(1256, 367);
            this.lvHistory.TabIndex = 1;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            this.lvHistory.SelectedIndexChanged += new System.EventHandler(this.lvHistory_SelectedIndexChanged);
            // 
            // id
            // 
            this.id.Text = "ID";
            this.id.Width = 165;
            // 
            // datetime
            // 
            this.datetime.Text = "下单时间";
            this.datetime.Width = 225;
            // 
            // currency
            // 
            this.currency.Text = "币种";
            this.currency.Width = 212;
            // 
            // side
            // 
            this.side.Text = "方向";
            this.side.Width = 85;
            // 
            // size
            // 
            this.size.Text = "数量";
            this.size.Width = 136;
            // 
            // price
            // 
            this.price.Text = "挂单价格";
            this.price.Width = 145;
            // 
            // fillSize
            // 
            this.fillSize.DisplayIndex = 7;
            this.fillSize.Text = "成交数量";
            this.fillSize.Width = 205;
            // 
            // priceAvg
            // 
            this.priceAvg.DisplayIndex = 6;
            this.priceAvg.Text = "成交均价";
            this.priceAvg.Width = 140;
            // 
            // update
            // 
            this.update.Text = "最后成交";
            this.update.Width = 192;
            // 
            // fee
            // 
            this.fee.Text = "手续费";
            this.fee.Width = 138;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblBaseBalance);
            this.panel1.Controls.Add(this.lblBaseBalanceStr);
            this.panel1.Controls.Add(this.lblQuoteBalance);
            this.panel1.Controls.Add(this.lblQuoteBalanceStr);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.cmbCandle);
            this.panel1.Controls.Add(this.btnStartStop);
            this.panel1.Controls.Add(this.txtFunds);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblStartQuote);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbFee);
            this.panel1.Controls.Add(this.cmbSpeed);
            this.panel1.Location = new System.Drawing.Point(1268, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 1012);
            this.panel1.TabIndex = 12;
            // 
            // lblBaseBalance
            // 
            this.lblBaseBalance.AutoSize = true;
            this.lblBaseBalance.Location = new System.Drawing.Point(108, 237);
            this.lblBaseBalance.Name = "lblBaseBalance";
            this.lblBaseBalance.Size = new System.Drawing.Size(76, 21);
            this.lblBaseBalance.TabIndex = 13;
            this.lblBaseBalance.Text = "label4";
            // 
            // lblBaseBalanceStr
            // 
            this.lblBaseBalanceStr.AutoSize = true;
            this.lblBaseBalanceStr.Location = new System.Drawing.Point(13, 237);
            this.lblBaseBalanceStr.Name = "lblBaseBalanceStr";
            this.lblBaseBalanceStr.Size = new System.Drawing.Size(76, 21);
            this.lblBaseBalanceStr.TabIndex = 13;
            this.lblBaseBalanceStr.Text = "label4";
            // 
            // lblQuoteBalance
            // 
            this.lblQuoteBalance.AutoSize = true;
            this.lblQuoteBalance.Location = new System.Drawing.Point(110, 192);
            this.lblQuoteBalance.Name = "lblQuoteBalance";
            this.lblQuoteBalance.Size = new System.Drawing.Size(76, 21);
            this.lblQuoteBalance.TabIndex = 12;
            this.lblQuoteBalance.Text = "label1";
            // 
            // lblQuoteBalanceStr
            // 
            this.lblQuoteBalanceStr.AutoSize = true;
            this.lblQuoteBalanceStr.Location = new System.Drawing.Point(13, 192);
            this.lblQuoteBalanceStr.Name = "lblQuoteBalanceStr";
            this.lblQuoteBalanceStr.Size = new System.Drawing.Size(76, 21);
            this.lblQuoteBalanceStr.TabIndex = 12;
            this.lblQuoteBalanceStr.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lvOrders);
            this.groupBox2.Location = new System.Drawing.Point(3, 606);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(610, 397);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "订单";
            // 
            // lvOrders
            // 
            this.lvOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvOrders.HideSelection = false;
            this.lvOrders.Location = new System.Drawing.Point(3, 27);
            this.lvOrders.Name = "lvOrders";
            this.lvOrders.Size = new System.Drawing.Size(604, 367);
            this.lvOrders.TabIndex = 0;
            this.lvOrders.UseCompatibleStateImageBehavior = false;
            this.lvOrders.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "币种";
            this.columnHeader1.Width = 169;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "方向";
            this.columnHeader2.Width = 93;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "价格";
            this.columnHeader3.Width = 160;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "金额";
            this.columnHeader4.Width = 168;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 403);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 402);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "佣金";
            // 
            // cmbFee
            // 
            this.cmbFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFee.FormattingEnabled = true;
            this.cmbFee.Items.AddRange(new object[] {
            "0.1",
            "0.08",
            "0.06",
            "0.04",
            "0.02",
            "0"});
            this.cmbFee.Location = new System.Drawing.Point(114, 398);
            this.cmbFee.Name = "cmbFee";
            this.cmbFee.Size = new System.Drawing.Size(121, 29);
            this.cmbFee.TabIndex = 7;
            this.cmbFee.SelectedIndexChanged += new System.EventHandler(this.cmbSpeed_SelectedIndexChanged);
            // 
            // candleView1
            // 
            this.candleView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.candleView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.candleView1.Location = new System.Drawing.Point(0, 1);
            this.candleView1.Name = "candleView1";
            this.candleView1.Size = new System.Drawing.Size(1262, 600);
            this.candleView1.TabIndex = 0;
            // 
            // WinEmulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1896, 1016);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.candleView1);
            this.Name = "WinEmulator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinEmulator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinEmulator_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Control.CandleView candleView1;
        private System.Windows.Forms.ComboBox cmbCandle;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSpeed;
        private System.Windows.Forms.Label lblStartQuote;
        private System.Windows.Forms.TextBox txtFunds;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvHistory;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader datetime;
        private System.Windows.Forms.ColumnHeader currency;
        private System.Windows.Forms.ColumnHeader side;
        private System.Windows.Forms.ColumnHeader size;
        private System.Windows.Forms.ColumnHeader price;
        private System.Windows.Forms.ColumnHeader fillSize;
        private System.Windows.Forms.ColumnHeader priceAvg;
        private System.Windows.Forms.ColumnHeader update;
        private System.Windows.Forms.ColumnHeader fee;
        private System.Windows.Forms.ListView lvOrders;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label lblBaseBalance;
        private System.Windows.Forms.Label lblBaseBalanceStr;
        private System.Windows.Forms.Label lblQuoteBalance;
        private System.Windows.Forms.Label lblQuoteBalanceStr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFee;
        private System.Windows.Forms.Label label4;
    }
}