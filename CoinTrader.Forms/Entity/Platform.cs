using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace CoinTrader.Forms.Entity
{
    /// <summary>
    /// 平台设置
    /// </summary>
    public class Platform
    {
        public Platform()
        {
            this.Currencies = new List<string>(new string[] { "BTC","ETH","LTC"});
        }

        public List<string> Currencies
        {
            get;set;
        }

        public bool ParseFromJson(JToken json)
        {
            try
            {
                if (json["Currencies"] != null)
                {
                    JArray arr = json["Currencies"] as JArray;

                    List<string> currencies = new List<string>();
                    foreach(var r in arr)
                    {
                        currencies.Add(r.ToString());
                    }

                    if(currencies.Count>0)
                    {
                        this.Currencies = currencies;
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
