using CoinTrader.OKXCore.Entity;
using System.Diagnostics;

namespace CoinTrader.Forms
{ 
    /// <summary>
    /// 稳定币结余,虚拟钱包
    /// </summary>
    public class USDXWallet : Wallet
    {
        private USDXWallet(string currency): base(currency){}

        private static USDXWallet _instance = null;

        /// <summary>
        /// 创建稳定币虚拟钱包
        /// </summary>
        /// <param name="anchorCurrency"></param>
        public static void CreateInstance(string anchorCurrency)
        {
            _instance = new USDXWallet(anchorCurrency);
        }

        public static USDXWallet Instance
        {
            get
            {
                Debug.Assert(_instance != null);
                return _instance;
            }
        }
    }
}
