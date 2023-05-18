using CoinTrader.Common;
using CoinTrader.Common.Interface;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Event;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.REST;
using CoinTrader.OKXCore.VO;
using System;
using System.Collections.Generic;

namespace CoinTrader.OKXCore.Manager
{
    /// <summary>
    /// 资产管理器
    /// </summary>
    public class AssetsManager:IEventListener
    {
        AssetsBalanceMonitor assetsMonitor = null;
        AccountBalanceMonitor accountMonitor = null;

        private AssetsManager()
        {
            var mgr = MonitorSchedule.Default;
            accountMonitor = new AccountBalanceMonitor();
            mgr.AddMonotor(accountMonitor);

            CheckAssetsMonitor();
        }

        private void CheckAssetsMonitor()
        {
            var mgr = MonitorSchedule.Default;

            if (APIStorage.APIInfo.IsSimulated)
            {
                if(assetsMonitor != null)
                {
                    mgr.RemoveMonitor(assetsMonitor);
                }
            }
            else
            {
                if (assetsMonitor == null)
                {
                    assetsMonitor = new AssetsBalanceMonitor();
                   
                }

                mgr.AddMonotor(assetsMonitor);
            }
        }

        public decimal GetBalance(BalanceType type, BalanceAmountType amountType, string ccy)
        {
            var balance = GetBalance(type, ccy);

            switch (amountType)
            {
                case BalanceAmountType.Available: return balance.Avalible;
                case BalanceAmountType.Frozen: return balance.Frozen;
            }

            return 0;
        }
        public BalanceVO GetBalance(BalanceType type, string ccy)
        {
            if (!string.IsNullOrEmpty(ccy))
            {
                switch (type)
                {
                    case BalanceType.Account:
                        if (assetsMonitor != null) return assetsMonitor.GetBalance(ccy);
                        break;
                    case BalanceType.Trading:
                        if (accountMonitor != null) return accountMonitor.GetBalance(ccy);
                        break;
                }
            }
            var ret = default(BalanceVO);
            ret.Currency = ccy;
            ret.Avalible = 0;
            ret.Frozen = 0;

            return ret;
        }

        /// <summary>
        /// 转移
        /// </summary>
        /// <param name="from">从</param>
        /// <param name="to">到</param>
        /// <param name="amount">数</param>
        public async void Transfer(string ccy, BalanceType from, BalanceType to, decimal amount)
        {
            if (from == to)
                return;

            if (amount <= 0)
                return;


            decimal transferAmount = amount;

            BalanceVO balance = default;

            switch (from)
            {
                case BalanceType.Account:
                    balance = GetBalance(BalanceType.Account, ccy);
                    break;
                case BalanceType.Trading:
                    balance = GetBalance(BalanceType.Trading, ccy);
                    break;
            }

            transferAmount = Math.Min(balance.Avalible, transferAmount);

            if (transferAmount > 0)
            {
                Transfer transferApi = new Transfer(ccy, from, to);
                transferApi.amt = transferAmount;
                var result = transferApi.ExecSync();

                if(result.success)
                {
                    EventCenter.Instance.Send(CoreEvent.BalanceTransfer, ccy.ToUpper());
                }
            }
        }

        public void Resync()
        {
            if (!APIStorage.APIInfo.IsSimulated && assetsMonitor != null)
            {
                this.assetsMonitor.ForceUpdate();
            }
        }

        private void OnConfigUpdated(object obj)
        {
            this.CheckAssetsMonitor();
        }
        public IEnumerable<EventListenItem> GetEvents()
        {
            return new EventListenItem[] {
                new EventListenItem(CoreEvent.ConfigChanged, OnConfigUpdated)
            };
        }

        private static AssetsManager _instance = null;
        public static AssetsManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new AssetsManager();
                }

                return _instance;
            }
        }
    }
}
