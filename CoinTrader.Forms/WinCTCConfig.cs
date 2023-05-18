using CoinTrader.Forms.Event;
using CoinTrader.Forms.Entity;
using CoinTrader.Common;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Event;
using CoinTrader.OKXCore.Manager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoinTrader.Forms
{
    public partial class WinCTCConfig : Form
    {
        public WinCTCConfig()
        {
            InitializeComponent();

            config = Config.Instance;
        }


        private bool IsUpdated = false;
        public bool ForceUpdate
        {
            get; set;
        }



        private List<TextBox> RequireTexts = new List<TextBox>();
        private async void button1_Click(object sender, EventArgs e)
        {
            if(!this.CheckRequireTextbox())
            {
                return;
            }

            ApiInfo oldApi = Config.Instance.ApiInfo;
            var config = new Config();// Config.Instance;
            List<string> currencies = new List<string>();
            Account apiOwner;
            string text;

            try
            {
                config.UsdCoin = this.cbAnchor.Text;
                LoginAccount loginAccount = config.Account;
                ApiInfo api = config.ApiInfo;

                text = this.txtLoginName.Text.Trim();

                loginAccount.SetLoginName(text);

                text = this.txtApiKey.Text.Trim();

                if (text.Length < 8)
                    throw new Exception("API Key不得少于8位");

                if (text != Config.MaskString(oldApi.ApiKey))
                    api.ApiKey = text;
                else
                    api.ApiKey = oldApi.ApiKey;

                text = this.txtApiPassphrase.Text.Trim();

                if (text.Length < 6)
                    throw new Exception("API Passphrase不得少于6位");

                if (text != Config.MaskString(oldApi.Passphrase))
                    api.Passphrase = text;
                else
                    api.Passphrase = oldApi.Passphrase;

                text = this.txtApiSecretKey.Text.Trim();
                if (text.Length < 8)
                    throw new Exception("API SecretKey不得少于8位");
                if (text != Config.MaskString(oldApi.SecretKey))
                    api.SecretKey = text;
                else
                    api.SecretKey = oldApi.SecretKey;

                api.IsSimulated = rdoSimulated.Checked;

                //api.ApiAddress = this.txtWebSocket.Text.Trim();

                Platform platform = config.PlatformConfig;

                string currenciesString = this.txtCurrencies.Text.Trim().ToUpper();

                currenciesString = currenciesString.Replace("；", ";").Replace(" ", "");

                currencies = new List<string>(currenciesString.Split(',', ';'));

                if (currencies.Count < 1)
                    throw new Exception("请填写至少一种交易币种");

                if (currencies.Contains(config.UsdCoin))
                    throw new Exception("稳定币种不能出现在币种列表");

                platform.Currencies = currencies;

                apiOwner = AccountManager.GetAccountByAPI(api.SecretKey,api.ApiKey,api.Passphrase,api.IsSimulated);

                if (apiOwner == null)
                {
                    throw new Exception("API连接失败");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
                return;
            }

            try
            {
                config.SaveToFile();
            }
            catch (Exception ex)
            {
                WinMessage.Show(MessageType.Error, "保存配置失败" + ex.Message);
            }

            AccountManager.UpdateAccount(apiOwner);

            ///重新更新交易对
            await InstrumentManager.UpdateInstrumentAsync(config.UsdCoin);

            for (int i = currencies.Count - 1; i >= 0; i--)
            {
                string currency = currencies[i];
                if (string.IsNullOrEmpty(currency))
                {
                    currencies.RemoveAt(i);
                }
                else
                {

                    string instrumentId = string.Format("{0}-{1}", currency, config.UsdCoin).ToUpper();
                    if (!InstrumentManager.SpotInstrument.HasInstrument(instrumentId))
                    {
                        throw new Exception("不存在交易对" + instrumentId);
                    }
                }
            }

            var apiInfo = config.ApiInfo;
            APIStorage.UpdateAPI(apiInfo.SecretKey, apiInfo.ApiKey, apiInfo.Passphrase, apiInfo.IsSimulated);
            //todo如果修改了API需要中断socket重新连接

            Config.UpdateConfig(config);

            IsUpdated = true;
            this.DialogResult = DialogResult.OK;
            this.Close();

            EventCenter.Instance.Send(EventNames.ConfigChanged, null);
        }

        Config config = null;
        private void WinConfig_Load(object sender, EventArgs e)
        {
            config = Config.Instance;

            LoginAccount account = config.Account;
            ApiInfo api = config.ApiInfo;
            this.txtLoginName.Text = account.LoginName;
 
            this.txtApiKey.Text = Config.MaskString(api.ApiKey);
            this.txtApiPassphrase.Text = Config.MaskString( api.Passphrase);
            this.txtApiSecretKey.Text = Config.MaskString(api.SecretKey);
            this.txtWebSocket.Text = api.ApiAddress;

            this.cbAnchor.Text = config.UsdCoin;

            this.rdoSimulated.Checked = config.ApiInfo.IsSimulated;

            this.txtCurrencies.Text = string.Join(";",config.PlatformConfig.Currencies);

            RequireTexts.Add(txtApiPassphrase);
            RequireTexts.Add(txtApiKey);
            RequireTexts.Add(txtApiSecretKey);
            RequireTexts.Add(txtLoginName);

            RequireTexts.Add(txtCurrencies);
            RequireTexts.Add(txtWebSocket);


        }

        private bool CheckRequireTextbox()
        {
            bool hasEmepy = false;
            foreach (var t in RequireTexts)
            {
                if (string.IsNullOrEmpty(t.Text))
                {
                    t.BackColor = Color.FromArgb(255, 190, 190);

                    hasEmepy = true;
                }
                else
                {
                    t.BackColor = Color.White;
                }
            }

            return !hasEmepy;
        }

        private void cbAnchor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CheckMarketChange()
        {

        }

        private void rdoCtc_Click(object sender, EventArgs e)
        {
            this.CheckMarketChange();
        }

        private void WinCTCConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ForceUpdate && !IsUpdated)
            {
                WinMessage.Show(MessageType.Error, "请设置完整配置");
                e.Cancel = true;
            }
        }
    }
}
