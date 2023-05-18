namespace CoinTrader.Forms
{
    partial class WinCopyConfig
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
            this.rdoSpot = new System.Windows.Forms.RadioButton();
            this.rdoSwap = new System.Windows.Forms.RadioButton();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cklTarget = new System.Windows.Forms.CheckedListBox();
            this.btnClr = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cklFiles = new System.Windows.Forms.CheckedListBox();
            this.lblCoping = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoSpot
            // 
            this.rdoSpot.AutoSize = true;
            this.rdoSpot.Location = new System.Drawing.Point(177, 39);
            this.rdoSpot.Name = "rdoSpot";
            this.rdoSpot.Size = new System.Drawing.Size(77, 25);
            this.rdoSpot.TabIndex = 0;
            this.rdoSpot.Text = "现货";
            this.rdoSpot.UseVisualStyleBackColor = true;
            this.rdoSpot.CheckedChanged += new System.EventHandler(this.rdoSwap_CheckedChanged);
            // 
            // rdoSwap
            // 
            this.rdoSwap.AutoSize = true;
            this.rdoSwap.Checked = true;
            this.rdoSwap.Location = new System.Drawing.Point(37, 39);
            this.rdoSwap.Name = "rdoSwap";
            this.rdoSwap.Size = new System.Drawing.Size(77, 25);
            this.rdoSwap.TabIndex = 0;
            this.rdoSwap.TabStop = true;
            this.rdoSwap.Text = "合约";
            this.rdoSwap.UseVisualStyleBackColor = true;
            this.rdoSwap.CheckedChanged += new System.EventHandler(this.rdoSwap_CheckedChanged);
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplate.FormattingEnabled = true;
            this.cmbTemplate.Location = new System.Drawing.Point(92, 93);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(234, 29);
            this.cmbTemplate.TabIndex = 1;
            this.cmbTemplate.SelectedIndexChanged += new System.EventHandler(this.cmbTemplate_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "模板";
            // 
            // cklTarget
            // 
            this.cklTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cklTarget.FormattingEnabled = true;
            this.cklTarget.IntegralHeight = false;
            this.cklTarget.Location = new System.Drawing.Point(3, 27);
            this.cklTarget.Name = "cklTarget";
            this.cklTarget.Size = new System.Drawing.Size(509, 544);
            this.cklTarget.TabIndex = 3;
            // 
            // btnClr
            // 
            this.btnClr.Location = new System.Drawing.Point(876, 551);
            this.btnClr.Name = "btnClr";
            this.btnClr.Size = new System.Drawing.Size(88, 35);
            this.btnClr.TabIndex = 4;
            this.btnClr.Text = "不选";
            this.btnClr.UseVisualStyleBackColor = true;
            this.btnClr.Click += new System.EventHandler(this.btnClr_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(876, 510);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(88, 35);
            this.btnAll.TabIndex = 5;
            this.btnAll.Text = "全选";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(392, 30);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(160, 54);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cklTarget);
            this.groupBox1.Location = new System.Drawing.Point(358, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 574);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "目标币种";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCoping);
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 603);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(969, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // cklFiles
            // 
            this.cklFiles.FormattingEnabled = true;
            this.cklFiles.IntegralHeight = false;
            this.cklFiles.Location = new System.Drawing.Point(11, 162);
            this.cklFiles.Name = "cklFiles";
            this.cklFiles.Size = new System.Drawing.Size(334, 424);
            this.cklFiles.TabIndex = 4;
            // 
            // lblCoping
            // 
            this.lblCoping.AutoSize = true;
            this.lblCoping.Location = new System.Drawing.Point(724, 43);
            this.lblCoping.Name = "lblCoping";
            this.lblCoping.Size = new System.Drawing.Size(214, 21);
            this.lblCoping.TabIndex = 7;
            this.lblCoping.Text = "正在复制中.........";
            this.lblCoping.Visible = false;
            // 
            // WinCopyConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 703);
            this.Controls.Add(this.cklFiles);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnClr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbTemplate);
            this.Controls.Add(this.rdoSwap);
            this.Controls.Add(this.rdoSpot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinCopyConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置复制";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoSpot;
        private System.Windows.Forms.RadioButton rdoSwap;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox cklTarget;
        private System.Windows.Forms.Button btnClr;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox cklFiles;
        private System.Windows.Forms.Label lblCoping;
    }
}