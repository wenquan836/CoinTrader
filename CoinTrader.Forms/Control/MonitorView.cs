using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTrader.OKXCore.Monitor;

namespace CoinTrader.Forms.Control
{
    public partial class MonitorView : UserControl
    {
        private MonitorBase _monitor = null;
        public MonitorBase monitor
        {
            get
            {
                return this._monitor;
            }
            set
            {
                this._monitor = value;

               
                string name = "";
                this.lblName.Text = "";
                this.lblState.ForeColor =  Color.Black;
                this.lblTime.Text = "";

                if (value != null)
                {
                    string customName = value.CustomName;
                    var type = value.GetType();

                    foreach(var attr in type.GetCustomAttributes(false))
                    {
                        if(attr is MonitorNameAttribute)
                        {
                            name = (attr as MonitorNameAttribute).Name;
                            break;
                        }
                    }
                    this.lblName.Text = string.Format("{0}{1}",name,customName);
                }
            }
        }

        public MonitorView()
        {
            InitializeComponent();
        }

        private void MonitorView_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(this._monitor != null)
            {
                this.lblState.ForeColor = _monitor.Effective ? Color.Red : Color.Black;
                this.lblTime.Text = _monitor.LastUpdate.ToLongTimeString();
            }
            else
            {
                this.lblState.ForeColor = Color.Black;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
