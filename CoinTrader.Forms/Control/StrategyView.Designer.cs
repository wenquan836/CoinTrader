namespace CoinTrader.Forms.Control
{
    partial class StrategyView
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
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.lblExcuting = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSetting = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkEnable.Location = new System.Drawing.Point(15, 11);
            this.chkEnable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(155, 29);
            this.chkEnable.TabIndex = 0;
            this.chkEnable.Text = "checkBox1";
            this.chkEnable.UseVisualStyleBackColor = true;
            this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnable_CheckedChanged);
            // 
            // lblExcuting
            // 
            this.lblExcuting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExcuting.AutoSize = true;
            this.lblExcuting.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExcuting.Location = new System.Drawing.Point(-119, 14);
            this.lblExcuting.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExcuting.Name = "lblExcuting";
            this.lblExcuting.Size = new System.Drawing.Size(34, 24);
            this.lblExcuting.TabIndex = 1;
            this.lblExcuting.Text = "●";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Location = new System.Drawing.Point(347, 11);
            this.btnSetting.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(78, 34);
            this.btnSetting.TabIndex = 2;
            this.btnSetting.Text = "设置";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(14, 43);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(120, 21);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "----------";
            // 
            // BehaviorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.lblExcuting);
            this.Controls.Add(this.chkEnable);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "BehaviorView";
            this.Size = new System.Drawing.Size(438, 75);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Label lblExcuting;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Label lblMessage;
    }
}
