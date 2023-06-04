using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager; 

namespace CoinTrader.Strategies
{
    /// <summary>
    /// 资金管理策略基类
    /// </summary>
    public abstract class FundsStrategyBase : StrategyBase
    {
        protected Wallet wallet;

        protected string Currency { get; set; }
 
        public override bool Init(string instId)
        {
            Currency = instId;
            if (!base.Init(instId)) return false;
            wallet = new Wallet(Currency);

            return true;
        }


        /// <summary>
        /// 资金划转,目前只支持交易账户、资金账户划转
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
