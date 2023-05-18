using CoinTrader.Forms.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using CoinTrader.Common.Util;
using Newtonsoft.Json;
 using System.Windows.Forms;
 
using CoinTrader.Common;

namespace CoinTrader.Forms
{
    public class Config
    {
        public static bool HasCryptoConfigFile()
        {
            return File.Exists(GetCryptoConfigPath());
        }

        public static string GetCryptoConfigPath()
        {
            string configName = "Config_Crypto_V5.json";

            return Path.Combine(Application.StartupPath, configName);
        }

        public static string GetNormalConfigPath()
        {
            string configName = "Config_V5.json";
            return Path.Combine(Application.StartupPath, configName);
        }
  
         public LoginAccount Account { get; private set; }
        public ApiInfo ApiInfo { get; private set; }
        public Platform PlatformConfig { get; private set; }

        public List<ApiInfo> SubAccountApiInfos { get; private set; }

        public string CommonRemark { get; set; }


       /// <summary>
       /// 稳定币
       /// </summary>
        public string UsdCoin { get; set; } = "USDT";

        public decimal ExchangeRate
        {
            get;set;
        }


        public Config()
        {
            this.ExchangeRate = 1;
            this.Account = new LoginAccount();
            this.ApiInfo = new ApiInfo();
            this.PlatformConfig = new Platform();
            this.SubAccountApiInfos = new List<ApiInfo>();
        }

        /// <summary>
        /// 重要字符串掩码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MaskString(string text)
        {
            if (text == null)
                text = "";

            if( text != null && text.Length > 4)
            {
                int len = text.Length;

                string s = text.Substring(0, 2);
                string e = text.Substring(len - 2, 2);
                string[] m = new string[len - 4] ;

                for (int i = 0; i < m.Length; i++)
                    m[i] = "*";

                return string.Format("{0}{1}{2}",s, string.Join("",m),e);
            }

            return text;
        }

        private static Config _instance = null;
        public static Config Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Config();

                return _instance;
            }
            set 
            {
                _instance = value; 
            }
        }

        public static bool UpdateConfig(Config cfg)
        {
            _instance = cfg; //存在多线程数据不一致问题
            return true;
        }

        public bool HasPassword()
        {
            return HasCryptoConfigFile();
        }


        public bool TryLoadFromFile()
        {

            string path = GetNormalConfigPath();
            bool crypto = false;
            if (HasCryptoConfigFile())
            {
                path = GetCryptoConfigPath();
                crypto = true;
            }

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                if (crypto)
                {
                    try
                    {
                        json = ConfigCrypto.Decrypt(json);//如果是加密的，就进行解密
                    }
                    catch
                    {
                        return false;
                    }
                }

                bool success = true;
                JToken resul = null;
                try
                {
                    resul = JToken.Parse(json);
                }
                catch (Exception ex)
                {
                    return false;
                }

                this.UsdCoin = resul["UsdCoin"] != null ? resul["UsdCoin"].Value<string>() : "USDT";
                this.ExchangeRate = resul["ExchangeRate"] != null ? resul["ExchangeRate"].Value<decimal>() : 1;
                success = success && this.Account.ParseFromJson(resul["Account"]);
                success = success && this.ApiInfo.ParseFromJson(resul["ApiInfo"]);
                success = success && this.PlatformConfig.ParseFromJson(resul["PlatformConfig"]);

                if (resul["SubAccountApiInfos"] != null)
                {
                    JArray subConfigs = resul["SubAccountApiInfos"] as JArray;


                    foreach (var sc in subConfigs)
                    {
                        var subApi = new ApiInfo();
                        subApi.ParseFromJson(sc);
                        this.SubAccountApiInfos.Add(subApi);
                    }
                }

                return success;
            }

            return false;
        }

        public void SaveToFile()
        {
            var json = JsonUtil.ObjectToJsonString(this);
            string path = GetNormalConfigPath();
            if (!string.IsNullOrEmpty(ConfigCrypto.Key))
            {
                path = GetCryptoConfigPath();
                json = ConfigCrypto.Encrypt(json);
            }

            File.WriteAllText(path, json);

            try
            {
                File.Delete(GetNormalConfigPath());
            }
            catch
            {

            }
        }

        /// <summary>
        /// 验证密码的有效性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ValidatePasswordAndLoad(string key)
        {
            if (!HasCryptoConfigFile())
                return true;

            string oldKey = ConfigCrypto.Key;

            key = this.HashPassword(key);
            ConfigCrypto.Key = key;

            bool success = this.TryLoadFromFile();

            if(!success)
            {
                ConfigCrypto.Key = oldKey;
                return false;
            }

            return true;
        }


        /// <summary>
        /// 返回密码的hash值
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private string HashPassword( string password)
        {
            if (password == null) password = "";
            return CryptoUtil.MD5(password);
        }

        public void SetPassword(string password, bool isRevert = false)
        {
            string newKey = this.HashPassword(password);

            string oldKey = ConfigCrypto.Key;

            ConfigCrypto.Key = newKey;

            try
            {
                this.SaveToFile();
            }
            catch(Exception ex)
            {
                if(!isRevert)
                {
                    this.SetPassword(oldKey, true); //还原
                }

                MessageBox.Show("重设登录密码错误:" + ex.Message);
            }
        }
    }
}
