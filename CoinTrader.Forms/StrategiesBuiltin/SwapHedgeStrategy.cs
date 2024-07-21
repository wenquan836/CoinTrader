
using System;
using System.ComponentModel;
using CoinTrader.Strategies;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Util;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Manager;

namespace CoinTrader.Forms.Strategies
{
    public enum OrderInterval
    {
        D1 = 1,
        H4 = 2,
        H6 = 3,
        H12 = 4,
        W1 = 5
    }

    public enum LongPositionCoin
    {
        [Description("未设置")]
        None = 0,
        BTC,
        ETH
    }

    public enum SizeType
    {
        [Description("固定金额")]
        Fixed,
        [Description("可用USD的百分比")]
        Percent
    }

    [Strategy(Name = "空头交叉对冲策略")]
    public class SwapHedgeStrategy : SwapStrategyBase
    {
        private MarketDataProvider longDataProvider = null; //多头数据源
        private MarketDataProvider shortDataProvider = null;//空头数据源
        private CountDown placeOrderCountDown = new CountDown(3600000);

        private LongPositionCoin _longCoin = LongPositionCoin.None;

        [StrategyParameter(Name = "多头持仓品种")]
        public LongPositionCoin LongCoin
        {
            get 
            {
                return _longCoin;
            }
            set
            {
                if (value == _longCoin)
                {
                    return;
                }

                _longCoin = value;

                LoadLongDataProvider();
            }
        }

        [StrategyParameter(Name = "仓位大小模式")]
        public SizeType PosisionSizeType { get; set; }

        [StrategyParameter(Name = "大小USD(单边)",Dependent = "PosisionSizeType", DependentValue = SizeType.Fixed,  Min = 100, Max = 1000000)]
        public double Size { get; set; }

        [StrategyParameter(Name = "百分比(单边)", Dependent = "PosisionSizeType", DependentValue = SizeType.Percent, Min = 0.01, Max = 0.5)]
        public double Percent { get; set; }

        [StrategyParameter(Name = "停止价格", Min = 0, Max = double.MaxValue, Intro ="如果此参数非0，则到达这个价格后停止下单")]
        public decimal StopPrice { get; set; } 
        [StrategyParameter(Name = "止损利润", Min = 0.002f, Max = 2.0f)]
        public float StopLoss { get; set; }        
        
        [StrategyParameter(Name = "止盈利润", Min = 0.002f, Max = 2.0f)]
        public float TakeProfit { get; set; }
        [StrategyParameter(Name = "移动止盈")]
        public bool MoveProfit{get;set;}
        [StrategyParameter(Name = "利润回撤值", Dependent = "MoveProfit", DependentValue = true)]
        public double Retreat { get; set; }
        [StrategyParameter(Name = "下单间隔")]
        public OrderInterval Interval { get; set; } 

        [StrategyParameter(Name = "杠杆倍数", Min = 1, Max = 10)]
        public uint Lever { get; set; }

        /*

        [StrategyParameter(Name = "自动增减保证金")]
        public bool AutoMargin { get; set; } = false;

        [StrategyParameter(Name = "保证金不足阈值", Dependent = "AutoMargin", DependentValue = true, Min = 2, Max = 10)]
        public float MarginLowThreshold { get; set; } = 2.0f;

        [StrategyParameter(Name = "保证金多余阈值", Dependent = "AutoMargin", DependentValue = true, Min = 2, Max = 10)]
        public float MarginHighThreshold { get; set; } = 10.0f;

        [StrategyParameter(Name = "增减保证金")]
        public decimal MarginChangeAmount { get; set; }
        */


        private object lockObj = new object();

        public double maxProfit = 0;
        DateTime maxProfitTime = DateTime.Now;//最高利润率发生时间
        CountDown cdMargin = new CountDown(20000,false);


        public SwapHedgeStrategy()
        {
            Size = 200;
            Percent = 0.1;
            TakeProfit = 0.05f;
            Retreat = 0.2;
            StopLoss = 0.025f;
            Interval = OrderInterval.D1;
            Lever = 2;
        }
 
