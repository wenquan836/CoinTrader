using CoinTrader.Common;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Interface;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Event;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.REST;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Strategies
{
    /// <summary>
    /// 合约策略基类，所有针对合约的策略均需要从这个基类派生
    /// </summary>
    public abstract class SwapStrategyBase : TradeStrategyBase
    {
        protected InstrumentSwap instrument;
 

        private SWPFundingRateMonitor fundingRateMonitor = null;

        public override void Init(string instId)
        {
            base.Init(instId);
            instrument = instrumentBase as InstrumentSwap;

            if (instrument == null)
            {

            }

            fundingRateMonitor = new SWPFundingRateMonitor(InstId);
            dataProvider.AddMonitor(fundingRateMonitor);
        }

        /// <summary>
        /// 买入，建立多头仓位 ,以盘口价直接买入
        /// </summary>
        /// <param name="amount">数量</param>
        protected override void Buy(decimal amount)
        {
            PositionManager.Instance.CreatePosition(InstId, PositionType.Long, amount);
        }

        /// <summary>
        /// 卖出：建立空头仓位，直接以盘口价格卖出
        /// </summary>
        /// <param name="amount">数量</param>
        protected override void Sell(decimal amount)
        {
            PositionManager.Instance.CreatePosition(InstId, PositionType.Short, amount);
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

        /// <summary>
        /// 获取资金费率
        /// </summary>
        /// <returns></returns>
        protected double GetFundingRate()
        {
            return this.fundingRateMonitor!= null? this.fundingRateMonitor.FundingRate.Rate : 0;
        }

        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        protected void ClosePosition(long id)
        {
            PositionManager.Instance.ClosePosition(id);
        }

        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        /// <param name="size">合约张数</param>
        protected void ClosePosition(long id,int size)
        {
            PositionManager.Instance.ClosePosition(id,size);
        }


        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount">数量</param>
        protected void ClosePosition(long id, decimal amount)
        {
            PositionManager.Instance.ClosePosition(id,amount);
        }


        /// <summary>
        /// 获取最大杠杆倍数
        /// </summary>
        /// <returns></returns>
        protected uint GetMaxLever()
        {
            return instrument.Lever;
        }

        /// <summary>
        /// 设置杠杆
        /// </summary>
        /// <param name="mode">保证金模式(逐仓和全仓)</param>
        /// <param name="lever">倍数</param>
        protected void SetLever( PositionType side, SwapMarginMode mode, uint lever)
        {
            if (lever < 1 || lever > GetMaxLever())
                return;

            AccountManager.SetLever(InstId, side,mode, lever);
        }

        /// <summary>
        /// 获取当前合约品种下的所有持仓
        /// </summary>
        /// <returns></returns>
        protected IList<Position> GetPositions()
        {
            return PositionManager.Instance.GetPositions(InstId);
        }
        
        /// <summary>
        /// 返回某个方向下的所有持仓
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<Position> GetPositions(PositionType type)
        {
            IList<Position> positions = new List<Position>();

            this.EachPosition((pos) => {
                if (pos.SideType == type && string.Compare(pos.InstId, InstId) == 0)
                {
                    positions.Add(pos); 
                }
            });

            return positions;
        }

        /// <summary>
        /// 获取指定ID的持仓
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected Position GetPosition(long id)
        {
            Position position = PositionManager.Instance.GetPosition(id);
            return position;
        }

        /// <summary>
        /// 遍历所有持仓
        /// </summary>
        /// <param name="callback"></param>
        protected void EachPosition(Action<Position> callback)
        {
            PositionManager.Instance.EachPosition(callback);
        }

        /// <summary>
        /// 市价创建仓位
        /// </summary>
        /// <param name="side">方向</param>
        /// <param name="amount">数量</param>
        /// <param name="mode">仓位模式</param>
        /// <returns>返回非0则成功</returns>
        protected long CreatePosition(PositionType side, decimal amount, SwapMarginMode mode )
        {
            return PositionManager.Instance.CreatePosition(InstId, side, amount, mode);
        }

        /// <summary>
        /// 市价创建仓位
        /// </summary>
        /// <param name="side">方向</param>
        /// <param name="size">合约张数</param>
        /// <param name="mode">仓位模式</param>
        /// <returns>返回非0则成功</returns>
        protected long CreatePosition(PositionType side, int size, SwapMarginMode mode)
        {
            return PositionManager.Instance.CreatePosition(InstId, side, size, mode);
        }
    }
}
