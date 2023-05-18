namespace CoinTrader.Forms
{
    partial class WinSwapLiquidate
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
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblMinSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.rdoMarket = new System.Windows.Forms.RadioButton();
            this.rdoLimit = new System.Windows.Forms.RadioButton();
            this.tbSize = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tbSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "可平仓数量";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(202, 41);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(21, 21);
            this.lblAmount.TabIndex = 0;
            this.lblAmount.Text = "0";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(206, 131);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(275, 31);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = " 平仓数量";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(206, 301);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(139, 49);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblMinSize
            // 
            this.lblMinSize.AutoSize = true;
            this.lblMinSize.Location = new System.Drawing.Point(202, 89);
            this.lblMinSize.Name = "lblMinSize";
            this.lblMinSize.Size = new System.Drawing.Size(54, 21);
            this.lblMinSize.TabIndex = 3;
            this.lblMinSize.Text = "0.01";
            this.lblMinSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "最小平仓数量";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = " 平仓价格";
            // 
            // txtPrice
            // 
            this.txtPrice.Enabled = false;
            this.txtPrice.Location = new System.Drawing.Point(371, 227);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(110, 31);
            this.txtPrice.TabIndex = 1;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rdoMarket
            // 
            this.rdoMarket.AutoSize = true;
            this.rdoMarket.Checked = true;
            this.rdoMarket.Location = new System.Drawing.Point(206, 231);
            this.rdoMarket.Name = "rdoMarket";
            this.rdoMarket.Size = new System.Drawing.Size(77, 25);
            this.rdoMarket.TabIndex = 4;
            this.rdoMarket.TabStop = true;
            this.rdoMarket.Text = "市价";
            this.rdoMarket.UseVisualStyleBackColor = true;
            // 
            // rdoLimit
            // 
            this.rdoLimit.AutoSize = true;
            this.rdoLimit.Location = new System.Drawing.Point(288, 231);
            this.rdoLimit.Name = "rdoLimit";
            this.rdoLimit.Size = new System.Drawing.Size(77, 25);
            this.rdoLimit.TabIndex = 4;
            this.rdoLimit.Text = "限价";
            this.rdoLimit.UseVisualStyleBackColor = true;
            this.rdoLimit.CheckedChanged += new System.EventHandler(this.rdoLimit_CheckedChanged);
            // 
            // tbSize
            // 
            this.tbSize.Location = new System.Drawing.Point(194, 178);
            this.tbSize.Maximum = 20;
            this.tbSize.Name = "tbSize";
            this.tbSize.Size = new System.Drawing.Size(287, 80);
            this.tbSize.TabIndex = 5;
            this.tbSize.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbSize.Value = 1;
            this.tbSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbSize_MouseMove);
            this.tbSize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbSize_MouseUp);
            // 
            // WinSwapLiquidate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 378);
            this.Controls.Add(this.rdoLimit);
            this.Controls.Add(this.rdoMarket);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMinSize);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinSwapLiquidate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "平仓";
            this.Load += new System.EventHandler(this.WinSwapLiquidate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblMinSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.RadioButton rdoMarket;
        private System.Windows.Forms.RadioButton rdoLimit;
        private System.Windows.Forms.TrackBar tbSize;
    }
}