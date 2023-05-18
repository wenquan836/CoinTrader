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

        public override void Init(string instId)
        {
            base.Init(instId);

            instrument = instrumentBase as InstrumentSpot;

            if(instrument == null )
            {

            }

            wallet = new Wallet(instrumentBase.BaseCcy);
        }

 
        /// <summary>
        /// 交易币种名称
        /// </summary>
        protected string BaseCurrency => dataProvider.BaseCurrency;

        /// <summary>
        /// 计价币种名称
        /// </summary>
        protected string QuoteCurrency => dataProvider.QuoteCurrency;
 
        /// <summary>
        /// 交易账户可用额度
        /// </summary>
        protected decimal AvailableInTrading => wallet.AvailableInTrading;

        /// <summary>
        /// 交易账户冻结额度
        /// </summary>
        protected decimal FrozenInTrading => wallet.FrozenInTrading;

        /// <summary>
        /// 资金账户可用额度
        /// </summary>
        protected decimal AvailableInAccount => wallet.AvailableInAccount;

        /// <summary>
        /// 资金账户冻结额度
        /// </summary>
        protected decimal FrozenInAccount => wallet.FrozenInAccount;

        /// <summary>
        /// 最小可交易数量
        /// </summary>
        protected decimal MinSize => instrument.MinSize;

        /// <summary>
        /// 最小价格精度
        /// </summary>
        protected decimal TickSize=>instrument.TickSize;

        /// <summary>
        /// 从币币市场买入,以盘口价直接买入
        /// </summary>
        /// <param name="amount">数量</param>
        protected override async void Buy(decimal amount)
        {
            string instId = InstId;
            CreateOrder api;
            amount = amount * this.Ask;//转为USDT数量
 
            if (QuoteAvailable >= amount)
            {
                api = new SpotBuyImmediately(instId, amount);
                var result = await api.Exec();

                if (result.code != 0)
                {
                    Logger.Instance.LogError("币币下单失败 " + result.message.ToString());
                }
                else
                {
                    //成功
                }
            }
            else
            {
                Logger.Instance.LogInfo(instId + "币币买入失败,余额不足 " + amount + "/ " + QuoteAvailable);
            }
        }

        /// <summary>
        /// 币币市场抛出,直接吃单卖出
        /// </summary>
        /// <param name="amount">数量</param>
        protected override async void Sell(decimal amount)
        {
            amount = Math.Min(amount, this.AvailableInTrading);

            string instId = InstId;

            if (amount > this.instrumentBase.MinSize)
            {
                CreateOrder api = new SpotSellImmediately(instId, amount);
                //api = new SpotCreateOrder(instId, OrderSide.Sell);
                //api.sz = amount.ToString();
                //api.px = this.Bid.ToString();
                var result = await api.Exec();
            }
        }

        /// <summary>
        /// 挂单
        /// </summary>
        /// <param name="side">卖出还是买入</param>
        /// <param name="amount">数量</param>
        /// <param name="price">价格</param>
        /// <param name="postOnly">是否是限价模式，如何是限价模式则，高宇盘口价买入或低于盘口价卖出则失败</param>
        /// <returns>非0，则返回订单ID， 返回0则失败</returns>
        protected long SendOrder(OrderSide side, decimal amount, decimal price, bool postOnly)
        {
            var orderManager = TradeOrderManager.Instance;
            var order = orderManager.PlaceOrder(InstId, amount, price, side, postOnly, true);
           
            if (order != null)
            {
                return order.PublicId;
            }

            return 0;
        }
    }
}
