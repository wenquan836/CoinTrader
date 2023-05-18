namespace CoinTradeGecko
{
    partial class WinWithdrawal
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtDestAccount = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtSourceAccount = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtDestAccount
            // 
            this.txtDestAccount.Location = new System.Drawing.Point(280, 50);
            this.txtDestAccount.Name = "txtDestAccount";
            this.txtDestAccount.Size = new System.Drawing.Size(320, 31);
            this.txtDestAccount.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(280, 213);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(320, 31);
            this.txtPassword.TabIndex = 0;
            // 
            // txtSourceAccount
            // 
            this.txtSourceAccount.Location = new System.Drawing.Point(280, 176);
            this.txtSourceAccount.Name = "txtSourceAccount";
            this.txtSourceAccount.Size = new System.Drawing.Size(320, 31);
            this.txtSourceAccount.TabIndex = 0;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(280, 250);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(320, 31);
            this.textBox4.TabIndex = 0;
            // 
            // txtCurrency
            // 
            this.txtCurrency.Location = new System.Drawing.Point(280, 139);
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.Size = new System.Drawing.Size(320, 31);
            this.txtCurrency.TabIndex = 0;
            // 
            // WinWithdrawal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 391);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.txtSourceAccount);
            this.Controls.Add(this.txtCurrency);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtDestAccount);
            this.Name = "WinWithdrawal";
            this.Text = "WinWithdrawal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtDestAccount;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtSourceAccount;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txtCurrency;
    }
}