using System;
using CoinTrader.OKXCore.Enum;
using CoinTrader.Strategies;

namespace CoinTrader.Forms.Strategies
{
    [Strategy(Name = "稳定币数量控制")]
    public class UsdxFundsStrategy : FundsStrategyBase
    {
        [StrategyParameter(Name = "交易账户持币数量", Min = 0, Max = 9999999999)]
        public decimal TradingHold{ get; set; }

        [StrategyParameter(Name = "差额阈值", Min = 1, Max = 1000000,Intro ="差额大于这个数时启动划转")]
        public decimal Threshold { get; set; } = 1;

 
        /// <summary>
        /// 每个定时器周期检查
        /// </summary>
        /// <param name="deltaTime"></param>
        protected override void OnTimer(int deltaTime)
        {
            var diff = wallet.AvailableInTrading + wallet.FrozenInTrading - TradingHold;
            decimal amount = Math.Abs(diff);

            if (amount >= Threshold) //交易账户数量差额大于指定值
            {
                if (diff < 0 && wallet.AvailableInAccount < Threshold) //资金账户数量也不足了,无法从资金账户划转
                    return;

                BalanceType from = diff < 0 ? BalanceType.Account : BalanceType.Trading;
                BalanceType to = diff < 0 ? BalanceType.Trading : BalanceType.Account;

                if (diff < 0)
                    amount = Math.Min(Math.Abs(diff), wallet.AvailableInAccount);

                this.Executing = true;
                Transfer(from, to, amount);
                Wait(1000);//等待一秒以同步余额信息
                this.Executing = false;
            }
        }
    }
}
