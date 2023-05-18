namespace CoinTrader.Forms.Control
{
    partial class DepthView
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
            this.flpBuy = new System.Windows.Forms.FlowLayoutPanel();
            this.flpSell = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpBuy
            // 
            this.flpBuy.AutoScroll = true;
            this.flpBuy.AutoSize = true;
            this.flpBuy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBuy.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpBuy.Location = new System.Drawing.Point(0, 0);
            this.flpBuy.Name = "flpBuy";
            this.flpBuy.Size = new System.Drawing.Size(870, 279);
            this.flpBuy.TabIndex = 1;
            this.flpBuy.WrapContents = false;
            // 
            // flpSell
            // 
            this.flpSell.AutoScroll = true;
            this.flpSell.AutoSize = true;
            this.flpSell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSell.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flpSell.Location = new System.Drawing.Point(0, 0);
            this.flpSell.Name = "flpSell";
            this.flpSell.Size = new System.Drawing.Size(870, 281);
            this.flpSell.TabIndex = 2;
            this.flpSell.WrapContents = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flpSell);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flpBuy);
            this.splitContainer1.Size = new System.Drawing.Size(872, 568);
            this.splitContainer1.SplitterDistance = 283;
            this.splitContainer1.TabIndex = 3;
            // 
            // DeepView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "DeepView";
            this.Size = new System.Drawing.Size(872, 568);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpBuy;
        private System.Windows.Forms.FlowLayoutPanel flpSell;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