        private void LoadLongDataProvider()
        {
            this.ReleaseLongDataProvider();
            string longInst = ""; 
            switch (LongCoin)
            {
                case LongPositionCoin.None:
                    return;
                case LongPositionCoin.BTC:
                    longInst = string.Format("{0}-{1}-SWAP", "BTC", QuoteCurrency);
                    break;
                case LongPositionCoin.ETH:
                    longInst = string.Format("{0}-{1}-SWAP", "ETH", QuoteCurrency);
                    break;
            }

            if (string.Compare(InstId, longInst, StringComparison.OrdinalIgnoreCase) == 0)
            {
                Message = "多头和空头不能是同一币种";
                return;
            }

            longDataProvider = DataProviderManager.Instance.GetProvider(longInst);
            longDataProvider.OnTick += OnLongTick;
        }

        /// <summary>
        /// 根据当前报价获得利润率
        /// </summary>
        /// <param name="pos">仓位</param>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        private decimal GetPositionProfit(Position pos, decimal ask, decimal bid)
        {
            var nowPrice = pos.SideType == PositionType.Short ? ask : bid; //根据空头和多头使用不同的结算价格
            var costPrice = pos.AvgPx;//开仓均价
            decimal profit = 0;

            switch(pos.SideType)
            {
                case PositionType.Long:
                    profit = (nowPrice - costPrice) / costPrice;
                    break;
                case PositionType.Short:
                    profit = (costPrice - nowPrice) / costPrice;  
                    break;
            }

            return profit;
        }

        protected  void OnTickLong()
        {
            if(!this.Enable) return;

            this.OnTick();
        }

        private void CheckMargin(params Position[] positions)
        {
            var usdx = USDXWallet.Instance.AvailableInTrading;
            foreach(var position in positions)
            {
                if(position.MgnRatio < 2.00)
                {
                    PositionManager.Instance.ChangeMarginBalance(position.PosId, +100);
                }
                else if(position.MgnRatio > 10)
                {
                    PositionManager.Instance.ChangeMarginBalance(position.PosId, -100);
                }
            }
        }

