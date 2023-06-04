using CoinTrader.Forms.Strategies;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Manager;
using CoinTrader.Strategies;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    public partial class WinEmulatorGuid : Form
    {

        StrategyGroup group;
        public WinEmulatorGuid()
        {
            InitializeComponent();
        }

        private void ReloadInstruments()
        {
            List<InstrumentBase> instruments = null;

            switch (this.group.groupType)
            {
                case StrategyType.Swap:
                    instruments = InstrumentManager.SwapInstrument.GetInstrumentList();

                    break;
                case StrategyType.Spot:
                    instruments = InstrumentManager.SpotInstrument.GetInstrumentList();

                    break;
            }

            cmbInstruments.Items.Clear();
            foreach (var inst in instruments)
            {
                cmbInstruments.Items.Add(inst.InstrumentId);
            }

            cmbInstruments.SelectedIndex = 0;
        }


        public void SetStrategyGroup(StrategyGroup group)
        {
            this.group = group;
             
            ReloadInstruments();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            var win = WindowManager.Instance.OpenWindow<WinEmulator>();
            win.SetStrategyGroup(this.group, this.cmbInstruments.Text);
            this.Close();
        }
    }
}
