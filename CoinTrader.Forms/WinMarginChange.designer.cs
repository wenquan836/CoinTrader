namespace CoinTrader.Forms
{
    partial class WinMarginChange
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblMargin = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMMR = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUsdxAmount = new System.Windows.Forms.Label();
            this.txtAmount = new CoinTrader.Forms.Control.NumberTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前保证金";
            // 
            // lblMargin
            // 
            this.lblMargin.AutoSize = true;
            this.lblMargin.Location = new System.Drawing.Point(175, 42);
            this.lblMargin.Name = "lblMargin";
            this.lblMargin.Size = new System.Drawing.Size(21, 21);
            this.lblMargin.TabIndex = 0;
            this.lblMargin.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "维持保证金";
            // 
            // lblMMR
            // 
            this.lblMMR.AutoSize = true;
            this.lblMMR.Location = new System.Drawing.Point(175, 93);
            this.lblMMR.Name = "lblMMR";
            this.lblMMR.Size = new System.Drawing.Size(21, 21);
            this.lblMMR.TabIndex = 1;
            this.lblMMR.Text = "0";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(269, 241);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(112, 54);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "增加数量";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "可用保证金";
            // 
            // lblUsdxAmount
            // 
            this.lblUsdxAmount.AutoSize = true;
            this.lblUsdxAmount.Location = new System.Drawing.Point(175, 196);
            this.lblUsdxAmount.Name = "lblUsdxAmount";
            this.lblUsdxAmount.Size = new System.Drawing.Size(21, 21);
            this.lblUsdxAmount.TabIndex = 6;
            this.lblUsdxAmount.Text = "0";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(175, 137);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.NumberType = CoinTrader.Forms.Control.NumberType.Integer;
            this.txtAmount.Size = new System.Drawing.Size(249, 31);
            this.txtAmount.TabIndex = 7;
            // 
            // WinMarginChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 358);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblUsdxAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblMMR);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMargin);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinMarginChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinMarginChange";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMargin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMMR;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUsdxAmount;
        private Control.NumberTextBox txtAmount;
    }
}