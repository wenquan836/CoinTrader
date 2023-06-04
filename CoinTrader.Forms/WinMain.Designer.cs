namespace CoinTrader.Forms
{
    partial class WinMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinMain));
            this.timer_state_scan = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageOrders = new System.Windows.Forms.TabPage();
            this.pnlMyOrders = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPageFunds = new System.Windows.Forms.TabPage();
            this.pnlBehavior = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPageMonitor = new System.Windows.Forms.TabPage();
            this.pnlMonitor = new System.Windows.Forms.FlowLayoutPanel();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.lblTotalMoney = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblCTCUsdt = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblOTCUsdt = new System.Windows.Forms.Label();
            this.lblUsdtCny = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.账号设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mniBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.mniBank = new System.Windows.Forms.ToolStripMenuItem();
            this.mniCopyConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSpots = new System.Windows.Forms.ToolStripMenuItem();
            this.打开所有运行ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭所有ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnSwap = new System.Windows.Forms.ToolStripMenuItem();
            this.持仓管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.合约查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSwapList = new System.Windows.Forms.ToolStripMenuItem();
            this.其他ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.交叉盘ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlMarketViews = new System.Windows.Forms.FlowLayoutPanel();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageOrders.SuspendLayout();
            this.tabPageFunds.SuspendLayout();
            this.tabPageMonitor.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_state_scan
            // 
            this.timer_state_scan.Enabled = true;
            this.timer_state_scan.Interval = 1000;
            this.timer_state_scan.Tick += new System.EventHandler(this.timer_state_scan_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.lblTotalMoney);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblLoginName);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1847, 42);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(540, 1471);
            this.panel2.TabIndex = 18;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageOrders);
            this.tabControl1.Controls.Add(this.tabPageFunds);
            this.tabControl1.Controls.Add(this.tabPageMonitor);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 774);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(540, 697);
            this.tabControl1.TabIndex = 25;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageOrders
            // 
            this.tabPageOrders.Controls.Add(this.pnlMyOrders);
            this.tabPageOrders.Location = new System.Drawing.Point(4, 31);
            this.tabPageOrders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageOrders.Name = "tabPageOrders";
            this.tabPageOrders.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageOrders.Size = new System.Drawing.Size(532, 662);
            this.tabPageOrders.TabIndex = 0;
            this.tabPageOrders.Text = "挂单";
            this.tabPageOrders.UseVisualStyleBackColor = true;
            // 
            // pnlMyOrders
            // 
            this.pnlMyOrders.AutoScroll = true;
            this.pnlMyOrders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMyOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMyOrders.Location = new System.Drawing.Point(4, 3);
            this.pnlMyOrders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlMyOrders.Name = "pnlMyOrders";
            this.pnlMyOrders.Size = new System.Drawing.Size(524, 656);
            this.pnlMyOrders.TabIndex = 16;
            this.pnlMyOrders.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataView_DBClick);
            // 
            // tabPageFunds
            // 
            this.tabPageFunds.Controls.Add(this.pnlBehavior);
            this.tabPageFunds.Location = new System.Drawing.Point(4, 31);
            this.tabPageFunds.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageFunds.Name = "tabPageFunds";
            this.tabPageFunds.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageFunds.Size = new System.Drawing.Size(532, 662);
            this.tabPageFunds.TabIndex = 1;
            this.tabPageFunds.Text = "资金管理";
            this.tabPageFunds.UseVisualStyleBackColor = true;
            // 
            // pnlBehavior
            // 
            this.pnlBehavior.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBehavior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBehavior.Location = new System.Drawing.Point(4, 3);
            this.pnlBehavior.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlBehavior.Name = "pnlBehavior";
            this.pnlBehavior.Size = new System.Drawing.Size(524, 656);
            this.pnlBehavior.TabIndex = 18;
            this.pnlBehavior.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataView_DBClick);
            // 
            // tabPageMonitor
            // 
            this.tabPageMonitor.Controls.Add(this.pnlMonitor);
            this.tabPageMonitor.Location = new System.Drawing.Point(4, 31);
            this.tabPageMonitor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPageMonitor.Name = "tabPageMonitor";
            this.tabPageMonitor.Size = new System.Drawing.Size(532, 662);
            this.tabPageMonitor.TabIndex = 2;
            this.tabPageMonitor.Text = "数据";
            this.tabPageMonitor.UseVisualStyleBackColor = true;
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.AutoScroll = true;
            this.pnlMonitor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitor.Location = new System.Drawing.Point(0, 0);
            this.pnlMonitor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(532, 662);
            this.pnlMonitor.TabIndex = 17;
            this.pnlMonitor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataView_DBClick);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtConsole);
            this.tabLog.Location = new System.Drawing.Point(4, 31);
            this.tabLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(532, 662);
            this.tabLog.TabIndex = 3;
            this.tabLog.Text = "日志";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Location = new System.Drawing.Point(0, 0);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(532, 662);
            this.txtConsole.TabIndex = 6;
            this.txtConsole.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataView_DBClick);
            // 
            // lblTotalMoney
            // 
            this.lblTotalMoney.AutoSize = true;
            this.lblTotalMoney.Location = new System.Drawing.Point(156, 90);
            this.lblTotalMoney.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalMoney.Name = "lblTotalMoney";
            this.lblTotalMoney.Size = new System.Drawing.Size(115, 21);
            this.lblTotalMoney.TabIndex = 28;
            this.lblTotalMoney.Text = "当前总估值";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 27;
            this.label2.Text = "当前总估值";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 35);
            this.label1.TabIndex = 26;
            this.label1.Text = "账号";
            // 
            // lblLoginName
            // 
            this.lblLoginName.AutoSize = true;
            this.lblLoginName.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoginName.Location = new System.Drawing.Point(95, 20);
            this.lblLoginName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoginName.Name = "lblLoginName";
            this.lblLoginName.Size = new System.Drawing.Size(69, 35);
            this.lblLoginName.TabIndex = 22;
            this.lblLoginName.Text = "---";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(8, 156);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(519, 218);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USDT";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.lblCTCUsdt);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.lblOTCUsdt);
            this.panel1.Controls.Add(this.lblUsdtCny);
            this.panel1.Location = new System.Drawing.Point(11, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 166);
            this.panel1.TabIndex = 52;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 61);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 21);
            this.label18.TabIndex = 2;
            this.label18.Text = "资金账户";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 21);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 21);
            this.label14.TabIndex = 2;
            this.label14.Text = "交易账户";
            // 
            // lblCTCUsdt
            // 
            this.lblCTCUsdt.AutoSize = true;
            this.lblCTCUsdt.Location = new System.Drawing.Point(112, 21);
            this.lblCTCUsdt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCTCUsdt.Name = "lblCTCUsdt";
            this.lblCTCUsdt.Size = new System.Drawing.Size(21, 21);
            this.lblCTCUsdt.TabIndex = 2;
            this.lblCTCUsdt.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(17, 134);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 21);
            this.label19.TabIndex = 3;
            this.label19.Text = "总估值";
            // 
            // lblOTCUsdt
            // 
            this.lblOTCUsdt.AutoSize = true;
            this.lblOTCUsdt.Location = new System.Drawing.Point(112, 61);
            this.lblOTCUsdt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOTCUsdt.Name = "lblOTCUsdt";
            this.lblOTCUsdt.Size = new System.Drawing.Size(21, 21);
            this.lblOTCUsdt.TabIndex = 2;
            this.lblOTCUsdt.Text = "0";
            // 
            // lblUsdtCny
            // 
            this.lblUsdtCny.AutoSize = true;
            this.lblUsdtCny.Location = new System.Drawing.Point(112, 134);
            this.lblUsdtCny.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsdtCny.Name = "lblUsdtCny";
            this.lblUsdtCny.Size = new System.Drawing.Size(21, 21);
            this.lblUsdtCny.TabIndex = 2;
            this.lblUsdtCny.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.mnSpots,
            this.mnSwap,
            this.其他ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(2387, 42);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.账号设置ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.mniBrowser,
            this.mniBank,
            this.mniCopyConfig});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(72, 40);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 账号设置ToolStripMenuItem
            // 
            this.账号设置ToolStripMenuItem.Name = "账号设置ToolStripMenuItem";
            this.账号设置ToolStripMenuItem.Size = new System.Drawing.Size(255, 40);
            this.账号设置ToolStripMenuItem.Text = "账号设置";
            this.账号设置ToolStripMenuItem.Click += new System.EventHandler(this.账号设置ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(255, 40);
            this.toolStripMenuItem2.Text = "登录密码";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // mniBrowser
            // 
            this.mniBrowser.Name = "mniBrowser";
            this.mniBrowser.Size = new System.Drawing.Size(255, 40);
            this.mniBrowser.Text = "浏览器设置";
            this.mniBrowser.Click += new System.EventHandler(this.浏览器设置ToolStripMenuItem_Click);
            // 
            // mniBank
            // 
            this.mniBank.Name = "mniBank";
            this.mniBank.Size = new System.Drawing.Size(255, 40);
            this.mniBank.Text = "收付款设置";
            this.mniBank.Click += new System.EventHandler(this.收付款设置ToolStripMenuItem_Click);
            // 
            // mniCopyConfig
            // 
            this.mniCopyConfig.Name = "mniCopyConfig";
            this.mniCopyConfig.Size = new System.Drawing.Size(255, 40);
            this.mniCopyConfig.Text = "配置参数复制";
            this.mniCopyConfig.Click += new System.EventHandler(this.mniCopyConfig_Click);
            // 
            // mnSpots
            // 
            this.mnSpots.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开所有运行ToolStripMenuItem1,
            this.关闭所有ToolStripMenuItem,
            this.toolStripMenuItem4});
            this.mnSpots.Name = "mnSpots";
            this.mnSpots.Size = new System.Drawing.Size(114, 40);
            this.mnSpots.Text = "现货交易";
            // 
            // 打开所有运行ToolStripMenuItem1
            // 
            this.打开所有运行ToolStripMenuItem1.Name = "打开所有运行ToolStripMenuItem1";
            this.打开所有运行ToolStripMenuItem1.Size = new System.Drawing.Size(264, 40);
            this.打开所有运行ToolStripMenuItem1.Text = "打开所有-运行";
            // 
            // 关闭所有ToolStripMenuItem
            // 
            this.关闭所有ToolStripMenuItem.Name = "关闭所有ToolStripMenuItem";
            this.关闭所有ToolStripMenuItem.Size = new System.Drawing.Size(264, 40);
            this.关闭所有ToolStripMenuItem.Text = "关闭所有";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(261, 6);
            // 
            // mnSwap
            // 
            this.mnSwap.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.持仓管理ToolStripMenuItem,
            this.合约查询ToolStripMenuItem,
            this.toolStripMenuItem5,
            this.mnuSwapList});
            this.mnSwap.Name = "mnSwap";
            this.mnSwap.Size = new System.Drawing.Size(114, 40);
            this.mnSwap.Text = "永续合约";
            // 
            // 持仓管理ToolStripMenuItem
            // 
            this.持仓管理ToolStripMenuItem.Name = "持仓管理ToolStripMenuItem";
            this.持仓管理ToolStripMenuItem.Size = new System.Drawing.Size(213, 40);
            this.持仓管理ToolStripMenuItem.Text = "持仓管理";
            this.持仓管理ToolStripMenuItem.Click += new System.EventHandler(this.持仓管理ToolStripMenuItem_Click);
            // 
            // 合约查询ToolStripMenuItem
            // 
            this.合约查询ToolStripMenuItem.Name = "合约查询ToolStripMenuItem";
            this.合约查询ToolStripMenuItem.Size = new System.Drawing.Size(213, 40);
            this.合约查询ToolStripMenuItem.Text = "合约查询";
            this.合约查询ToolStripMenuItem.Click += new System.EventHandler(this.合约查询ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(210, 6);
            // 
            // mnuSwapList
            // 
            this.mnuSwapList.Name = "mnuSwapList";
            this.mnuSwapList.Size = new System.Drawing.Size(213, 40);
            this.mnuSwapList.Text = "策略交易";
            this.mnuSwapList.Click += new System.EventHandler(this.合约管理ToolStripMenuItem_Click);
            // 
            // 其他ToolStripMenuItem
            // 
            this.其他ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.交叉盘ToolStripMenuItem1});
            this.其他ToolStripMenuItem.Name = "其他ToolStripMenuItem";
            this.其他ToolStripMenuItem.Size = new System.Drawing.Size(72, 40);
            this.其他ToolStripMenuItem.Text = "其他";
            // 
            // 交叉盘ToolStripMenuItem1
            // 
            this.交叉盘ToolStripMenuItem1.Name = "交叉盘ToolStripMenuItem1";
            this.交叉盘ToolStripMenuItem1.Size = new System.Drawing.Size(192, 40);
            this.交叉盘ToolStripMenuItem1.Text = "交叉盘";
            this.交叉盘ToolStripMenuItem1.Click += new System.EventHandler(this.交叉盘ToolStripMenuItem1_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.pnlMarketViews);
            this.pnlMain.Location = new System.Drawing.Point(0, 39);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1839, 1723);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlMarketViews
            // 
            this.pnlMarketViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMarketViews.AutoScroll = true;
            this.pnlMarketViews.Location = new System.Drawing.Point(0, 0);
            this.pnlMarketViews.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlMarketViews.Name = "pnlMarketViews";
            this.pnlMarketViews.Size = new System.Drawing.Size(1835, 1476);
            this.pnlMarketViews.TabIndex = 3;
            this.pnlMarketViews.Visible = false;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // WinMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2387, 1513);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "WinMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数字资管";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WinMain_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageOrders.ResumeLayout(false);
            this.tabPageFunds.ResumeLayout(false);
            this.tabPageMonitor.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer_state_scan;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageOrders;
        private System.Windows.Forms.FlowLayoutPanel pnlMyOrders;
        private System.Windows.Forms.TabPage tabPageFunds;
        private System.Windows.Forms.FlowLayoutPanel pnlBehavior;
        private System.Windows.Forms.TabPage tabPageMonitor;
        private System.Windows.Forms.FlowLayoutPanel pnlMonitor;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblUsdtCny;
        private System.Windows.Forms.Label lblOTCUsdt;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblCTCUsdt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 账号设置ToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.FlowLayoutPanel pnlMarketViews;
        private System.Windows.Forms.ToolStripMenuItem mnSpots;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalMoney;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mniBrowser;
        private System.Windows.Forms.ToolStripMenuItem mniBank;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem mnSwap;
        private System.Windows.Forms.ToolStripMenuItem 持仓管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 合约查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSwapList;
        private System.Windows.Forms.ToolStripMenuItem mniCopyConfig;
        private System.Windows.Forms.ToolStripMenuItem 其他ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 交叉盘ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打开所有运行ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 关闭所有ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    }
}

