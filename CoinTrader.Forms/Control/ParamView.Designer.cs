namespace CoinTrader.Forms.Control
{
    partial class ParamView
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblPos = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.Location = new System.Drawing.Point(-107, 12);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(340, 21);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // lblPos
            // 
            this.lblPos.AutoSize = true;
            this.lblPos.Location = new System.Drawing.Point(257, 12);
            this.lblPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPos.Name = "lblPos";
            this.lblPos.Size = new System.Drawing.Size(0, 21);
            this.lblPos.TabIndex = 1;
            // 
            // ParamView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPos);
            this.Controls.Add(this.lblName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ParamView";
            this.Size = new System.Drawing.Size(639, 47);
            this.Load += new System.EventHandler(this.ParmaView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPos;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
