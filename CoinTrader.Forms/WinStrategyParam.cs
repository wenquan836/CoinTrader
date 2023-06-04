using CoinTrader.Forms.Control;
using CoinTrader.Forms.Strategies;
using CoinTrader.Strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    public partial class WinStrategyParam : Form
    {
        public event Action OnSave;


        private StrategyBase strategy = null;

        private List<ParamView> Views = new List<ParamView>();

        public WinStrategyParam()
        {
            InitializeComponent();
        }


        public void SetStrategy(StrategyBase strategy)
        {
            this.strategy = strategy;
            var type = strategy.GetType();
            var attr = type.GetCustomAttributes(typeof(StrategyAttribute), false);

            if(attr.Length > 0)
            {
                var behaviorAttr = attr[0] as StrategyAttribute;
                this.Text = strategy.InstId.ToUpper() + behaviorAttr.Name + "参数设置";
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(var p in properties)
            {
                var attrParam = p.GetCustomAttribute(typeof(StrategyParameterAttribute)) as StrategyParameterAttribute;

                if (attrParam == null)
                    continue;

                var control = new ParamView();
                control.SetProperty(strategy, p);
                control.SetOnChangedCallback(this.OnParamChanged);
                this.flowLayoutPanel1.Controls.Add(control);
                this.Views.Add(control);
            }

            foreach(ParamView v in this.Views)
            {
                this.OnParamChanged(v);
            }
        }

        private void OnParamChanged(ParamView view)
        {
            foreach(var v in this.Views)
            {
                if (v == view)
                    continue;

                if(!string.IsNullOrEmpty( v.Depend) && v.Depend == view.Property.Name)
                {
                    object val = view.GetValue();
                    if (val != null)
                    {
                        object dval = v.DependValue;
                        Type t = view.Property.PropertyType;

                        if (string.Equals(val.ToString(), dval.ToString())) // ？？？字符串作为比较的中间值是否靠谱?
                        {
                            v.Visible = true;
                        }
                        else
                        {
                            v.Visible = false;
                        }
                    }
                }
            }
            //view.Property.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var c in this.flowLayoutPanel1.Controls)
            {
                ParamView view = c as ParamView;
                string msg = view.ValidateValues();

                if(!string.IsNullOrEmpty(msg))
                {
                    MessageBox.Show(msg);
                    return;
                }
            }

            foreach (var c in this.flowLayoutPanel1.Controls)
            {
                ParamView view = c as ParamView;
                try
                {
                    view.Save();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            string errMsg = this.strategy.SaveConfig();

            if(!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }

            this.OnSave?.Invoke();
            strategy.OnParamaterChanged();
            this.Close();
        }
    }
}
