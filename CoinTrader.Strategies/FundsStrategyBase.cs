using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Strategies
{
    /// <summary>
    /// 资金管理策略基类
    /// </summary>
    public abstract class FundsStrategyBase : StrategyBase
    {
        protected Wallet wallet;

        protected string Currency { get; set; }
 
        public override void Init(string instId)
        {
            base.Init(instId);
            wallet = new Wallet(Currency);
        }


        /// <summary>
        /// 资金划转
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="amount"></param>
        protected void Transfer(BalanceType from, BalanceType to, decimal amount)
        {
            ///不支持模拟账户的划转
            if (AccountManager.IsSimulated)
                return;

            AssetsManager.Instance.Transfer(Currency, from, to, amount);
        }
    }
}
