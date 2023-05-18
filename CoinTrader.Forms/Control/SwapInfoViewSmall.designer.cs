namespace CoinTrader.Forms.Control
{
    partial class SwapInfoViewSmall
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
            this.lblSide = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUpl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMargin = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblLiqPx = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAvgPx = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblLever = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.Font = new System.Drawing.Font("宋体", 9F);
            this.lblSide.Location = new System.Drawing.Point(20, 12);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(32, 21);
            this.lblSide.TabIndex = 0;
            this.lblSide.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "收益";
            // 
            // lblUpl
            // 
            this.lblUpl.AutoSize = true;
            this.lblUpl.Location = new System.Drawing.Point(192, 12);
            this.lblUpl.Name = "lblUpl";
            this.lblUpl.Size = new System.Drawing.Size(87, 21);
            this.lblUpl.TabIndex = 4;
            this.lblUpl.Text = "000(0%)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(209, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "保证金";
            // 
            // lblMargin
            // 
            this.lblMargin.AutoSize = true;
            this.lblMargin.Location = new System.Drawing.Point(288, 87);
            this.lblMargin.Name = "lblMargin";
            this.lblMargin.Size = new System.Drawing.Size(21, 21);
            this.lblMargin.TabIndex = 5;
            this.lblMargin.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(209, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "强平价";
            // 
            // lblLiqPx
            // 
            this.lblLiqPx.AutoSize = true;
            this.lblLiqPx.Location = new System.Drawing.Point(288, 54);
            this.lblLiqPx.Name = "lblLiqPx";
            this.lblLiqPx.Size = new System.Drawing.Size(21, 21);
            this.lblLiqPx.TabIndex = 6;
            this.lblLiqPx.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 21);
            this.label9.TabIndex = 6;
            this.label9.Text = "均价";
            // 
            // lblAvgPx
            // 
            this.lblAvgPx.AutoSize = true;
            this.lblAvgPx.Location = new System.Drawing.Point(80, 87);
            this.lblAvgPx.Name = "lblAvgPx";
            this.lblAvgPx.Size = new System.Drawing.Size(21, 21);
            this.lblAvgPx.TabIndex = 6;
            this.lblAvgPx.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblLever
            // 
            this.lblLever.AutoSize = true;
            this.lblLever.Location = new System.Drawing.Point(73, 12);
            this.lblLever.Name = "lblLever";
            this.lblLever.Size = new System.Drawing.Size(32, 21);
            this.lblLever.TabIndex = 7;
            this.lblLever.Text = "-x";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "数量";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(79, 56);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(21, 21);
            this.lblAmount.TabIndex = 8;
            this.lblAmount.Text = "0";
            // 
            // SwapInfoViewSmall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblLever);
            this.Controls.Add(this.lblAvgPx);
            this.Controls.Add(this.lblLiqPx);
            this.Controls.Add(this.lblMargin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblUpl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSide);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Name = "SwapInfoViewSmall";
            this.Size = new System.Drawing.Size(441, 116);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSide;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUpl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMargin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblLiqPx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblAvgPx;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblLever;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAmount;
    }
}
