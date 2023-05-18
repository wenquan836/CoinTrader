using CoinTrader.Common.Util;

namespace CoinTrader.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigCrypto
    {
        public static string Key { get; set; }
 
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Encrypt(string val)
        {
            return EncryptWithKey(val, Key);
        }

        public static string EncryptWithKey(string val,string key)
        {
            if (string.IsNullOrEmpty(key))
                return val;
            return CryptoUtil.AESEncrypt(val, key, CryptoUtil.MD5(key).Substring(0,16));
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string Decrypt(string val)
        {
            return DecryptWithKey(val, Key);
        }

        public static string DecryptWithKey(string val,string key)
        {
            if (string.IsNullOrEmpty(key))
                return val;

            return CryptoUtil.AESDecrypt(val, key,  CryptoUtil.MD5(key).Substring(0,16));
        }

    }
}
