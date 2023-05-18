namespace CoinTrader.Forms
{
    partial class WinStartup
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnRetry = new System.Windows.Forms.Button();
            this.pnlStart = new System.Windows.Forms.Panel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.pnlStart.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(262, 52);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(387, 75);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "正在初始化......";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(346, 158);
            this.btnRetry.Margin = new System.Windows.Forms.Padding(4);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(189, 51);
            this.btnRetry.TabIndex = 1;
            this.btnRetry.Text = "重 试";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // pnlStart
            // 
            this.pnlStart.Controls.Add(this.lblMessage);
            this.pnlStart.Controls.Add(this.btnRetry);
            this.pnlStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStart.Location = new System.Drawing.Point(0, 0);
            this.pnlStart.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.pnlStart.Name = "pnlStart";
            this.pnlStart.Size = new System.Drawing.Size(847, 247);
            this.pnlStart.TabIndex = 2;
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.label1);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.txtPwd);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Location = new System.Drawing.Point(0, 0);
            this.pnlLogin.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(847, 247);
            this.pnlLogin.TabIndex = 3;
            this.pnlLogin.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLogin_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "登录密码";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(516, 97);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(101, 37);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "确 定";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(324, 100);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(180, 31);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPwd_KeyUp);
            // 
            // WinStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 247);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pnlStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinStartup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "初始化";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinStartup_FormClosing);
            this.Load += new System.EventHandler(this.WinInitiate_Load);
            this.pnlStart.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Panel pnlStart;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label1;
    }
}