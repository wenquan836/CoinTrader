using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// API调用的结果
    /// </summary>
    public struct APIResult
    {
        public int code;
        public bool success => code == 0;
        public string message;
        public JToken data;

    }
}