        protected override void OnTick()
        {
            if (longDataProvider == null)
                return;

            if (!this.Enable) return;

            ///所有数据均有效获取到的情况下
            if (!(Effective && longDataProvider.Effective))
            {
                return;
            }

            if (System.Threading.Monitor.TryEnter(lockObj))//非强制加锁
            {
                //空头仓位
                var positionShort = PositionManager.Instance.GetPosition(InstId, PositionType.Short);
                //多头仓位
                var positionLong = PositionManager.Instance.GetPosition(longDataProvider.InstrumentId, PositionType.Long);

                //存在双向持仓
                if (positionShort != null && positionLong != null)
                {
                    var longAsk = longDataProvider.Ask;
                    var longBid = longDataProvider.Bid;
                    var shortAsk = Ask;
                    var shortBid = Bid;

                    //if(AutoMargin && cdMargin.Check())
                    //{
                    //    CheckMargin(positionLong, positionShort);
                    //}
                    var uplPercent = Convert.ToDouble(GetPositionProfit(positionLong, longAsk, longBid) + GetPositionProfit(positionShort, shortAsk, shortBid));
                    var uplValue = positionLong.Upl + positionShort.Upl;
                    bool close = false;
                    ///对冲利润大于止盈利润值
                    if (uplPercent >= TakeProfit)
                    {
                        if (MoveProfit)
                        {
                            var tmp = maxProfit;
                            maxProfit = Math.Max(maxProfit, uplPercent);

                            if(maxProfit>tmp)
                            {
                                maxProfitTime = DateTime.Now;
                            }

                            if (maxProfit > 0 && maxProfit - uplPercent >= Retreat)
                            {
                                LogDebug( string.Format("移动止盈平仓{0} {1} 最高利润率 {2:p} 当前利润率{3:p} 最高利润时间{4}"  , InstId , GetType().Name, maxProfit, uplPercent,maxProfitTime));
                                close = true;
                            }
                        }
                        else
                        {
                            LogDebug("止盈平仓" +  InstId + " " + GetType().Name);
                            close = true;
                        }
                    }
                    else if (uplPercent <= -Math.Abs(StopLoss))//对冲利润到达止损值
                    {
                        LogDebug("止损平仓" + InstId + " "+ GetType().Name);
                        close = true;
                    }

                    Message =  string.Format( "未平仓利润≈{0:0.00} ({1:p})", uplValue, uplPercent);
                    
                    if (close)
                    {
                        LogDebug( string.Format( "平仓价格：多头({0} | {1}) 空头({2} | {3}) 利润 {4:p}",longAsk , longBid,shortAsk,shortBid,uplPercent));
                        ClosePosition(positionShort.PosId);
                        ClosePosition(positionLong.PosId);
                        Wait(5);
                    }
                    else
                    {
                        if(uplValue > 0)//如果盈利为正，逐仓情况下调整保证金， 避免单向爆仓//TODO
                        {

                        }
                    }
                }
                else if (positionShort != null || positionLong != null)//单边持仓，强制关闭，可能单边爆仓
                {
                    var pos = positionShort != null ? positionShort : positionLong;
                    if ((DateUtil.GetServerUTCDateTime() - pos.CTime).TotalSeconds > 20)
                    {
                        ClosePosition(pos.PosId);
                        LogDebug("单向持仓"  + pos.InstId +  "强制平仓");
                    }
                }
                else //无持仓
                {   
                    Message = "空仓";

                    ///到达停止价格
                    if (Bid <= this.StopPrice)
                        return;

                    DateTime now = DateTime.Now;
                    bool placeOrder = false;

                    switch (Interval)
                    {
                        case OrderInterval.D1:
                            placeOrder = now.Hour == 0;
                            break;
                        case OrderInterval.H4:
                            placeOrder = now.Hour % 4 == 0;
                            break;
                        case OrderInterval.H6:
                            placeOrder = now.Hour % 6 == 0;
                            break;
                        case OrderInterval.H12:
                            placeOrder = now.Hour % 12 == 0;
                            break;
                        case OrderInterval.W1:
                            placeOrder = now.DayOfWeek == 0 && now.Hour == 0;
                            break;
                    }
                    
                    if (placeOrder && placeOrderCountDown.Check())
                    {   
                        maxProfit = 0;
                        var instShort = InstId;                        
                        var instLong = longDataProvider.InstrumentId;
                        AccountManager.SetLever(instShort, PositionType.Short, SwapMarginMode.Isolated, Lever);
                        AccountManager.SetLever(instLong, PositionType.Long, SwapMarginMode.Isolated, Lever);

                        var quoteBalance = QuoteAvailable;
                        var amountShort = GetPostionSize(quoteBalance, Ask, Bid);//获取每次下单的数量
                        var amountLong = GetPostionSize(quoteBalance, longDataProvider.Ask, longDataProvider.Bid);//获取每次下单的数量
                        var shortId = PositionManager.Instance.CreatePosition(instShort, PositionType.Short, amountShort, SwapMarginMode.Isolated);
                        
                        if (shortId > 0)//判断是否下单成功，空头
                        {
                            var longId = PositionManager.Instance.CreatePosition(instLong, PositionType.Long, amountLong, SwapMarginMode.Isolated);
                            
                            if (longId > 0)//判断是否下单成功 ,多头
                            {
                                LogDebug( string.Format("下单成功 {0} {1} longId {2} shortId {3}",InstId,GetType().Name,longId,shortId));
                                //双向都成功下单
                                Wait(5.0f);//等待数据同步
                            }
                            else
                            {
                                LogError( string.Format("多头方向下单失败 {0} {1}", InstId,GetType().Name));
                            }
                        }
                        else
                        {
                            LogError(string.Format("空头方向下单失败 {0} {1}", InstId,GetType().Name));
                        }
                    }
                }

                System.Threading.Monitor.Exit(lockObj);
            }
        }

        /// <summary>
        /// 持仓量转化为数量
        /// </summary>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        private decimal GetPostionSize( decimal quoteBalance, decimal ask, decimal bid)
        {
            decimal avalible = quoteBalance * Convert.ToDecimal(Math.Sqrt(Lever)) * 0.498m; //单边仓位可用的usdt * 杠杆倍数的平方根的一半
            decimal posSize = 0;

            switch(PosisionSizeType)
            {
                case SizeType.Fixed:
                    posSize = Convert.ToDecimal(Size); 
                    break;
                case SizeType.Percent:
                    posSize = quoteBalance * Convert.ToDecimal(Percent);
                    break;
            }

            posSize = Math.Min(avalible, Convert.ToDecimal(posSize));
            return posSize / ((ask + bid) * 0.5M);
        }

        private void OnLongTick(decimal ask, decimal bid)
        {
            this.OnTick();
        }

        private void ReleaseLongDataProvider()
        {
            if (longDataProvider != null)
            {
                this.longDataProvider.OnTick -= OnLongTick;
                DataProviderManager.Instance.ReleaseProvider(this.longDataProvider);
                this.longDataProvider = null;
            }
        }

        public override void Dispose()
        {
            ReleaseLongDataProvider();
            base.Dispose();
        }
    }
}
