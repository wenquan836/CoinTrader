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
    public partial class WinPassword : Form
    {
        public WinPassword()
        {
            InitializeComponent();

            this.txtOld.Visible = Config.Instance.HasPassword();
            this.lblOld.Visible = Config.Instance.HasPassword();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(!Config.Instance.HasPassword() || Config.Instance.ValidatePasswordAndLoad(this.txtOld.Text))
            {
                string newPwd = this.txtNew1.Text;
                string newConfirm = this.txtNew2.Text;

                if(newPwd.Length < 8 )
                {
                    MessageBox.Show("密码长度不得低于8位");
                    return;
                }

                if (newPwd == newConfirm)
                {
                    Config.Instance.SetPassword(newPwd);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("两次输入的密码不一致");
                }
            }
            else
            {
                MessageBox.Show("旧密码错误");
            }
        }

        private void WinPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(! Config.Instance.HasPassword())
            {
                WinMessage.Show(MessageType.Error, "请设置登录密码");
                e.Cancel = true;
            }
        }
    }
}
