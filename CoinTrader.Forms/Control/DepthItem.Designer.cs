namespace CoinTrader.Forms.Control
{
    partial class DepthItem
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
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblOrders = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPrice
            // 
            this.lblPrice.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPrice.AutoSize = true;
            this.lblPrice.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice.Font = new System.Drawing.Font("宋体", 12F);
            this.lblPrice.Location = new System.Drawing.Point(11, 5);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(39, 20);
            this.lblPrice.TabIndex = 0;
            this.lblPrice.Text = "0.0";
            // 
            // lblAmount
            // 
            this.lblAmount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAmount.AutoSize = true;
            this.lblAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblAmount.Font = new System.Drawing.Font("宋体", 12F);
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(14)))), ((int)(((byte)(1)))));
            this.lblAmount.Location = new System.Drawing.Point(172, 5);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(69, 20);
            this.lblAmount.TabIndex = 0;
            this.lblAmount.Text = "200000";
            this.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrders
            // 
            this.lblOrders.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOrders.AutoSize = true;
            this.lblOrders.BackColor = System.Drawing.Color.Transparent;
            this.lblOrders.Font = new System.Drawing.Font("宋体", 12F);
            this.lblOrders.ForeColor = System.Drawing.Color.Black;
            this.lblOrders.Location = new System.Drawing.Point(296, 5);
            this.lblOrders.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrders.Name = "lblOrders";
            this.lblOrders.Size = new System.Drawing.Size(39, 20);
            this.lblOrders.TabIndex = 1;
            this.lblOrders.Text = "200";
            // 
            // DepthItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblOrders);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblPrice);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DepthItem";
            this.Size = new System.Drawing.Size(357, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblOrders;
    }
}
