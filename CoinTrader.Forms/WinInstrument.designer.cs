namespace CoinTrader.Forms
{
    partial class WinInstrument
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.instId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.category = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctVal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctMult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lever = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.minSz = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tickSz = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lotSz = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.instId,
            this.category,
            this.ctVal,
            this.ctMult,
            this.lever,
            this.minSz,
            this.tickSz,
            this.lotSz});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1747, 630);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // instId
            // 
            this.instId.Text = "产品";
            this.instId.Width = 208;
            // 
            // category
            // 
            this.category.Text = "手续费档位";
            this.category.Width = 189;
            // 
            // ctVal
            // 
            this.ctVal.Text = "合约面值";
            this.ctVal.Width = 195;
            // 
            // ctMult
            // 
            this.ctMult.Text = "合约乘数";
            this.ctMult.Width = 149;
            // 
            // lever
            // 
            this.lever.Text = "最大杠杆倍数";
            this.lever.Width = 156;
            // 
            // minSz
            // 
            this.minSz.Text = "最小下单数量";
            this.minSz.Width = 269;
            // 
            // tickSz
            // 
            this.tickSz.Text = "价格精度";
            this.tickSz.Width = 190;
            // 
            // lotSz
            // 
            this.lotSz.Text = "下单数量精度";
            this.lotSz.Width = 195;
            // 
            // WinInstrument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1747, 630);
            this.Controls.Add(this.listView1);
            this.Name = "WinInstrument";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合约查询";
            this.Load += new System.EventHandler(this.WinInstrument_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader instId;
        private System.Windows.Forms.ColumnHeader category;
        private System.Windows.Forms.ColumnHeader ctVal;
        private System.Windows.Forms.ColumnHeader lever;
        private System.Windows.Forms.ColumnHeader ctMult;
        private System.Windows.Forms.ColumnHeader minSz;
        private System.Windows.Forms.ColumnHeader tickSz;
        private System.Windows.Forms.ColumnHeader lotSz;
    }
}