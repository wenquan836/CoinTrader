/*
 * 这是一个示例文件，
 * 放在外置的StrategiesCustomized文件夹下
 * 每次重启的时候就会动态加载StrategiesCustomized文件夹下的 .cs和.csx文件
 * 这个文件里的代码是动态编译，修改后重启生效。
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using CoinTrader.Common.Classes;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.Common;
using CoinTrader.Strategies;

namespace CoinTrader.Forms.Strategies.Customer
{
    enum Direction
    {
        [Description(description:"顺势")]
        Forward = 0,
        [Description(description: "逆势")]
        Reverse = 1
    }

    [Strategy(Name = "合约交易策略")]
    internal class SampleSwapStrategy : SwapStrategyBase
    {
        [StrategyParameter(Name = "仓位大小(USD)", Min = 1, Max = 100000000, Intro = "按稳定币计价的仓位大小")]
        public decimal PositionSizeUsd { get; set; }

        private uint lever = 1;
        [StrategyParameter(Name = "杠杆倍数", Min = 1, Max = 150)]
        public uint Lever
        {
            get
            {
                return lever;
            }
            set
            {
                lever = value;
                resetLever = true;
            }
        }
    
        [StrategyParameter(Name = "仓位模式")]
        public SwapMarginMode Mode{get;set;}

        [StrategyParameter(Name = "趋势")]
        public Direction DirectionType{get;set; }

        [StrategyParameter(Name = "K线采样数",Min = 1,Max = 200)]
        public int KLinSample{get; set;}

        [StrategyParameter(Name = "波动幅度(%)", Min = .1, Max = 1000, Intro = "触发涨跌幅")]
        public float Range{ get;set;}

        [StrategyParameter(Name = "数量", Min = 1, Max = 200,Intro = "满足多少个符合涨跌幅的K线")]
        public int KLineCount{ get;set; }

        CandleGranularity _candle;
        [StrategyParameter(Name = "K线周期")]
        public CandleGranularity OpenCandle
        {
            get { return _candle; }
            set
            {
                if (_candle != value)
                {
                    UnloadCandle(this._candle);
                    _candle = value;
                }
            }
        }

        [StrategyParameter(Name = "止损(%)", Min = .1, Max = 100)]
        public float StopLoss
        {
            get; set;
        }

        [StrategyParameter(Name = "止盈(%)", Min = .1, Max = 100)]
        public float StopSurplus
        {
            get; set;
        }

        [StrategyParameter(Name = "移动止盈")]
        public bool MoveProfit
        {
            get;set;
        }

        [StrategyParameter(Name = "移动止盈回调幅度(%)",Dependent = "MoveProfit",DependentValue = true, Min = .1,Max =100)]
        public float Retracement
        {
            get; set;
        }

        [StrategyParameter(Name = "开启补仓")]
        public bool OpenAppend
        {
            get; set;
        }

        [StrategyParameter(Name = "最大补仓次数", Min = 1, Max = 100, Dependent = "OpenAppend", DependentValue = true, Intro = "亏损幅度到达幅度后总共进行几次补仓")]
        public decimal AppendTimes
        {
            get; set;
        }

        [StrategyParameter(Name = "补仓亏损幅度(%)", Min = 0.1, Max = 100, Dependent = "OpenAppend", DependentValue = true, Intro = "当前仓位亏损幅度达到多少的时候进行一次补仓")]
        public decimal AppendLoss
        {
            get; set;
        }

        /// <summary>
        /// 是否需要重新设置杠杆倍数
        /// </summary>
        private bool resetLever = true;

        /// <summary>
        /// 触发移动止盈的最后最高（最低）价格记录
        /// </summary>
        private decimal lastTrigerPrice = 0;

        /// <summary>
        /// 交易冷却（主要是等待同步数据)
        /// </summary>
        private float delay = 2.0f;

        public SampleSwapStrategy()
        {
            this.KLinSample = 1;
            this.KLineCount = 1;
            this.StopLoss = 10;
            this.StopSurplus = 10;
            this.Range = 1;
            this.Retracement = 1;
            this.lever = 10;
            this.PositionSizeUsd = 500;
            this.AppendLoss = 10;
            this.AppendTimes = 1;
            this._candle = CandleGranularity.M15;
        }

        /// <summary>
        /// 初始化函数， 传入instId
        /// </summary>
        /// <param name="instId">代表这个策略将在那个交易品种上运行</param>
        /// <returns>成功返回true，失败返回false</returns>
        public override bool Init(string instId)
        {
            //基类初始化成功
            if (base.Init(instId))
            {
                 return true;
            }

            return false;
        }

        /// <summary>
        /// 每次收到服务器端报价的时候就会执行这个函数
        /// </summary>
        protected override void  OnTick()
        {
            this.Executing = false;

            if (!Effective)//数据不正常
                return;
 
            IList<Position> postions = GetPositions(); //获取当前币种的所有的持仓
            var amount = GetPostionSize(Ask, Bid);//获取每次下单的数量

            Position pos = postions.Count > 0 ? postions[0] : null;

            Message = string.Format("仓位大小 {0}{1}",amount.ToString(instrument.AmountFormat), instrument.CtValCcy);

            if (pos == null) //没有持仓
            {
                PositionType side;
                if (CanOpen(Ask, Bid, out side) &&  amount > 0) //判断是否可以开仓
                {
                    this.Executing = true;
                    lastTrigerPrice = 0;//清零移动止盈标记价格
                    if (resetLever) //重置杠杆倍数
                    {
                        SetLever(side, Mode, lever);
                        resetLever = false;
                    }
                    
                    if (CreatePosition(side, amount, Mode) > 0)//判断是否下单成功
                    {
                        Wait(delay);
                    }
                }
            }
            else //已持仓情况
            {
                if (CanClose(pos, Ask, Bid))//判断是否可以平仓， 包括止盈、止损两种情况
                {
                    this.Executing = true;
                    ClosePosition(pos.PosId); //平仓
                    Wait(delay);
                }
                else if (CanAppend(pos, Ask, Bid))//是否可以追加仓位
                {
                    this.Executing = true;
                    if (CreatePosition(pos.SideType, amount,Mode) > 0)//判断是否下单成功
                    {
                        PlusAppendTimes(pos.PosId);
                        Wait(delay);
                    }

                    lastTrigerPrice = 0;//清零移动止盈标记价格
                }
            }
        }

        /// <summary>
        /// 获取仓位加仓次数
        /// </summary>
        /// <param name="posId"></param>
        /// <returns></returns>
        private int GetAppendTimes(long posId)
        {
            string storageKey = GetStorageKey(posId);

            return LocalStorage.GetValue<int>(storageKey);
        }

        /// <summary>
        /// 本地存储键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetStorageKey(long id)
        {
            return string.Format("append_times_{0}", id);
        }


        /// <summary>
        /// 保存补仓次数
        /// </summary>
        /// <param name="posId"></param>
        private void PlusAppendTimes(long posId)
        {
            string storageKey = GetStorageKey(posId);
            int times = LocalStorage.GetValue<int>(storageKey);
            times++;
            LocalStorage.SetValue(storageKey, times);
        }

        /// <summary>
        /// 计算仓位大小， 仓位是以主币种数量来定。 不是按张数
        /// </summary>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        private decimal GetPostionSize(decimal ask, decimal bid)
        {
            var balance = QuoteAvailable; //获取稳定币结余
            var quoteAmount = Math.Min(balance, PositionSizeUsd);
            var amount = quoteAmount / ((ask + bid) * 0.5M);//计算出每次下单的大小
            return amount;
        }

        /// <summary>
        /// 根据方向选择平仓价格
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        private decimal GetClosePrice(PositionType orderSide, decimal ask, decimal bid)
        {
            return orderSide == PositionType.Short ? ask : bid;
        }

        /// <summary>
        /// 根据盘口报价选择开仓价格
        /// </summary>
        /// <param name="orderSide"></param>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        private decimal GetOpenPrice(PositionType orderSide, decimal ask, decimal bid)
        {
            return orderSide == PositionType.Short ? bid : ask;
        }

        /// <summary>
        /// 获取持仓盈利百分比
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        private decimal GetProfitPercent(Position pos, decimal ask, decimal bid)
        {
            var openPrice = pos.AvgPx;
            var price = GetClosePrice(pos.SideType,ask,bid);
            return (pos.SideType == PositionType.Short ? openPrice / price : price / openPrice)-1.0m;
        }

        private decimal ToPercent(float val)
        {
            return Convert.ToDecimal( val) * 0.01m;
        }
        private decimal ToPercent(decimal val)
        {
            return val * 0.01m;
        }

        /// <summary>
        /// 判断是否可以平仓（止盈或止损）
        /// </summary>
        /// <param name="pos">仓位</param>
        /// <param name="ask">卖价</param>
        /// <param name="bid">买价</param>
        /// <returns></returns>
        private bool CanClose(Position pos,decimal ask, decimal bid)
        {
            var profit = GetProfitPercent(pos, ask, bid);

            if(profit > 0) //持仓已盈利情况
            {
                if(MoveProfit) //移动盈利
                {
                    var closePrice = GetClosePrice(pos.SideType, ask, bid);
                    if(lastTrigerPrice > 0) //已经被触发过，判断是否触发移动止盈
                    {
                        switch (pos.SideType)//记录移动止盈的 最高（最低）参考价。
                        {
                            case PositionType.Long: //多头持仓的情况
                                return lastTrigerPrice / closePrice >= 1.0m + ToPercent(Retracement);// 多头回撤
                                
                            case PositionType.Short: //空头持仓的情况
                                return closePrice / lastTrigerPrice >= 1.0m + ToPercent(Retracement);// 空头回撤
                        }
                    }

                    if (profit >= ToPercent(StopSurplus)) //达到盈利目标，开始记录移动止盈的最高（最低）价格
                    {
                        switch (pos.SideType)//记录移动止盈的 最高（最低）参考价。
                        {
                            case PositionType.Long: //多头持仓的情况
                                lastTrigerPrice = Math.Max(lastTrigerPrice, closePrice);// 取最高价
                                break;
                            case PositionType.Short: //空头持仓的情况
                                lastTrigerPrice = lastTrigerPrice == 0 ?  closePrice : Math.Min(lastTrigerPrice, closePrice);// 取最低价
                                break;
                        }
                    }
                }
                else
                {
                    if(profit >=  ToPercent( StopSurplus)) //达到盈利目标
                    {
                        return true;
                    }
                }
            }
            else //持仓非盈利情况
            {
                if(profit <= -ToPercent( this.StopLoss) )//到达止损亏损幅度
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断是否可以开仓
        /// </summary>
        /// <param name="ask"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        private bool CanOpen(decimal ask, decimal bid,out PositionType side)
        {
            if(CheckKLine(out side)) //检查K线是否符合触发条件
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查K线是否已经符合触发建仓条件
        /// </summary>
        /// <returns></returns>
        private bool CheckKLine(out PositionType side)
        {
            side = PositionType.Long;
            var candleProvider = GetCandleProvider(_candle);//获取K线

            if(KLinSample > 0 && candleProvider != null)//k线是否已经加载成功
            {
                int num = 0;//
                int downCount = 0, upCount = 0; //涨跌幅计数
                candleProvider.EachCandle((candle) =>
                {
                    var amp = (candle.Close / candle.Open) - 1.0m; //当前k线的涨跌幅

                    if (amp >= (decimal)ToPercent(Range))//上涨超过超过设定幅度
                    {
                        upCount++;
                    }

                    if (amp <= (decimal)ToPercent(-Range))//下跌超过设定幅度
                    {
                        downCount++;
                    }
                    num++;
                    return num >= KLinSample;
                });

                if (upCount >= KLineCount)//上涨数量达到设定数量
                {
                    switch(this.DirectionType)
                    {
                        case Direction.Forward:
                            side = PositionType.Long;
                            break;
                        case Direction.Reverse:
                            side = PositionType.Short;
                            break;
                    }

                    return true;
                }
                else if (downCount >= KLineCount) //下跌数量达到设定数量
                {
                    switch (this.DirectionType)
                    {
                        case Direction.Forward:
                            side = PositionType.Short;
                            break;
                        case Direction.Reverse:
                            side = PositionType.Long;
                            break;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否可以加仓
        /// </summary>
        /// <returns></returns>
        private bool CanAppend(Position pos, decimal ask, decimal bid)
        {
            if (!this.OpenAppend)
                return false;

            var profit = GetProfitPercent(pos, ask, bid); //获取利润值

            if(profit <= - (decimal)ToPercent( AppendLoss))
            {
                int appendTimes = GetAppendTimes(pos.PosId);//获取当前仓位的补仓次数

                if(appendTimes < this.AppendTimes) //判断是否达到最大补仓数
                {
                    return true;
                }
            }

            return false;
        }
    }
}
