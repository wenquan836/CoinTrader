using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.Strategies.Runtime;
using System;
using System.Collections.Generic;
 
namespace CoinTrader.Strategies
{
    /// <summary>
    /// 合约策略基类，所有针对合约的策略均需要从这个基类派生
    /// </summary>
    public abstract class SwapStrategyBase : TradeStrategyBase
    {
        protected InstrumentSwap instrument;
        protected override StrategyType StrategyType => StrategyType.Swap;

        private SwapStrategyRuntime swapRuntime;

        /// <summary>
        /// 获取最大杠杆倍数
        /// </summary>
        /// <returns></returns>
        protected uint MaxLever => swapRuntime.GetMaxLever();

        /// <summary>
        /// 获取资金费率
        /// </summary>
        /// <returns></returns>
        protected double FundingRate => swapRuntime.GetFundingRate();

        public override bool Init(string instId)
        {
           if(! base.Init(instId))
                return false;

            instrument = instrumentBase as InstrumentSwap;

            swapRuntime = runtime as SwapStrategyRuntime;

            if (instrument == null)
            {

            }
            return true;
        }

        /// <summary>
        /// 设置杠杆
        /// </summary>
        /// <param name="mode">保证金模式(逐仓和全仓)</param>
        /// <param name="lever">倍数</param>
        public void SetLever(PositionType side, SwapMarginMode mode, uint lever)
        {
            swapRuntime.SetLever(side, mode, lever);
        }

        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        protected void ClosePosition(long id)
        {
            swapRuntime.ClosePosition(id);
        }

        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        /// <param name="size">合约张数</param>
        protected void ClosePosition(long id,int size)
        {
            swapRuntime.ClosePosition(id,size);
        }


        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount">数量</param>
        protected void ClosePosition(long id, decimal amount)
        {
            swapRuntime.ClosePosition(id,amount);
        }

        /// <summary>
        /// 获取当前合约品种下的所有持仓
        /// </summary>
        /// <returns></returns>
        protected IList<Position> GetPositions()
        {
            return swapRuntime.GetPositions();
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
            Position position = swapRuntime.GetPosition(id);
            return position;
        }

        /// <summary>
        /// 遍历所有持仓
        /// </summary>
        /// <param name="callback"></param>
        protected void EachPosition(Action<Position> callback)
        {
            swapRuntime.EachPosition(callback);
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
            return swapRuntime.CreatePosition(side, amount, mode);
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
            return swapRuntime.CreatePosition(side, size, mode);
        }
    }
}
