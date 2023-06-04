

using CoinTrader.Common;
using CoinTrader.Common;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Event;
using CoinTrader.OKXCore.Manager;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.REST;
using System;
using System.Collections.Generic;

namespace CoinTrader.OKXCore.Manager
{
    /// <summary>
    /// 仓位管理
    /// </summary>
    public class PositionManager
    {
        SWPPositionMonitor monitor = new SWPPositionMonitor();
        private PositionManager()
        {
            
        }

        public void StartUpdate()
        {
            SWPPositionMonitor monitor = this.monitor != null ? this.monitor : new SWPPositionMonitor();
            MonitorSchedule mgr = MonitorSchedule.Default;
            mgr.AddMonotor(monitor);
        }

        public void EndUpdate()
        {
            if(monitor != null)
            {
                MonitorSchedule mgr = MonitorSchedule.Default;
                mgr.RemoveMonitor(monitor);
                monitor = null;
            }
        }

        /// <summary>
        /// 是否有某个方向的仓位
        /// </summary>
        /// <param name="instrument"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool HasPosition(string instrument, PositionType type)
        {
            return GetPosition(instrument, type) != null;
        }

        public bool HasPosition(string instrument)
        {
            bool hasPos = false;

            this.EachPosition((pos) =>
            {
                if (!hasPos && string.Compare(instrument, pos.InstId) == 0)
                {
                    hasPos = true;
                }
            });

            return hasPos;
        }

        /// <summary>
        /// 获取交易品种下的所有持仓
        /// </summary>
        /// <param name="instId"></param>
        /// <returns></returns>
        public List<Position> GetPositions(string instId)
        {
            List< Position> positions = new List<Position>(1);

            this.EachPosition((pos) => {
                if (string.Compare(instId, pos.InstId) == 0)
                {
                    positions.Add(pos);
                }
            });

            return positions;
        }

        /// <summary>
        /// 根据持仓方向获取持仓数据
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Position GetPosition(string instId,   PositionType type)
        {
            Position position = null;

            this.EachPosition((pos) => {
                if (position == null && string.Compare( pos.InstId , instId) == 0)
                {
                    if(type == PositionType.Long && pos.PosSide == PositionSide.Long)
                    {
                        position = pos;
                    }
                    else if(type == PositionType.Short && pos.PosSide == PositionSide.Short)
                    {
                        position = pos;
                    }
                }
            });

            return position;
        }

        public Position GetPosition(long id)
        {
            Position position = null;

            this.EachPosition((pos) => {
                if (pos.PosId == id)
                {
                    position = pos;
                }
            });

            return position;
        }

        public void EachPosition(Action<Position> callback)
        {
            if(this.monitor != null)
                this.monitor.EachPostion(callback);
        }

        /// <summary>
        /// 设置止盈价格
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        public bool SetTakeProfit(long id, decimal price)
        {
            Position pos = this.GetPosition(id);

            if(pos != null)
            {
                InstrumentSwap instrument = InstrumentManager.SwapInstrument.GetInstrument(pos.InstId);
                return this.SetTakeProfit(id, pos.AvailPos * instrument.CtVal, price);
            }

            return false;
        }

        public bool SetStopPrice(string instId,string posSide, string tdMode, decimal amount, bool stopLoss, decimal price)
        {
            InstrumentSwap instrument = InstrumentManager.SwapInstrument.GetInstrument(instId);
            int size = (int)Math.Round(amount / instrument.CtVal);

            StrategyEntrust api;

            if (stopLoss)
            {
                var stopLossApi = new StrategyEntrustStopLoss();
                stopLossApi.slOrdPx = "-1";// price.ToString();
                stopLossApi.slTriggerPx = price.ToString();

                api = stopLossApi;
            }
            else
            {
                var takeProfitApi = new StrategyEntrustTakeProfit();

                takeProfitApi.tpOrdPx = "-1";// price.ToString();
                takeProfitApi.tpTriggerPx = price.ToString();
                api = takeProfitApi;
            }

            string side = posSide == PositionSide.Short ? Side.Buy : Side.Sell;

            api.instId = instId;
            api.posSide = posSide;
            api.tdMode = tdMode;
            api.side = side;
            api.ordType = "conditional";//单向止盈止损
            api.sz = size.ToString();

            var result = api.ExecSync();
            bool success = result.code == 0;

            if (success)
            {
                var data = result.data;

                success = data.Value<int>("sCode") == 0;
            }

            return success;
        }

