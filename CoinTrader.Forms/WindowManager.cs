using CoinTrader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    class WindowManager
    {
        private List<Form> Windows = new List<Form>();



        public T OpenWindow<T>() where T : Form, new()
        {
            var win = new T();
            this.Windows.Add(win);
            win.Show();
            win.FormClosed += Win_FormClosed;
            return win;
        }

        public void CloseAll()
        {
            while(this.Windows.Count > 0)
            {
                var w = this.Windows[0];
                w.Close();
            }
        }

        private void Win_FormClosed(object sender, FormClosedEventArgs e)
        {
            var win = sender as Form;
            this.Windows.Remove(win);

            if (this.Windows.Count == 0)
            {
                // this.Root.Close();
                Logger.Instance.Close();
                Application.Exit();
            }
        }

        private static WindowManager _instance = null;

        public static WindowManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WindowManager();
                }

                return _instance;
            }
        }
    }
}
