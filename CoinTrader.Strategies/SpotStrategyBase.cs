using CoinTrader.Common;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.REST;
using System;

namespace CoinTrader.Strategies
{
    /// <summary>
    /// 现货策略基类， 所有现货的都需要从这个类派生
    /// </summary>
    public abstract class SpotStrategyBase: TradeStrategyBase
    {
        protected Wallet wallet;
        protected InstrumentSpot instrument;

        protected override StrategyType StrategyType => StrategyType.Spot;

        public override bool Init(string instId)
        {
            if (!base.Init(instId)) return false;

            instrument = instrumentBase as InstrumentSpot;
            if(instrument == null )return false;

            wallet = new Wallet(instrumentBase.BaseCcy);

            return true;
        }

 


        /// <summary>
        /// 交易账户可用额度
        /// </summary>
        protected decimal AvailableInTrading => runtime.BaseBalance.Avalible;

        /// <summary>
        /// 交易账户冻结额度
        /// </summary>
        protected decimal FrozenInTrading => runtime.BaseBalance.Frozen;


        /// <summary>
        /// 最小可交易数量
        /// </summary>
        protected decimal MinSize => runtime.MinSize;

        /// <summary>
        /// 最小价格精度
        /// </summary>
        protected decimal TickSize=> runtime.TickSize;
    }
}
