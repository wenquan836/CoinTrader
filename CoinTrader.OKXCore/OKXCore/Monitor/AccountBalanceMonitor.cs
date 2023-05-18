using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.Network;
using CoinTrader.OKXCore.VO;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CoinTrader.OKXCore.Monitor
{
    [MonitorName(Name = "交易账户结余")]
    public class AccountBalanceMonitor : WebSocketMonitorBase
    {
        bool updated = false;
        ConcurrentDictionary<string,BalanceVO> currentBalance = new ConcurrentDictionary<string, BalanceVO>();
         public AccountBalanceMonitor() : base(true)
        {
            this.Subscribe(WebsocketChannels.Channel_account, null);
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

        public override bool Effective
        {
            get => updated && base.Effective;
        }
 
        protected override void ProcessData(string channel, string instId, JArray datas)
        {
            if(channel == WebsocketChannels.Channel_account)
            {
                var arr = datas[0].Value<JArray>("details");
                var balanceVo = default(BalanceVO);
                decimal avalible;
                decimal frozen;

                foreach (JToken item in arr)
                {
                    balanceVo.Currency = item.Value<string>("ccy");

                    string strValue = string.Empty;
                    strValue = item.Value<string>("availBal");


                    //简单交易模式
                    if (string.IsNullOrEmpty(strValue))
                    {
                        strValue = item.Value<string>("availEq");//保证金模式?
                    }

                    decimal.TryParse(strValue, out avalible);

                    strValue = item.Value<string>("frozenBal");
                    frozen = string.IsNullOrEmpty(strValue) ? 0 : item.Value<decimal>("frozenBal");

                    balanceVo.Frozen = frozen;
                    balanceVo.Avalible = avalible;
                    currentBalance.AddOrUpdate(balanceVo.Currency, balanceVo, (k, v) => { return balanceVo; });
                 }

                //SwapBalance();
                this.updated = true;
                this.Feed();
            }

            base.ProcessData(channel, instId, datas);
        }
    }
}
