using CoinTrader.Common;
using CoinTrader.Common.Classes;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;
using System;
using System.Collections.Generic;
using CoinTrader.Strategies;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace CoinTrader.Forms.Strategies
{

    [Strategy(Name = "做市商卖策略", Group = "做市商策略")]
    public class SpotMakerSellStrategy : SpotStrategyBase
    {
        private long myOrderId = 0;

        [StrategyParameter(Name = "卖单最大数量")]
        public decimal SellMaxSize{get;set;}

        /// <summary>
        /// 卖单盘口价格上浮(%)
        /// </summary>
        [StrategyParameter(Name = "价格上浮", Min = 0, Max = 1)]
        public decimal SellProfitMax{get; set;}

        /// <summary>
        /// 低于这个值将修改订单价格
        /// </summary>
        [StrategyParameter(Name = "最低上浮", Min = -.1, Max = 1)]
        public decimal SellProfitMin{get; set;}

        [StrategyParameter(Name = "上停止价格", Min = 0)]

        public decimal UpStop
        {
            get; set;
        }

        [StrategyParameter(Name = "下停止价格", Min = 0)]

        public decimal DownStop
        {
            get; set;
        }

        protected override void OnTick()
        {
            this.Executing = false;

            if (!Effective) //数据不可用
            {
                if (myOrderId > 0)
                {
                    this.CancelAllSellOrders();
                    myOrderId = 0;
                }
                return;
            }

            #region 重新同步挂单数据
            bool isFindOrder = false;
            EachSellOrder((order) =>
            {
                if (myOrderId > 0 && myOrderId == order.PublicId)
                {
                    isFindOrder = true;
                }
                else
                {
                    CancelOrder(order.PublicId, "撤销不同步卖单");
                }
            });

            if (myOrderId > 0 && !isFindOrder)
            {
                CancelOrder(myOrderId, "订单不存在");
                myOrderId = 0;
                return;
            }

            #endregion

            #region 是否触发停止价格
            if ((UpStop > 0 && Ask >= UpStop) || (DownStop > 0 && Ask <= DownStop))
            {
                if (myOrderId > 0)
                {
                    CancelOrder(myOrderId);
                    myOrderId = 0;
                }

                return;
            }

            #endregion

            #region 检查出售挂单
            decimal avalibleAmount = AvailableInTrading;//没有挂单的金额
            decimal frozenAmount = FrozenInTrading; //已挂单的额度
  
             
            //检查出售挂单
            OrderBase sellOrder = this.myOrderId > 0 ? GetOrder(myOrderId) : null;
            decimal profitMin = this.SellProfitMin;
            decimal profitMax = this.SellProfitMax;
            decimal maxSellPrice = Math.Round(Ask * (1.0m  + profitMax), instrument.TickSizeDigit);
            decimal minSellPrice = Math.Round(Ask * (1.0m + profitMin), instrument.TickSizeDigit);
            decimal sellAmount = Math.Min(SellMaxSize, sellOrder != null ? sellOrder.AvailableAmount + avalibleAmount : avalibleAmount);
            
            if (sellOrder != null)
            {
                //价格超出范围,修改已有订单
                if ((minSellPrice - sellOrder.Price) > 0  
                    || (sellOrder.Price - maxSellPrice) > 0)
                {
           
                    if(! ModifyOrder(sellOrder.PublicId, sellAmount, maxSellPrice, true))
                    {
                        myOrderId = 0;
                    }
                }
            }

            if (myOrderId == 0 && sellAmount >= MinSize)
            {
               myOrderId = SendOrder(OrderSide.Sell, sellAmount, maxSellPrice, true);
            }
            #endregion
        }

        private void CancelOrder(long id, string reason = null)
        {
            base.CancelOrder(id);
            if (!string.IsNullOrEmpty(reason))
            {
                Logger.Instance.LogDebug(string.Format("取消挂单{0} {1}", instrument.InstrumentId, reason));
            }
        }

        public override void Dispose()
        {
            this.CancelAllSellOrdersAsync();
            base.Dispose();
        }
    }
}


