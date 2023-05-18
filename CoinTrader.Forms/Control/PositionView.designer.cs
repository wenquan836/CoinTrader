namespace CoinTrader.Forms.Control
{
    partial class PositionView
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.instId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.posSide = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.avgPx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.upl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lever = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.liqPx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.margin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mgnRatio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mmr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.interest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.instId,
            this.posSide,
            this.pos,
            this.avgPx,
            this.upl,
            this.lever,
            this.liqPx,
            this.margin,
            this.mgnRatio,
            this.mmr,
            this.cTime,
            this.mode,
            this.interest});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1868, 665);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // instId
            // 
            this.instId.Text = "币种";
            this.instId.Width = 160;
            // 
            // posSide
            // 
            this.posSide.Text = "方向";
            this.posSide.Width = 80;
            // 
            // pos
            // 
            this.pos.Text = "数量";
            this.pos.Width = 97;
            // 
            // avgPx
            // 
            this.avgPx.Text = "开仓均价";
            this.avgPx.Width = 121;
            // 
            // upl
            // 
            this.upl.Text = "收益";
            this.upl.Width = 108;
            // 
            // lever
            // 
            this.lever.Text = "杠杆倍数";
            this.lever.Width = 123;
            // 
            // liqPx
            // 
            this.liqPx.Text = "预估强平价";
            this.liqPx.Width = 125;
            // 
            // margin
            // 
            this.margin.Text = "保证金";
            this.margin.Width = 129;
            // 
            // mgnRatio
            // 
            this.mgnRatio.Text = "保证金率";
            this.mgnRatio.Width = 150;
            // 
            // mmr
            // 
            this.mmr.Text = "维持保证金";
            this.mmr.Width = 130;
            // 
            // cTime
            // 
            this.cTime.Text = "建立时间";
            this.cTime.Width = 226;
            // 
            // interest
            // 
            this.interest.Text = "利息";
            this.interest.Width = 159;
            // 
            // mode
            // 
            this.mode.Text = "模式";
            this.mode.Width = 69;
            // 
            // PositionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Name = "PositionView";
            this.Size = new System.Drawing.Size(1868, 665);
            this.Load += new System.EventHandler(this.PositionView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader instId;
        private System.Windows.Forms.ColumnHeader posSide;
        private System.Windows.Forms.ColumnHeader pos;
        private System.Windows.Forms.ColumnHeader avgPx;
        private System.Windows.Forms.ColumnHeader upl;
        private System.Windows.Forms.ColumnHeader lever;
        private System.Windows.Forms.ColumnHeader liqPx;
        private System.Windows.Forms.ColumnHeader margin;
        private System.Windows.Forms.ColumnHeader mgnRatio;
        private System.Windows.Forms.ColumnHeader mmr;
        private System.Windows.Forms.ColumnHeader cTime;
        private System.Windows.Forms.ColumnHeader interest;
        private System.Windows.Forms.ColumnHeader mode;
    }
}
