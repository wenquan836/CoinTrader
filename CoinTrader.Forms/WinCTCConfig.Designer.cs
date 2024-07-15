namespace CoinTrader.Forms
{
    partial class WinCTCConfig
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.txtCurrencies = new System.Windows.Forms.TextBox();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAnchor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoSimulated = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.txtApiSecretKey = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtApiPassphrase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWebSocket = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.txtCurrencies);
            this.groupBox1.Controls.Add(this.txtLoginName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(36, 26);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(590, 184);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "账户设置";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(496, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 65);
            this.button2.TabIndex = 7;
            this.button2.Text = "选择";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(51, 105);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(58, 24);
            this.label24.TabIndex = 6;
            this.label24.Text = "币种";
            // 
            // txtCurrencies
            // 
            this.txtCurrencies.Location = new System.Drawing.Point(147, 98);
            this.txtCurrencies.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCurrencies.Multiline = true;
            this.txtCurrencies.Name = "txtCurrencies";
            this.txtCurrencies.Size = new System.Drawing.Size(340, 65);
            this.txtCurrencies.TabIndex = 5;
            this.txtCurrencies.Text = "BTC;LTC;EOS;BCH;ETH;ETC;OKB";
            this.toolTip1.SetToolTip(this.txtCurrencies, "每个币种之间用分号（;）分隔， 比如BTC;ETH;LTC");
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(147, 37);
            this.txtLoginName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(340, 35);
            this.txtLoginName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "登录名";
            // 
            // cbAnchor
            // 
            this.cbAnchor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnchor.FormattingEnabled = true;
            this.cbAnchor.Items.AddRange(new object[] {
            "USDT",
            "USDC"});
            this.cbAnchor.Location = new System.Drawing.Point(168, 50);
            this.cbAnchor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAnchor.Name = "cbAnchor";
            this.cbAnchor.Size = new System.Drawing.Size(319, 32);
            this.cbAnchor.TabIndex = 2;
            this.cbAnchor.SelectedIndexChanged += new System.EventHandler(this.cbAnchor_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(79, 58);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 24);
            this.label8.TabIndex = 0;
            this.label8.Text = "稳定币";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoSimulated);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.txtApiSecretKey);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtApiPassphrase);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtApiKey);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(681, 26);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(715, 343);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "API设置";
            // 
            // rdoSimulated
            // 
            this.rdoSimulated.AutoSize = true;
            this.rdoSimulated.Location = new System.Drawing.Point(432, 224);
            this.rdoSimulated.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoSimulated.Name = "rdoSimulated";
            this.rdoSimulated.Size = new System.Drawing.Size(113, 28);
            this.rdoSimulated.TabIndex = 3;
            this.rdoSimulated.Text = "模拟盘";
            this.rdoSimulated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoSimulated.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(208, 224);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(89, 28);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "实盘";
            this.radioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // txtApiSecretKey
            // 
            this.txtApiSecretKey.Location = new System.Drawing.Point(211, 154);
            this.txtApiSecretKey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtApiSecretKey.Name = "txtApiSecretKey";
            this.txtApiSecretKey.Size = new System.Drawing.Size(434, 35);
            this.txtApiSecretKey.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 160);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 24);
            this.label7.TabIndex = 0;
            this.label7.Text = "SecretKey";
            // 
            // txtApiPassphrase
            // 
            this.txtApiPassphrase.Location = new System.Drawing.Point(211, 98);
            this.txtApiPassphrase.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtApiPassphrase.Name = "txtApiPassphrase";
            this.txtApiPassphrase.Size = new System.Drawing.Size(434, 35);
            this.txtApiPassphrase.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 102);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 24);
            this.label6.TabIndex = 0;
            this.label6.Text = "API密码";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(211, 37);
            this.txtApiKey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(434, 35);
            this.txtApiKey.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 42);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "ApiKey";
            // 
            // txtWebSocket
            // 
            this.txtWebSocket.Location = new System.Drawing.Point(1773, 395);
            this.txtWebSocket.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWebSocket.Name = "txtWebSocket";
            this.txtWebSocket.Size = new System.Drawing.Size(434, 35);
            this.txtWebSocket.TabIndex = 1;
            this.txtWebSocket.Text = "wss://ws.okx.com:8443/ws/v5/public";
            this.txtWebSocket.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1598, 401);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 24);
            this.label10.TabIndex = 0;
            this.label10.Text = "WebSocket";
            this.label10.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 411);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(466, 123);
            this.button1.TabIndex = 2;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbAnchor);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(36, 232);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox4.Size = new System.Drawing.Size(590, 137);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "稳定币设置";
            // 
            // WinCTCConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1425, 569);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtWebSocket);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinCTCConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinCTCConfig_FormClosing);
            this.Load += new System.EventHandler(this.WinConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtApiSecretKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtApiPassphrase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbAnchor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWebSocket;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtCurrencies;
        private System.Windows.Forms.RadioButton rdoSimulated;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button2;
    }
}