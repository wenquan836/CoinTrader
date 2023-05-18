using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Util
{
    public class CryptoUtil
    {

        /// <summary>
        /// 生成SHA256
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <returns></returns>
        public static string SHA256(string source)
        {
            if(source == null) throw new ArgumentNullException("source");
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }

            return builder.ToString();
        } 

        /// <summary>
        /// 获得MD5字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string MD5(string source)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            //try
            //{
                inputBye = Encoding.UTF8.GetBytes(source);
            //}
            //catch (Exception ex)
            //{
           //     inputBye = Encoding.GetEncoding("GB2312").GetBytes(source);
            //}
            outputBye = m5.ComputeHash(inputBye);
            m5.Clear();
            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToLower();
            return retStr;
        }

        public static string AESEncrypt(string str, string key,string iv)
        {
            Byte[] keyArray = Encoding.UTF8.GetBytes(key);
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
            var rijndael = new System.Security.Cryptography.RijndaelManaged();
            //rijndael.KeySize = keyArray.Length;
            rijndael.Key = keyArray;
            rijndael.Mode = System.Security.Cryptography.CipherMode.CBC;
            rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            rijndael.IV = System.Text.Encoding.UTF8.GetBytes(iv);
            System.Security.Cryptography.ICryptoTransform cTransform = rijndael.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string AESDecrypt(string str, string key, string iv)
        {
            Byte[] keyArray = Encoding.UTF8.GetBytes(key);
            Byte[] toDecryptArray = Convert.FromBase64String(str);


            var rijndael = new System.Security.Cryptography.RijndaelManaged();
            //rijndael.KeySize = keyArray.Length;
            rijndael.Key = keyArray;
            rijndael.Mode = System.Security.Cryptography.CipherMode.CBC;
            rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            rijndael.IV = System.Text.Encoding.UTF8.GetBytes(iv);
            System.Security.Cryptography.ICryptoTransform cTransform = rijndael.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
