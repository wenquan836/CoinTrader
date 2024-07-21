namespace CoinTrader.Forms.Control
{
    partial class SwapView
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblInstrument = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblMinSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMinAmount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLever = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFee = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnStat = new System.Windows.Forms.Button();
            this.pnlBehavior = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMonitor = new System.Windows.Forms.Label();
            this.lblPostion = new System.Windows.Forms.Label();
            this.tickView1 = new CoinTrader.Forms.Control.TickView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pnlPosition = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlEmpty = new System.Windows.Forms.Panel();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.pnlMonitor = new System.Windows.Forms.FlowLayoutPanel();
            this.timerPosition = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.pnlPosition.SuspendLayout();
            this.pnlEmpty.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(614, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(46, 38);
            this.btnClose.TabIndex = 49;
            this.btnClose.Text = "x";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblInstrument
            // 
            this.lblInstrument.AutoSize = true;
            this.lblInstrument.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Underline);
            this.lblInstrument.Location = new System.Drawing.Point(81, 50);
            this.lblInstrument.Name = "lblInstrument";
            this.lblInstrument.Size = new System.Drawing.Size(79, 33);
            this.lblInstrument.TabIndex = 50;
            this.lblInstrument.Text = "--aa";
            this.lblInstrument.Click += new System.EventHandler(this.lblInstrument_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 52;
            this.label1.Text = "最小下单量";
            // 
            // lblMinSize
            // 
            this.lblMinSize.AutoSize = true;
            this.lblMinSize.Location = new System.Drawing.Point(176, 127);
            this.lblMinSize.Name = "lblMinSize";
            this.lblMinSize.Size = new System.Drawing.Size(34, 24);
            this.lblMinSize.TabIndex = 52;
            this.lblMinSize.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 52;
            this.label3.Text = "最小面值";
            // 
            // lblMinAmount
            // 
            this.lblMinAmount.AutoSize = true;
            this.lblMinAmount.Location = new System.Drawing.Point(176, 168);
            this.lblMinAmount.Name = "lblMinAmount";
            this.lblMinAmount.Size = new System.Drawing.Size(34, 24);
            this.lblMinAmount.TabIndex = 52;
            this.lblMinAmount.Text = "--";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(359, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 24);
            this.label5.TabIndex = 52;
            this.label5.Text = "最大杠杆";
            // 
            // lblLever
            // 
            this.lblLever.AutoSize = true;
            this.lblLever.Location = new System.Drawing.Point(480, 120);
            this.lblLever.Name = "lblLever";
            this.lblLever.Size = new System.Drawing.Size(34, 24);
            this.lblLever.TabIndex = 52;
            this.lblLever.Text = "--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 52;
            this.label2.Text = "资金费率";
            // 
            // lblFee
            // 
            this.lblFee.AutoSize = true;
            this.lblFee.Location = new System.Drawing.Point(480, 168);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(34, 24);
            this.lblFee.TabIndex = 52;
            this.lblFee.Text = "--";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(663, 443);
            this.tabControl1.TabIndex = 53;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pnlBehavior);
            this.tabPage1.Controls.Add(this.lblMonitor);
            this.tabPage1.Controls.Add(this.lblPostion);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lblFee);
            this.tabPage1.Controls.Add(this.lblInstrument);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tickView1);
            this.tabPage1.Controls.Add(this.lblLever);
            this.tabPage1.Controls.Add(this.lblMinSize);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.lblMinAmount);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(647, 396);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "合约";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnStat
            // 
            this.btnStat.Location = new System.Drawing.Point(533, 2);
            this.btnStat.Name = "btnStat";
            this.btnStat.Size = new System.Drawing.Size(73, 41);
            this.btnStat.TabIndex = 56;
            this.btnStat.Text = "统计";
            this.btnStat.UseVisualStyleBackColor = true;
            this.btnStat.Click += new System.EventHandler(this.btnStat_Click);
            // 
            // pnlBehavior
            // 
            this.pnlBehavior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBehavior.Location = new System.Drawing.Point(3, 224);
            this.pnlBehavior.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlBehavior.Name = "pnlBehavior";
            this.pnlBehavior.Size = new System.Drawing.Size(641, 169);
            this.pnlBehavior.TabIndex = 55;
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(27, 50);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(35, 24);
            this.lblMonitor.TabIndex = 54;
            this.lblMonitor.Text = "❤";
            // 
            // lblPostion
            // 
            this.lblPostion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblPostion.ForeColor = System.Drawing.Color.White;
            this.lblPostion.Location = new System.Drawing.Point(-1, 0);
            this.lblPostion.Margin = new System.Windows.Forms.Padding(3);
            this.lblPostion.Name = "lblPostion";
            this.lblPostion.Size = new System.Drawing.Size(73, 34);
            this.lblPostion.TabIndex = 53;
            this.lblPostion.Text = "持仓";
            this.lblPostion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPostion.Visible = false;
            // 
            // tickView1
            // 
            this.tickView1.Location = new System.Drawing.Point(376, 10);
            this.tickView1.Name = "tickView1";
            this.tickView1.Size = new System.Drawing.Size(271, 93);
            this.tickView1.TabIndex = 51;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pnlPosition);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(647, 379);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "持仓";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pnlPosition
            // 
            this.pnlPosition.AutoScroll = true;
            this.pnlPosition.Controls.Add(this.pnlEmpty);
            this.pnlPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPosition.Location = new System.Drawing.Point(3, 3);
            this.pnlPosition.Name = "pnlPosition";
            this.pnlPosition.Size = new System.Drawing.Size(641, 373);
            this.pnlPosition.TabIndex = 1;
            // 
            // pnlEmpty
            // 
            this.pnlEmpty.Controls.Add(this.lblEmpty);
            this.pnlEmpty.Location = new System.Drawing.Point(3, 3);
            this.pnlEmpty.Name = "pnlEmpty";
            this.pnlEmpty.Size = new System.Drawing.Size(608, 127);
            this.pnlEmpty.TabIndex = 3;
            // 
            // lblEmpty
            // 
            this.lblEmpty.AutoSize = true;
            this.lblEmpty.Location = new System.Drawing.Point(252, 58);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(106, 24);
            this.lblEmpty.TabIndex = 2;
            this.lblEmpty.Text = "暂无持仓";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pnlMonitor);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(647, 379);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "数据";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonitor.Location = new System.Drawing.Point(0, 0);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(647, 379);
            this.pnlMonitor.TabIndex = 0;
            // 
            // timerPosition
            // 
            this.timerPosition.Interval = 300;
            this.timerPosition.Tick += new System.EventHandler(this.timerPosition_Tick);
            // 
            // SwapView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnStat);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.Name = "SwapView";
            this.Size = new System.Drawing.Size(665, 447);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.pnlPosition.ResumeLayout(false);
            this.pnlEmpty.ResumeLayout(false);
            this.pnlEmpty.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblInstrument;
        private TickView tickView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMinSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMinAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLever;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.FlowLayoutPanel pnlMonitor;
        private System.Windows.Forms.FlowLayoutPanel pnlPosition;
        private System.Windows.Forms.Label lblPostion;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.FlowLayoutPanel pnlBehavior;
        private System.Windows.Forms.Panel pnlEmpty;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.Button btnStat;
        private System.Windows.Forms.Timer timerPosition;
    }
}
