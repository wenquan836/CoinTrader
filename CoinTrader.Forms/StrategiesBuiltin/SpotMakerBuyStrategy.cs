
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using System;
using CoinTrader.Strategies;

namespace CoinTrader.Forms.Strategies
{
    [Strategy(Name = "做市商买策略",Group = "做市商策略")]
    public class SpotMakerBuyStrategy : SpotStrategyBase
    {
        private long myOrderId = 0;
 
        [StrategyParameter(Name = "最大持仓数量", Min = 0, Max = double.MaxValue, Intro ="持有数量达到这个数量后将不再买入")]
        public decimal MaxHold { get; set; } = 0;

        [StrategyParameter(Name = "买单最大数量",Min = 0)]
        public decimal BuyMaxSize{get; set;}

        /// <summary>
        /// 买单盘口价格下浮(%)
        /// </summary>
        [StrategyParameter(Name = "价格下浮", Min = 0, Max = 1)]
        public decimal BuyProfitMax { get; set; } = 0.005m;

        /// <summary>
        /// 下浮撤单阈值
        /// </summary>
        [StrategyParameter(Name = "下浮最低", Min = -0.1, Max = 1)]
        public decimal BuyProfitMin{get; set;}

        [StrategyParameter(Name = "挂单冷却(MS)", Intro = "两次挂单之间的时间间隔，单位毫秒，最小为1", Min = 1, Max = 1000000)]
        public int OrderCountDown { get; set; } = 2000;

        [StrategyParameter(Name = "上停止价格", Min = 0)]
        public decimal UpStop{ get; set;}

        [StrategyParameter(Name = "下停止价格", Min = 0)]
        public decimal DownStop{   get; set; }

        protected override void OnTick()
        {
            this.Executing = false;

            if (!Effective) //数据不可用
            {
                if (myOrderId > 0)
                {
                    this.CancelAllBuyOrders();
                    myOrderId = 0;
                }
                return;
            }

            #region 重新同步订单单数据
            bool isFindOrder = false;
            EachBuyOrder((order) =>
            {
                if (myOrderId > 0 && myOrderId == order.PublicId)
                {
                    isFindOrder = true;
                }
                else
                {
                    //撤销不明订单， 可能是手动挂单， 也可能是上次运行后退出程序后留下的订单
                    CancelOrder(order.PublicId, "撤销不同步买单");
                }
            });

            if (myOrderId > 0 && !isFindOrder) //在订单列表里没找到，或者订单可能已经被吃掉
                myOrderId = 0;

            #endregion
 
            #region 触发停止价格检查

            if ((UpStop > 0 && Bid >= UpStop) || (DownStop > 0 && Bid <= DownStop))
            {
                if (myOrderId > 0)
                {
                    CancelOrder(myOrderId, "触发停止价格");
                    myOrderId = 0;
                }

                Wait(OrderCountDown);
                return;
            }

            #endregion

            #region 检查买入挂单
            decimal avalibleAmount = AvailableInTrading;//没有挂单的金额
            decimal frozenAmount = FrozenInTrading; //已挂单卖出的额度
            decimal amountCount = avalibleAmount + frozenAmount; //总持仓

            //根据上浮下浮价格计算出买入价格边界
            decimal minBuyPrice = Math.Round(Bid * (1.0m - BuyProfitMax), instrument.TickSizeDigit); 
            decimal maxBuyPrice = Math.Round(Bid * (1.0m - BuyProfitMin), instrument.TickSizeDigit);
            OrderBase buyOrder = GetOrder(myOrderId);
            decimal amountDiff = this.MaxHold - amountCount; //计算跟最大持仓的差额
            decimal canBuyAmount = QuoteAvailable / minBuyPrice + (buyOrder != null ? buyOrder.AvailableAmount : 0); //剩余可购买数量

            amountDiff = Math.Min(BuyMaxSize, Math.Min(amountDiff, canBuyAmount));

            if (amountDiff > MinSize)
            {
                //需要挂单购买的条件
                if (buyOrder == null
                    || (buyOrder.Price - maxBuyPrice) > 0   //超出买入最低上浮边界
                    || (minBuyPrice - buyOrder.Price) > 0   //超出卖出最大上浮边界
                    )
                {
                    if (myOrderId > 0) //先尝试对原有订单进行修改
                    {
                        bool ret = ModifyOrder(myOrderId, amountDiff, minBuyPrice, true);
                        if (!ret) myOrderId = 0;
                    }

                    if (myOrderId == 0)
                        myOrderId = SendOrder(OrderSide.Buy, amountDiff, minBuyPrice, true);//发送订单

                    Wait(OrderCountDown);
                }
            }
            else
            {
                if (myOrderId != 0)
                {
                    CancelOrder(myOrderId, "已满仓，撤销买单");
                    myOrderId = 0;
                }
            }

            #endregion
        }

        /// <summary>
        /// 撤销订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reason">原因</param>
        private void CancelOrder(long id, string reason = null)
        {
            base.CancelOrder(id);
            if (!string.IsNullOrEmpty(reason))
                LogDebug(string.Format("取消挂单{0} {1}", InstId, reason));
        }
        public override void Dispose()
        {
            this.CancelAllBuyOrdersAsync();
            base.Dispose();
        }
    }
}

