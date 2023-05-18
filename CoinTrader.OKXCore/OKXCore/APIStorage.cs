using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  CoinTrader.OKXCore
{
    public static class APIStorage
    {
        private static API api;
        internal static API APIInfo => api;

        public static void UpdateAPI(string secretKey, string apiKey, string passphrase, bool isSimulated)
        {
            api.ApiKey = apiKey;
            api.Passphrase = passphrase;
            api.SecretKey = secretKey;
            api.IsSimulated = isSimulated;
        }
    }
}
