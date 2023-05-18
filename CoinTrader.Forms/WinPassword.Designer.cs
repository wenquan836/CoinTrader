namespace CoinTrader.Forms
{
    partial class WinPassword
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
            this.txtOld = new System.Windows.Forms.TextBox();
            this.txtNew1 = new System.Windows.Forms.TextBox();
            this.txtNew2 = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblOld = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOld
            // 
            this.txtOld.Location = new System.Drawing.Point(229, 46);
            this.txtOld.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtOld.Name = "txtOld";
            this.txtOld.PasswordChar = '*';
            this.txtOld.Size = new System.Drawing.Size(380, 31);
            this.txtOld.TabIndex = 0;
            this.txtOld.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtNew1
            // 
            this.txtNew1.Location = new System.Drawing.Point(229, 103);
            this.txtNew1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtNew1.Name = "txtNew1";
            this.txtNew1.PasswordChar = '*';
            this.txtNew1.Size = new System.Drawing.Size(380, 31);
            this.txtNew1.TabIndex = 1;
            // 
            // txtNew2
            // 
            this.txtNew2.Location = new System.Drawing.Point(229, 161);
            this.txtNew2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.txtNew2.Name = "txtNew2";
            this.txtNew2.PasswordChar = '*';
            this.txtNew2.Size = new System.Drawing.Size(380, 31);
            this.txtNew2.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(255, 233);
            this.btnOk.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(189, 66);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblOld
            // 
            this.lblOld.AutoSize = true;
            this.lblOld.Location = new System.Drawing.Point(123, 54);
            this.lblOld.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(73, 21);
            this.lblOld.TabIndex = 4;
            this.lblOld.Text = "旧密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "新密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 170);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "新密码确认";
            // 
            // WinPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 345);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblOld);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtNew2);
            this.Controls.Add(this.txtNew1);
            this.Controls.Add(this.txtOld);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密码设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinPassword_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOld;
        private System.Windows.Forms.TextBox txtNew1;
        private System.Windows.Forms.TextBox txtNew2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblOld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}