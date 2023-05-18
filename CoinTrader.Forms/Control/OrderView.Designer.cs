namespace CoinTrader.Forms
{
    partial class OrderView
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
            this.btnOperate = new System.Windows.Forms.Button();
            this.lblSide = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnOperate
            // 
            this.btnOperate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOperate.Location = new System.Drawing.Point(461, 18);
            this.btnOperate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOperate.Name = "btnOperate";
            this.btnOperate.Size = new System.Drawing.Size(92, 36);
            this.btnOperate.TabIndex = 0;
            this.btnOperate.Text = "撤消";
            this.btnOperate.UseVisualStyleBackColor = true;
            this.btnOperate.Click += new System.EventHandler(this.btnOperate_Click);
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.ForeColor = System.Drawing.Color.Red;
            this.lblSide.Location = new System.Drawing.Point(4, 25);
            this.lblSide.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(52, 21);
            this.lblSide.TabIndex = 2;
            this.lblSide.Text = "卖出";
            this.lblSide.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(79, 25);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(153, 21);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "-------------";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OrderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblSide);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnOperate);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "OrderView";
            this.Size = new System.Drawing.Size(571, 72);
            this.Load += new System.EventHandler(this.OrderView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOperate;
        private System.Windows.Forms.Label lblSide;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Timer timer1;
    }
}
