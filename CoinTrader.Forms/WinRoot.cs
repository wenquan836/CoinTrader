using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    public partial class WinRoot : Form
    {

        private List<Form> Windows = new List<Form>();
        public WinRoot()
        {
            InitializeComponent();
        }

        private void WinRoot_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void WinRoot_Enter(object sender, EventArgs e)
        {
            WindowManager.Instance.OpenWindow<WinMain>();
            this.Visible = false;
        }
    }
}
