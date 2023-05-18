using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Entity
{
    /// <summary>
    /// 虚拟钱包， 把所有地方的数量汇聚在这里、资金账户、交易账户， 冻结、可用
    /// </summary>
    public class Wallet
    {
        private string currency;
        public Wallet(string currency)
        {
            this.currency = currency;
        }

        /// <summary>
        /// 交易账户可用额度
        /// </summary>
        public decimal AvailableInTrading  => AssetsManager.Instance.GetBalance(BalanceType.Trading, BalanceAmountType.Available, currency);

        /// <summary>
        /// 交易账户冻结额度
        /// </summary>
        public decimal FrozenInTrading => AssetsManager.Instance.GetBalance(BalanceType.Trading, BalanceAmountType.Frozen, currency);

        /// <summary>
        /// 资金账户可用额度
        /// </summary>
        public decimal AvailableInAccount => AssetsManager.Instance.GetBalance(BalanceType.Account, BalanceAmountType.Available, currency);

        /// <summary>
        /// 资金账户冻结额度
        /// </summary>
        public decimal FrozenInAccount => AssetsManager.Instance.GetBalance(BalanceType.Account, BalanceAmountType.Frozen, currency);

        /// <summary>
        /// 总计
        /// </summary>
        public decimal Total => AvailableInAccount + FrozenInAccount + AvailableInTrading + FrozenInTrading;

    }
}
