
using CoinTrader.Forms.Strategies;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.IO;
using CoinTrader.OKXCore.Manager;
using CoinTrader.Strategies;

namespace CoinTrader.Forms
{

    enum ConfigType
    {
        Spot,
        Swap
    }

    public partial class WinCopyConfig : Form
    {
        public WinCopyConfig()
        {
            InitializeComponent();
            LoadList();
        }


        void LoadList()
        {
            if (this.rdoSpot.Checked)
                this.LoadList(ConfigType.Spot);

            if (this.rdoSwap.Checked)
                this.LoadList(ConfigType.Swap);
        }

        void LoadList(ConfigType type)
        {
            this.cmbTemplate.Items.Clear();
            this.cklTarget.Items.Clear();
            this.cklFiles.Items.Clear();

            switch (type)
            {
                case ConfigType.Spot:
                    var listSpot = InstrumentManager.SpotInstrument.GetAllInstrument();

                    foreach (var kv in listSpot)
                    {
                        cmbTemplate.Items.Add(kv.Value.InstrumentId);
                        this.cklTarget.Items.Add(kv.Value.InstrumentId);
                    }
                    break;
                case ConfigType.Swap:

                    var listSwap = InstrumentManager.SwapInstrument.GetAllInstrument();
                    foreach (var kv in listSwap)
                    {
                        cmbTemplate.Items.Add(kv.Value.InstrumentId);
                        this.cklTarget.Items.Add(kv.Value.InstrumentId);
                    }
                    break;
            }
        }

        private void rdoSwap_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadList();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string select = this.cmbTemplate.SelectedItem != null? this.cmbTemplate.SelectedItem.ToString() :"";

            int targets = this.cklTarget.CheckedItems.Count;
            int files = this.cklFiles.CheckedItems.Count;
            string type = rdoSwap.Checked ? "合约" : "现货";

            if (string.IsNullOrEmpty(select))
            {
                MessageBox.Show("请选择需要拷贝的模板");
                return;
            }

            if (cklFiles.Items.Count == 0)
            {
                MessageBox.Show(String.Format("{0}{1}下没有文件可复制", type, select));
                return;
            }            
            
            if (files == 0)
            {
                MessageBox.Show("请选择需要拷贝的文件");
                return;
            }

            if (targets == 0)
            {
                MessageBox.Show("请选择目标币种");
                return;
            }





            string text = string.Format("将拷贝{0}的配置文件覆盖到{1}个目标币种",select,targets);

            if(MessageBox.Show(text, type + "配置拷贝", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.BeginCopy();
            }
        }


        private async void BeginCopy()
        {
            List<string> sourceFileNames = new List<string>();
            List<string> targets = new List<string>();
            string source = this.cmbTemplate.SelectedItem.ToString();

            foreach(var s in this.cklFiles.CheckedItems)
            {
                sourceFileNames.Add(s.ToString());
            }

            foreach (var s in this.cklTarget.CheckedItems)
            {
                targets.Add(s.ToString());
            }


            this.btnOk.Enabled = false;
            this.cklFiles.Enabled = false;
            this.btnAll.Enabled = false;
            this.btnClr.Enabled = false;
            this.cklTarget.Enabled = false;
            this.rdoSpot.Enabled = false;
            this.rdoSwap.Enabled = false;
            this.cmbTemplate.Enabled = false;
            this.lblCoping.Visible = true;

            var result = await( StrategyConfig.CopyConfigs(source, sourceFileNames, targets));

            if(result.success)
            {
                this.Close();
                MessageBox.Show("复制成功。","成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("复制文件失败" + result.error, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);


                this.btnOk.Enabled =  
                this.cklFiles.Enabled = 
                this.btnAll.Enabled =  
                this.btnClr.Enabled =  
                this.cklTarget.Enabled = 
                this.rdoSpot.Enabled =  
                this.rdoSwap.Enabled =  
                this.cmbTemplate.Enabled = true;
                this.lblCoping.Visible = false;
            }
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            for(int i = 0;i< this.cklTarget.Items.Count;i++)
            {
                this.cklTarget.SetItemChecked(i, false);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.cklTarget.Items.Count; i++)
            {
                this.cklTarget.SetItemChecked(i, true);
            }
        }

        string GetcurrentSourcePath()
        {
            string path = Path.Combine(StrategyConfig.GetDirectory(), this.cmbTemplate.SelectedItem.ToString());
            return path;
        }

        private void cmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cklFiles.Items.Clear();

            if(this.cmbTemplate.SelectedItem == null)
            {
                return;
            }

            string path = this.GetcurrentSourcePath();


            if(Directory.Exists(path))
            {
                var dirInfo = new DirectoryInfo(path);

                var files =  dirInfo.GetFiles();
                int index = 0;
                foreach(var f in files)
                {
                    this.cklFiles.Items.Add(f.Name);
                    this.cklFiles.SetItemChecked(index, true);
                    index++;
                }
            }
        }
    }
}
