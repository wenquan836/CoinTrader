using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.REST;
using CoinTrader.OKXCore.VO;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CoinTrader.OKXCore.Monitor
{
    [MonitorName(Name = "资金账户结余")]
    public class AssetsBalanceMonitor : RESTMonitor
    {
        Dictionary<string, BalanceVO> currentBalance = new Dictionary<string, BalanceVO>();
        Dictionary<string, BalanceVO> freeBalance = new Dictionary<string, BalanceVO>();

        public AssetsBalanceMonitor() : base(new AssetsBalance(),1000)
        {

        }

        /// <summary>
        /// 获取指定币种的余额信息
        /// </summary>
        /// <param name="ccy"></param>
        /// <returns></returns>
        public BalanceVO GetBalance(string ccy)
        {
            var defalut = default(BalanceVO);
            if (!string.IsNullOrEmpty(ccy))
            {
                ccy = ccy.ToUpper();
                if (currentBalance.TryGetValue(ccy, out defalut))
                    return defalut;
            }

            defalut.Currency = ccy;
            defalut.Avalible = 0;
            defalut.Frozen = 0;
            return defalut;
        }

        private void SwapBalance()
        {
            var tmp = currentBalance;
            currentBalance = freeBalance;
            freeBalance = tmp;
        }

        protected override void OnDataUpdate(JToken ret)
        {
            JArray data = ret as JArray;
            ParseDetails(data);
            this.Feed();
        }

        private void ParseDetails(JArray arr)
        {
            decimal avalible, frozen;

            BalanceVO balance = default;
            freeBalance.Clear();
            foreach (JToken item in arr)
            {
                string currency = item.Value<string>("ccy");

                string strValue = string.Empty;
                strValue = item.Value<string>("availBal");
                decimal.TryParse(strValue, out avalible);

                strValue = item.Value<string>("frozenBal");
                frozen = string.IsNullOrEmpty(strValue) ? 0 : decimal.Parse(strValue);
                balance.Currency = currency;
                balance.Frozen = frozen;
                balance.Avalible = avalible;
                freeBalance.Add(currency, balance);
             }

            SwapBalance();
        }
    }
}
