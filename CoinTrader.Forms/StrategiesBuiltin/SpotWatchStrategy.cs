
using System;
using CoinTrader.Strategies;

namespace CoinTrader.Forms.Strategies
{

    [Strategy(Name = "现货自动盯盘")]
    public class SpotWatchStrategy : SpotStrategyBase
    {
        private bool buyTriggered = false;
        private bool sellTriggered = false;

        private decimal sellPrice;
        [StrategyParameter(Name = "清仓价格", Min = 0, Max = double.MaxValue, Intro = "当买盘价格高于这个价格时全部抛售")]
        public decimal SellPrice
        {
            get { return sellPrice; }
            set 
            {
                sellPrice = value;
                sellTriggered = false;
            }
        }

        private decimal buyAmount = 0;

        [StrategyParameter(Name = "买入数量", Min = 0, Max = double.MaxValue, Intro ="稳定币不足可能导致买入失败")]
        public decimal BuyAmount
        {
            get
            {
                return buyAmount;
            }
            set
            {
                if (value < MinSize)
                    throw new Exception("最少买入数量不能少于" + MinSize);

                buyTriggered = false;
                buyAmount = value;
            }
        }

        decimal buyPrice = 0;
        [StrategyParameter(Name = "买入价格", Min = 0, Max = double.MaxValue, Intro = "当卖盘价格小于等于这个价格时触发买入操作")]
        public decimal BuyPrice
        {
            get 
            { 
                return buyPrice; 
            }
            set
            {
                buyPrice = value;
                buyTriggered = false;
            }
        }

        /// <summary>
        /// 所有的交易逻辑都在这个报价函数里执行
        /// </summary>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        protected override void OnTick()
        {
            this.Executing = false;
            if (!Effective)
                return;
            
            if (!buyTriggered)
            {
                if (Ask <= BuyPrice)
                {
                    this.Executing = true;
                    Buy(BuyAmount);
                    Wait(500);
                    buyTriggered = true;
                }
            }

            if (!sellTriggered)
            {
                if (Bid >= SellPrice && SellPrice > 0 && AvailableInTrading >= MinSize)
                {
                    this.Executing = true;
                    Sell(AvailableInTrading);
                    sellTriggered = true;
                    Wait(500);
                }
            }
        }

        /// <summary>
        /// 定时器函数
        /// </summary>
        /// <param name="deltaTime"></param>
        protected override void OnTimer(int deltaTime)
        {
           Message = $"买入价格{BuyPrice}  清仓价格{SellPrice} 数量 {BuyAmount}{BaseCurrency}";
        }
    }
}