namespace CoinTrader.Forms.Control
{
    partial class SpotView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnStat = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.lblCTCAvalible = new System.Windows.Forms.Label();
            this.lblCTCHold = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label20 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlBehavior = new System.Windows.Forms.FlowLayoutPanel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabTrade = new System.Windows.Forms.TabPage();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.tabQuick = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotal2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCTCAvalible2 = new System.Windows.Forms.Label();
            this.lblCTCHold2 = new System.Windows.Forms.Label();
            this.txtSellAmount = new System.Windows.Forms.TextBox();
            this.btnSell = new System.Windows.Forms.Button();
            this.txtBuyAmount = new System.Windows.Forms.TextBox();
            this.btnBuy = new System.Windows.Forms.Button();
            this.btnAllIn = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.tabData = new System.Windows.Forms.TabPage();
            this.pnlMonitor = new System.Windows.Forms.FlowLayoutPanel();
            this.tabDepth = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.tickView1 = new CoinTrader.Forms.Control.TickView();
            this.tickView2 = new CoinTrader.Forms.Control.TickView();
            this.depthView1 = new CoinTrader.Forms.Control.DepthView();
            this.groupBox3.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabTrade.SuspendLayout();
            this.tabQuick.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabDepth.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.btnStat);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.lblCTCAvalible);
            this.groupBox3.Controls.Add(this.lblCTCHold);
            this.groupBox3.Location = new System.Drawing.Point(28, 99);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(548, 75);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "交易账户";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(235, 40);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 21);
            this.label15.TabIndex = 0;
            this.label15.Text = "冻结";
            // 
            // btnStat
            // 
            this.btnStat.Location = new System.Drawing.Point(458, 19);
            this.btnStat.Margin = new System.Windows.Forms.Padding(4);
            this.btnStat.Name = "btnStat";
            this.btnStat.Size = new System.Drawing.Size(82, 48);
            this.btnStat.TabIndex = 50;
            this.btnStat.Tag = "";
            this.btnStat.Text = "统计";
            this.btnStat.UseVisualStyleBackColor = true;
            this.btnStat.Click += new System.EventHandler(this.btnStat_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(33, 40);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 21);
            this.label16.TabIndex = 0;
            this.label16.Text = "可用";
            // 
            // lblCTCAvalible
            // 
            this.lblCTCAvalible.AutoSize = true;
            this.lblCTCAvalible.Location = new System.Drawing.Point(90, 40);
            this.lblCTCAvalible.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCAvalible.Name = "lblCTCAvalible";
            this.lblCTCAvalible.Size = new System.Drawing.Size(21, 21);
            this.lblCTCAvalible.TabIndex = 9;
            this.lblCTCAvalible.Text = "0";
            // 
            // lblCTCHold
            // 
            this.lblCTCHold.AutoSize = true;
            this.lblCTCHold.Location = new System.Drawing.Point(293, 40);
            this.lblCTCHold.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCHold.Name = "lblCTCHold";
            this.lblCTCHold.Size = new System.Drawing.Size(21, 21);
            this.lblCTCHold.TabIndex = 9;
            this.lblCTCHold.Text = "0";
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCurrency.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrency.Location = new System.Drawing.Point(53, 40);
            this.lblCurrency.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(69, 35);
            this.lblCurrency.TabIndex = 44;
            this.lblCurrency.Text = "BTC";
            this.lblCurrency.Click += new System.EventHandler(this.lblCurrency_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(163, 52);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 21);
            this.label20.TabIndex = 46;
            this.label20.Text = "估值";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(224, 52);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(54, 21);
            this.lblTotal.TabIndex = 45;
            this.lblTotal.Text = "0000";
            // 
            // pnlBehavior
            // 
            this.pnlBehavior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBehavior.Location = new System.Drawing.Point(4, 177);
            this.pnlBehavior.Margin = new System.Windows.Forms.Padding(4);
            this.pnlBehavior.Name = "pnlBehavior";
            this.pnlBehavior.Size = new System.Drawing.Size(590, 164);
            this.pnlBehavior.TabIndex = 47;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabTrade);
            this.tabMain.Controls.Add(this.tabQuick);
            this.tabMain.Controls.Add(this.tabData);
            this.tabMain.Controls.Add(this.tabDepth);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(606, 380);
            this.tabMain.TabIndex = 48;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabTrade
            // 
            this.tabTrade.Controls.Add(this.tickView1);
            this.tabTrade.Controls.Add(this.lblMonitor);
            this.tabTrade.Controls.Add(this.label20);
            this.tabTrade.Controls.Add(this.lblTotal);
            this.tabTrade.Controls.Add(this.lblCurrency);
            this.tabTrade.Controls.Add(this.groupBox3);
            this.tabTrade.Controls.Add(this.pnlBehavior);
            this.tabTrade.Location = new System.Drawing.Point(4, 31);
            this.tabTrade.Margin = new System.Windows.Forms.Padding(4);
            this.tabTrade.Name = "tabTrade";
            this.tabTrade.Padding = new System.Windows.Forms.Padding(4);
            this.tabTrade.Size = new System.Drawing.Size(598, 345);
            this.tabTrade.TabIndex = 0;
            this.tabTrade.Text = "交易";
            this.tabTrade.UseVisualStyleBackColor = true;
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(24, 47);
            this.lblMonitor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(31, 21);
            this.lblMonitor.TabIndex = 48;
            this.lblMonitor.Text = "❤";
            // 
            // tabQuick
            // 
            this.tabQuick.Controls.Add(this.label5);
            this.tabQuick.Controls.Add(this.lblTotal2);
            this.tabQuick.Controls.Add(this.label1);
            this.tabQuick.Controls.Add(this.label2);
            this.tabQuick.Controls.Add(this.lblCTCAvalible2);
            this.tabQuick.Controls.Add(this.lblCTCHold2);
            this.tabQuick.Controls.Add(this.txtSellAmount);
            this.tabQuick.Controls.Add(this.btnSell);
            this.tabQuick.Controls.Add(this.txtBuyAmount);
            this.tabQuick.Controls.Add(this.btnBuy);
            this.tabQuick.Controls.Add(this.btnAllIn);
            this.tabQuick.Controls.Add(this.btnClearAll);
            this.tabQuick.Controls.Add(this.tickView2);
            this.tabQuick.Location = new System.Drawing.Point(4, 31);
            this.tabQuick.Name = "tabQuick";
            this.tabQuick.Size = new System.Drawing.Size(598, 350);
            this.tabQuick.TabIndex = 4;
            this.tabQuick.Text = "快捷";
            this.tabQuick.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 35);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 21);
            this.label5.TabIndex = 51;
            this.label5.Text = "估值";
            // 
            // lblTotal2
            // 
            this.lblTotal2.AutoSize = true;
            this.lblTotal2.Location = new System.Drawing.Point(184, 35);
            this.lblTotal2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal2.Name = "lblTotal2";
            this.lblTotal2.Size = new System.Drawing.Size(32, 21);
            this.lblTotal2.TabIndex = 50;
            this.lblTotal2.Text = "--";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(307, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 46;
            this.label1.Text = "冻结";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 96);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 21);
            this.label2.TabIndex = 47;
            this.label2.Text = "可用";
            // 
            // lblCTCAvalible2
            // 
            this.lblCTCAvalible2.AutoSize = true;
            this.lblCTCAvalible2.Location = new System.Drawing.Point(162, 96);
            this.lblCTCAvalible2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCAvalible2.Name = "lblCTCAvalible2";
            this.lblCTCAvalible2.Size = new System.Drawing.Size(21, 21);
            this.lblCTCAvalible2.TabIndex = 48;
            this.lblCTCAvalible2.Text = "0";
            // 
            // lblCTCHold2
            // 
            this.lblCTCHold2.AutoSize = true;
            this.lblCTCHold2.Location = new System.Drawing.Point(365, 96);
            this.lblCTCHold2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCHold2.Name = "lblCTCHold2";
            this.lblCTCHold2.Size = new System.Drawing.Size(21, 21);
            this.lblCTCHold2.TabIndex = 49;
            this.lblCTCHold2.Text = "0";
            // 
            // txtSellAmount
            // 
            this.txtSellAmount.Font = new System.Drawing.Font("宋体", 12F);
            this.txtSellAmount.Location = new System.Drawing.Point(162, 218);
            this.txtSellAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtSellAmount.Name = "txtSellAmount";
            this.txtSellAmount.Size = new System.Drawing.Size(121, 39);
            this.txtSellAmount.TabIndex = 44;
            this.txtSellAmount.Text = "1000";
            // 
            // btnSell
            // 
            this.btnSell.Location = new System.Drawing.Point(11, 218);
            this.btnSell.Margin = new System.Windows.Forms.Padding(4);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(133, 39);
            this.btnSell.TabIndex = 43;
            this.btnSell.Tag = "";
            this.btnSell.Text = "减仓(USD)";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // txtBuyAmount
            // 
            this.txtBuyAmount.Font = new System.Drawing.Font("宋体", 12F);
            this.txtBuyAmount.Location = new System.Drawing.Point(162, 160);
            this.txtBuyAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtBuyAmount.Name = "txtBuyAmount";
            this.txtBuyAmount.Size = new System.Drawing.Size(121, 39);
            this.txtBuyAmount.TabIndex = 44;
            this.txtBuyAmount.Text = "1000";
            // 
            // btnBuy
            // 
            this.btnBuy.Location = new System.Drawing.Point(11, 160);
            this.btnBuy.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(133, 39);
            this.btnBuy.TabIndex = 43;
            this.btnBuy.Tag = "";
            this.btnBuy.Text = "补仓(USD)";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // btnAllIn
            // 
            this.btnAllIn.Location = new System.Drawing.Point(58, 351);
            this.btnAllIn.Name = "btnAllIn";
            this.btnAllIn.Size = new System.Drawing.Size(133, 49);
            this.btnAllIn.TabIndex = 1;
            this.btnAllIn.Text = "满仓买入";
            this.btnAllIn.UseVisualStyleBackColor = true;
            this.btnAllIn.Visible = false;
            this.btnAllIn.Click += new System.EventHandler(this.btnAllIn_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(57, 283);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(134, 49);
            this.btnClearAll.TabIndex = 0;
            this.btnClearAll.Text = "市价清仓";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Visible = false;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.pnlMonitor);
            this.tabData.Location = new System.Drawing.Point(4, 31);
            this.tabData.Margin = new System.Windows.Forms.Padding(4);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(4);
            this.tabData.Size = new System.Drawing.Size(598, 350);
            this.tabData.TabIndex = 1;
            this.tabData.Text = "数据";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitor.Location = new System.Drawing.Point(4, 4);
            this.pnlMonitor.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(590, 342);
            this.pnlMonitor.TabIndex = 0;
            // 
            // tabDepth
            // 
            this.tabDepth.Controls.Add(this.depthView1);
            this.tabDepth.Location = new System.Drawing.Point(4, 31);
            this.tabDepth.Margin = new System.Windows.Forms.Padding(4);
            this.tabDepth.Name = "tabDepth";
            this.tabDepth.Size = new System.Drawing.Size(598, 350);
            this.tabDepth.TabIndex = 2;
            this.tabDepth.Text = "深度";
            this.tabDepth.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(562, -2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(42, 33);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "x";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // tickView1
            // 
            this.tickView1.Location = new System.Drawing.Point(349, 18);
            this.tickView1.Name = "tickView1";
            this.tickView1.Size = new System.Drawing.Size(245, 81);
            this.tickView1.TabIndex = 51;
            // 
            // tickView2
            // 
            this.tickView2.Location = new System.Drawing.Point(322, 165);
            this.tickView2.Name = "tickView2";
            this.tickView2.Size = new System.Drawing.Size(261, 81);
            this.tickView2.TabIndex = 45;
            // 
            // depthView1
            // 
            this.depthView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.depthView1.Location = new System.Drawing.Point(0, 0);
            this.depthView1.Margin = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.depthView1.Name = "depthView1";
            this.depthView1.Size = new System.Drawing.Size(598, 350);
            this.depthView1.TabIndex = 0;
            // 
            // SpotView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SpotView";
            this.Size = new System.Drawing.Size(606, 380);
            this.ParentChanged += new System.EventHandler(this.SpotView_ParentChanged);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabTrade.ResumeLayout(false);
            this.tabTrade.PerformLayout();
            this.tabQuick.ResumeLayout(false);
            this.tabQuick.PerformLayout();
            this.tabData.ResumeLayout(false);
            this.tabDepth.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblCTCAvalible;
        private System.Windows.Forms.Label lblCTCHold;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.FlowLayoutPanel pnlBehavior;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabTrade;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.FlowLayoutPanel pnlMonitor;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.TabPage tabDepth;
        private DepthView depthView1;
        private System.Windows.Forms.Button btnStat;
        private System.Windows.Forms.TabPage tabQuick;
        private System.Windows.Forms.TextBox txtSellAmount;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.TextBox txtBuyAmount;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Button btnAllIn;
        private System.Windows.Forms.Button btnClearAll;
        private TickView tickView1;
        private TickView tickView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCTCAvalible2;
        private System.Windows.Forms.Label lblCTCHold2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotal2;
    }
}
