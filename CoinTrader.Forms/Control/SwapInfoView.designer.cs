namespace CoinTrader.Forms.Control
{
    partial class SwapInfoView
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
            this.lblName = new System.Windows.Forms.Label();
            this.Liquidate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
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
            this.lblPx = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMagrin = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSide.Location = new System.Drawing.Point(145, 16);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(40, 28);
            this.lblSide.TabIndex = 0;
            this.lblSide.Text = "--";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(15, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(54, 28);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "---";
            // 
            // Liquidate
            // 
            this.Liquidate.Location = new System.Drawing.Point(474, 159);
            this.Liquidate.Name = "Liquidate";
            this.Liquidate.Size = new System.Drawing.Size(107, 41);
            this.Liquidate.TabIndex = 2;
            this.Liquidate.Text = "平仓";
            this.Liquidate.UseVisualStyleBackColor = true;
            this.Liquidate.Click += new System.EventHandler(this.Liquidate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(473, 200);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 40);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "市价全平";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "收益";
            // 
            // lblUpl
            // 
            this.lblUpl.AutoSize = true;
            this.lblUpl.Location = new System.Drawing.Point(373, 23);
            this.lblUpl.Name = "lblUpl";
            this.lblUpl.Size = new System.Drawing.Size(98, 21);
            this.lblUpl.TabIndex = 4;
            this.lblUpl.Text = "0.0 (0%)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 21);
            this.label5.TabIndex = 5;
            this.label5.Text = "保证金";
            // 
            // lblMargin
            // 
            this.lblMargin.AutoSize = true;
            this.lblMargin.Location = new System.Drawing.Point(120, 181);
            this.lblMargin.Name = "lblMargin";
            this.lblMargin.Size = new System.Drawing.Size(21, 21);
            this.lblMargin.TabIndex = 5;
            this.lblMargin.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(229, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 21);
            this.label7.TabIndex = 6;
            this.label7.Text = "强平价格";
            // 
            // lblLiqPx
            // 
            this.lblLiqPx.AutoSize = true;
            this.lblLiqPx.Location = new System.Drawing.Point(329, 78);
            this.lblLiqPx.Name = "lblLiqPx";
            this.lblLiqPx.Size = new System.Drawing.Size(21, 21);
            this.lblLiqPx.TabIndex = 6;
            this.lblLiqPx.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(229, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 21);
            this.label9.TabIndex = 6;
            this.label9.Text = "开仓均价";
            // 
            // lblAvgPx
            // 
            this.lblAvgPx.AutoSize = true;
            this.lblAvgPx.Location = new System.Drawing.Point(329, 128);
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
            this.lblLever.Location = new System.Drawing.Point(208, 23);
            this.lblLever.Name = "lblLever";
            this.lblLever.Size = new System.Drawing.Size(32, 21);
            this.lblLever.TabIndex = 7;
            this.lblLever.Text = "-x";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "持仓数量";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(120, 128);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(21, 21);
            this.lblAmount.TabIndex = 8;
            this.lblAmount.Text = "0";
            // 
            // lblPx
            // 
            this.lblPx.AutoSize = true;
            this.lblPx.Location = new System.Drawing.Point(120, 78);
            this.lblPx.Name = "lblPx";
            this.lblPx.Size = new System.Drawing.Size(21, 21);
            this.lblPx.TabIndex = 9;
            this.lblPx.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "标记价格";
            // 
            // btnMagrin
            // 
            this.btnMagrin.Location = new System.Drawing.Point(474, 78);
            this.btnMagrin.Name = "btnMagrin";
            this.btnMagrin.Size = new System.Drawing.Size(107, 41);
            this.btnMagrin.TabIndex = 2;
            this.btnMagrin.Text = "保证金";
            this.btnMagrin.UseVisualStyleBackColor = true;
            this.btnMagrin.Click += new System.EventHandler(this.btnMagrin_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(474, 119);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(107, 40);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "止盈止损";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // SwapInfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPx);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblLever);
            this.Controls.Add(this.lblAvgPx);
            this.Controls.Add(this.lblLiqPx);
            this.Controls.Add(this.lblMargin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblUpl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMagrin);
            this.Controls.Add(this.Liquidate);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblSide);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Name = "SwapInfoView";
            this.Size = new System.Drawing.Size(584, 246);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSide;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button Liquidate;
        private System.Windows.Forms.Button btnClose;
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
        private System.Windows.Forms.Label lblPx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMagrin;
        private System.Windows.Forms.Button btnStop;
    }
}