        /// <summary>
        /// 单向止盈止损， 止盈止损
        /// 当用户进行单向止盈止损委托（ordType=conditional）时，如果用户同时传了止盈止损四个参数，只进行止损的功能校验，忽略止盈的业务逻辑校验。
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="stopLose"></param>
        /// <param name="price"></param>
        private bool SetStopPrice(long id, decimal amount, bool stopLose, decimal price)
        {
            Position pos = GetPosition(id);
            if (pos != null)
            {
                return this.SetStopPrice(pos.InstId, pos.PosSide, pos.MgnMode, amount, stopLose, price);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public bool SetTakeProfit(long id, decimal amount, decimal price)
        {
            return SetStopPrice(id,amount,false,price);
        }


        /// <summary>
        /// 设置止损价格
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        public bool SetStopLoss(long id, decimal price)
        {
            Position pos = this.GetPosition(id);

            if (pos != null)
            {
                InstrumentSwap instrument = InstrumentManager.SwapInstrument.GetInstrument(pos.InstId);
                return this.SetStopLoss(id, pos.AvailPos * instrument.CtVal, price);
            }

            return false;
        }

        public bool SetStopLoss(long id, decimal amount, decimal price)
        {
            return SetStopPrice(id, amount, true, price);
        }

        /// <summary>
        /// 平仓
        /// </summary>
        /// <param name="id">持仓ID</param>
        /// <param name="size">数量</param>
        /// <param name="odrType">交易模式</param>
        /// <param name="price">价格,如果是限价模式请填写价格</param>
        public bool ClosePosition(long id, decimal amount, string odrType, decimal price)
        {
            Position pos = GetPosition(id);
            if (pos != null)
            {

                InstrumentSwap instrument = InstrumentManager.SwapInstrument.GetInstrument(pos.InstId);
                int size = (int)Math.Round(amount / instrument.CtVal);

               return this.ClosePosition(id,size, odrType, price);
            }

            return false;
        }

        /// <summary>
        /// 平仓
        /// </summary>
        /// <param name="id">持仓ID</param>
        /// <param name="size">数量</param>
        /// <param name="odrType">交易模式</param>
        /// <param name="price">价格,如果是限价模式请填写价格</param>
        private bool ClosePosition(long id, int size, string odrType, decimal price)
        {
            Position pos = GetPosition(id);
            if (pos != null)
            {                 
                if (size > 0)
                {
                    size = Math.Min(size, Convert.ToInt32(pos.AvailPos));
                    var api = new SwapClose(pos.InstId, pos.PosSide, pos.MgnMode);
                    api.sz = "" + size;  
                    api.ordType = odrType;
                    api.px = price.ToString();
                    var result = api.ExecSync();
                    if(result.success)
                    {
                        if (pos.AvailPos <= size)//全平仓
                            monitor.RemovePosition(id);
                        else
                            pos.AvailPos -= size;

                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 调整保证金
        /// </summary>
        /// <param name="id">持仓id</param>
        /// <param name="amount">数量，正数增加负数减少</param>
        public void ChangeMarginBalance(long id, decimal amount)
        {
            if (amount != 0)
            {
                Position pos = GetPosition(id);
                if (pos != null)
                {
                    var api = new SWPMarginBalance(pos.InstId, pos.PosSide, amount);
                    api.ExecAsync();
                }
            }
        }

        /// <summary>
        /// 数量转换合约张数
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static int AmountToSize(string instId, decimal amount)
        {
            InstrumentSwap instrument = InstrumentManager.SwapInstrument.GetInstrument(instId);
            int size = (int)Math.Round(amount / instrument.CtVal);

            return size;
        }

        /// <summary>
        /// 合约张数转数量
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="sz">合约张数</param>
        /// <returns></returns>
        public static decimal SizeToAmount(string instId,int sz)
        {
            return SizeToAmount(instId, 1.0m * sz);
        }


        /// <summary>
        /// 合约张数转数量
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="sz">合约张数</param>
        /// <returns></returns>
        public static decimal SizeToAmount(string instId, decimal sz)
        {
            InstrumentSwap instrument = InstrumentManager.SwapInstrument.GetInstrument(instId);
            decimal amount = sz * instrument.CtVal;

            return amount;
        }

        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id">持仓ID</param>
        /// <param name="size">数量</param>
        public bool ClosePosition(long id, int size)
        {
           return this.ClosePosition(id, size, OrderType.Market, 0);
        }

        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id">持仓ID</param>
        /// <param name="size">数量</param>
        public bool ClosePosition(long id, decimal amount)
        {
           return this.ClosePosition(id, amount, OrderType.Market, 0);
        }

        /// <summary>
        /// 市价平仓
        /// </summary>
        /// <param name="id"></param>
        public bool ClosePosition(long id)
        {
            Position pos = GetPosition(id);
            if (pos != null)
            {
                int size = Convert.ToInt32(pos.Pos);
                return this.ClosePosition(id, size);
            }

            return false;
        }

        /// <summary>
        /// 建仓或加仓，按张数下单
        /// </summary>
        /// <param name="instId">产品类型</param>
        /// <param name="side">方向， 空头或多头</param>
        /// <param name="amount">数量（币种数量）</param>
        /// <param name="price">如果价格大于0 ，表示是限价挂单,为0则直接吃单</param>
        public long CreatePosition(string instId, PositionType side, int size, SwapMarginMode mode = SwapMarginMode.Isolated, decimal price = 0)
        {
            InstrumentSwap instrument = InstrumentManager.SwapInstrument.GetInstrument(instId);

            string errMsg;
            if (size >= instrument.MinSize)
            {
                string strSide = side == PositionType.Short ? PositionSide.Short : PositionSide.Long;
                var api = new SwapOpen(instId, strSide);
                api.sz = "" + size;
                api.ordType = price > 0 ? OrderType.Limit : OrderType.Market;//市价直接吃入
                api.px = price.ToString(instrument.PriceFormat);
                api.tdMode = mode == SwapMarginMode.Cross ? MarginMode.Cross : MarginMode.Isolated;
                var result = api.ExecSync();
                
                if (result.success)
                {
                    var data = result.data as JArray;
                    return data[0].Value<long>("ordId");
                }
                else
                {
                    errMsg = result.message;
                }
            }
            else
            {
                errMsg = $"{instId}下单数量不能小于最小值{instrument.MinSize}张或{instrument.CtVal}{instrument.CtValCcy}";
            }

            if (!string.IsNullOrEmpty(errMsg))
            {
                Logger.Instance.LogError(errMsg);
            }

            return 0;
        }

        /// <summary>
        /// 建仓或加仓，按数量下单
        /// </summary>
        /// <param name="instId">产品类型</param>
        /// <param name="side">方向， 空头或多头</param>
        /// <param name="amount">数量（币种数量）</param>
        /// <param name="price">如果价格大于0 ，表示是限价挂单</param>
        public long CreatePosition(string instId, PositionType side, decimal amount, SwapMarginMode mode = SwapMarginMode.Isolated, decimal price = 0)
        {
            int size = AmountToSize(instId, amount);// (int)Math.Round(amount / instrument.CtVal);

            return this.CreatePosition(instId, side, size,mode, price);
        }
 
        private static PositionManager _instance;
        public static PositionManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    var ins = new  PositionManager();

                    _instance = ins;
                }

                return _instance;
            }
        }
    }
}
