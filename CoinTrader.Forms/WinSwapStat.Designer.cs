namespace CoinTrader.Forms
{
    partial class WinSwapStat
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.datetime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.currency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.side = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fillSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceAvg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalBuy = new System.Windows.Forms.Label();
            this.lblTotalSell = new System.Windows.Forms.Label();
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBuyAvg = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSellAvg = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMaxBuy = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFee = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSellCount = new System.Windows.Forms.Label();
            this.lblBuyCount = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblWaiting = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbPage = new System.Windows.Forms.ComboBox();
            this.cmbResyncDays = new System.Windows.Forms.ComboBox();
            this.btnFromId = new System.Windows.Forms.Button();
            this.btnSelOnly = new System.Windows.Forms.Button();
            this.btnResync = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制IDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.datetime,
            this.currency,
            this.side,
            this.size,
            this.price,
            this.fillSize,
            this.priceAvg});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(17, 19);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1314, 731);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            this.listView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseMove);
            // 
            // id
            // 
            this.id.Text = "ID";
            this.id.Width = 240;
            // 
            // datetime
            // 
            this.datetime.Text = "下单时间";
            this.datetime.Width = 241;
            // 
            // currency
            // 
            this.currency.Text = "币种";
            this.currency.Width = 198;
            // 
            // side
            // 
            this.side.Text = "方向";
            this.side.Width = 89;
            // 
            // size
            // 
            this.size.Text = "数量";
            this.size.Width = 136;
            // 
            // price
            // 
            this.price.Text = "挂单价格";
            this.price.Width = 199;
            // 
            // fillSize
            // 
            this.fillSize.DisplayIndex = 7;
            this.fillSize.Text = "成交数量";
            this.fillSize.Width = 250;
            // 
            // priceAvg
            // 
            this.priceAvg.DisplayIndex = 6;
            this.priceAvg.Text = "成交均价";
            this.priceAvg.Width = 255;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "总买额";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "总卖额";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 496);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "总利润";
            // 
            // lblTotalBuy
            // 
            this.lblTotalBuy.AutoSize = true;
            this.lblTotalBuy.Location = new System.Drawing.Point(100, 16);
            this.lblTotalBuy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalBuy.Name = "lblTotalBuy";
            this.lblTotalBuy.Size = new System.Drawing.Size(22, 24);
            this.lblTotalBuy.TabIndex = 1;
            this.lblTotalBuy.Text = "0";
            // 
            // lblTotalSell
            // 
            this.lblTotalSell.AutoSize = true;
            this.lblTotalSell.Location = new System.Drawing.Point(100, 65);
            this.lblTotalSell.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalSell.Name = "lblTotalSell";
            this.lblTotalSell.Size = new System.Drawing.Size(22, 24);
            this.lblTotalSell.TabIndex = 1;
            this.lblTotalSell.Text = "0";
            // 
            // lblTotalProfit
            // 
            this.lblTotalProfit.AutoSize = true;
            this.lblTotalProfit.Location = new System.Drawing.Point(100, 496);
            this.lblTotalProfit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(22, 24);
            this.lblTotalProfit.TabIndex = 1;
            this.lblTotalProfit.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 213);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "买均价";
            // 
            // lblBuyAvg
            // 
            this.lblBuyAvg.AutoSize = true;
            this.lblBuyAvg.Location = new System.Drawing.Point(100, 213);
            this.lblBuyAvg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuyAvg.Name = "lblBuyAvg";
            this.lblBuyAvg.Size = new System.Drawing.Size(22, 24);
            this.lblBuyAvg.TabIndex = 1;
            this.lblBuyAvg.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 262);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 24);
            this.label6.TabIndex = 1;
            this.label6.Text = "卖均价";
            // 
            // lblSellAvg
            // 
            this.lblSellAvg.AutoSize = true;
            this.lblSellAvg.Location = new System.Drawing.Point(100, 262);
            this.lblSellAvg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSellAvg.Name = "lblSellAvg";
            this.lblSellAvg.Size = new System.Drawing.Size(22, 24);
            this.lblSellAvg.TabIndex = 1;
            this.lblSellAvg.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 391);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 24);
            this.label5.TabIndex = 1;
            this.label5.Text = "总结余";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(100, 391);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(22, 24);
            this.lblBalance.TabIndex = 1;
            this.lblBalance.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblMaxBuy);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblFee);
            this.panel1.Controls.Add(this.lblTotalProfit);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblSellAvg);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblBuyAvg);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblBalance);
            this.panel1.Controls.Add(this.lblSellCount);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblTotalSell);
            this.panel1.Controls.Add(this.lblBuyCount);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblTotalBuy);
            this.panel1.Location = new System.Drawing.Point(1341, 19);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 937);
            this.panel1.TabIndex = 2;
            // 
            // lblMaxBuy
            // 
            this.lblMaxBuy.AutoSize = true;
            this.lblMaxBuy.Location = new System.Drawing.Point(187, 310);
            this.lblMaxBuy.Name = "lblMaxBuy";
            this.lblMaxBuy.Size = new System.Drawing.Size(22, 24);
            this.lblMaxBuy.TabIndex = 3;
            this.lblMaxBuy.Text = "0";
            this.lblMaxBuy.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 310);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(166, 24);
            this.label9.TabIndex = 3;
            this.label9.Text = "最大资金占用:";
            this.label9.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 441);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 24);
            this.label7.TabIndex = 2;
            this.label7.Text = "总佣金";
            // 
            // lblFee
            // 
            this.lblFee.AutoSize = true;
            this.lblFee.Location = new System.Drawing.Point(100, 441);
            this.lblFee.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(22, 24);
            this.lblFee.TabIndex = 1;
            this.lblFee.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 114);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 24);
            this.label11.TabIndex = 1;
            this.label11.Text = "总买量";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 163);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 24);
            this.label10.TabIndex = 1;
            this.label10.Text = "总卖量";
            // 
            // lblSellCount
            // 
            this.lblSellCount.AutoSize = true;
            this.lblSellCount.Location = new System.Drawing.Point(100, 163);
            this.lblSellCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSellCount.Name = "lblSellCount";
            this.lblSellCount.Size = new System.Drawing.Size(22, 24);
            this.lblSellCount.TabIndex = 1;
            this.lblSellCount.Text = "0";
            // 
            // lblBuyCount
            // 
            this.lblBuyCount.AutoSize = true;
            this.lblBuyCount.Location = new System.Drawing.Point(100, 114);
            this.lblBuyCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuyCount.Name = "lblBuyCount";
            this.lblBuyCount.Size = new System.Drawing.Size(22, 24);
            this.lblBuyCount.TabIndex = 1;
            this.lblBuyCount.Text = "0";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(86, 15);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpStart.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(227, 35);
            this.dtpStart.TabIndex = 3;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(346, 15);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpEnd.MinDate = new System.DateTime(2021, 7, 3, 0, 0, 0, 0);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(227, 35);
            this.dtpEnd.TabIndex = 3;
            this.dtpEnd.Value = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(610, 9);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 51);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.lblWaiting);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.cmbPage);
            this.panel2.Controls.Add(this.cmbResyncDays);
            this.panel2.Controls.Add(this.btnFromId);
            this.panel2.Controls.Add(this.btnSelOnly);
            this.panel2.Controls.Add(this.dtpEnd);
            this.panel2.Controls.Add(this.btnResync);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.dtpStart);
            this.panel2.Controls.Add(this.txtId);
            this.panel2.Location = new System.Drawing.Point(17, 761);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1804, 176);
            this.panel2.TabIndex = 5;
            // 
            // lblWaiting
            // 
            this.lblWaiting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWaiting.AutoSize = true;
            this.lblWaiting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblWaiting.Location = new System.Drawing.Point(1564, 120);
            this.lblWaiting.Name = "lblWaiting";
            this.lblWaiting.Size = new System.Drawing.Size(226, 24);
            this.lblWaiting.TabIndex = 11;
            this.lblWaiting.Text = "正在同步数据......";
            this.lblWaiting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWaiting.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(316, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 24);
            this.label14.TabIndex = 9;
            this.label14.Text = "-";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1553, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 24);
            this.label8.TabIndex = 8;
            this.label8.Text = "页码";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(15, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 24);
            this.label13.TabIndex = 8;
            this.label13.Text = "日期";
            // 
            // cmbPage
            // 
            this.cmbPage.AccessibleName = "";
            this.cmbPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPage.FormattingEnabled = true;
            this.cmbPage.Location = new System.Drawing.Point(1633, 15);
            this.cmbPage.Name = "cmbPage";
            this.cmbPage.Size = new System.Drawing.Size(151, 32);
            this.cmbPage.TabIndex = 7;
            this.cmbPage.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cmbResyncDays
            // 
            this.cmbResyncDays.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmbResyncDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResyncDays.FormattingEnabled = true;
            this.cmbResyncDays.Items.AddRange(new object[] {
            "3天",
            "1周",
            "2周",
            "1月",
            "2月",
            "全部"});
            this.cmbResyncDays.Location = new System.Drawing.Point(1112, 19);
            this.cmbResyncDays.Name = "cmbResyncDays";
            this.cmbResyncDays.Size = new System.Drawing.Size(132, 32);
            this.cmbResyncDays.TabIndex = 7;
            // 
            // btnFromId
            // 
            this.btnFromId.Location = new System.Drawing.Point(179, 93);
            this.btnFromId.Name = "btnFromId";
            this.btnFromId.Size = new System.Drawing.Size(233, 51);
            this.btnFromId.TabIndex = 5;
            this.btnFromId.Text = "从选择项开始统计";
            this.btnFromId.UseVisualStyleBackColor = true;
            this.btnFromId.Click += new System.EventHandler(this.btnFromId_Click);
            // 
            // btnSelOnly
            // 
            this.btnSelOnly.Location = new System.Drawing.Point(20, 93);
            this.btnSelOnly.Name = "btnSelOnly";
            this.btnSelOnly.Size = new System.Drawing.Size(122, 51);
            this.btnSelOnly.TabIndex = 5;
            this.btnSelOnly.Text = "统计选中";
            this.btnSelOnly.UseVisualStyleBackColor = true;
            this.btnSelOnly.Click += new System.EventHandler(this.btnSelOnly_Click);
            // 
            // btnResync
            // 
            this.btnResync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResync.Location = new System.Drawing.Point(935, 9);
            this.btnResync.Name = "btnResync";
            this.btnResync.Size = new System.Drawing.Size(152, 51);
            this.btnResync.TabIndex = 6;
            this.btnResync.Text = "重新同步";
            this.btnResync.UseVisualStyleBackColor = true;
            this.btnResync.Click += new System.EventHandler(this.btnResync_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(335, 101);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(237, 35);
            this.txtId.TabIndex = 10;
            this.txtId.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制IDToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(162, 42);
            // 
            // 复制IDToolStripMenuItem
            // 
            this.复制IDToolStripMenuItem.Name = "复制IDToolStripMenuItem";
            this.复制IDToolStripMenuItem.Size = new System.Drawing.Size(161, 38);
            this.复制IDToolStripMenuItem.Text = "复制ID";
            // 
            // WinSwapStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1825, 957);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "WinSwapStat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "交易统计";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinCTCStat_FormClosing);
            this.Load += new System.EventHandler(this.WinSwapStat_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader datetime;
        private System.Windows.Forms.ColumnHeader currency;
        private System.Windows.Forms.ColumnHeader side;
        private System.Windows.Forms.ColumnHeader size;
        private System.Windows.Forms.ColumnHeader price;
        private System.Windows.Forms.ColumnHeader fillSize;
        private System.Windows.Forms.ColumnHeader priceAvg;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalBuy;
        private System.Windows.Forms.Label lblTotalSell;
        private System.Windows.Forms.Label lblTotalProfit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblBuyAvg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSellAvg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.Button btnSelOnly;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSellCount;
        private System.Windows.Forms.Label lblBuyCount;
        private System.Windows.Forms.Button btnResync;
        private System.Windows.Forms.ComboBox cmbPage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbResyncDays;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblMaxBuy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制IDToolStripMenuItem;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Button btnFromId;
        private System.Windows.Forms.Label lblWaiting;
    }
}