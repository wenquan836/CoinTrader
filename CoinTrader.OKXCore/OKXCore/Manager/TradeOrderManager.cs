using CoinTrader.Common.Classes;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Monitor;
using CoinTrader.OKXCore.REST;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Manager
{
    public class TradeOrderManager
    {
        private readonly string CodeField = "sCode";

        MonitorSchedule monitorManager = null;
        MyOrderMonitor monitor = null;
        public TradeOrderManager()
        {
            this.monitorManager = MonitorSchedule.Default;
            this.AddOrderMonitor();
        }

        Pool<OrderBase> pool = null;

        Pool<OrderBase> OrderPool
        {
            get
            {
                if(pool == null)
                    pool = Pool<OrderBase>.GetPool();

                return pool;
            }
        }

        public void EachSellOrder(Action<OrderBase> callback)
        {
            if (monitor != null)
                monitor.EachSellOrder(callback);
        }

        private bool ApiExecuteResult(APIResult apiResult, out JToken data)
        {
            if (apiResult.code == 0)
            {
                JToken result = apiResult.data;

                if (result is JArray)
                    result = result[0];

                if (result.Value<int>(CodeField) == 0)
                {
                    data = result;
                    return true;
                }
            }
            data = null;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="instId"></param>
        /// <returns></returns>
        public OrderBase QueryOrder(long orderId, string instId)
        {
            return this.QueryOrder(orderId, instId, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="instId"></param>
        /// <returns></returns>
        public Task<OrderBase> QueryOrderAsync(long orderId, string instId)
        {
            return Task.Run(()=> this.QueryOrder(orderId, instId, false));
        }

        /// <summary>
        /// 查询订单状态
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="instId">产品id</param>
        /// <param name="updateMonitorBuffer">是否同时更新监视器缓存</param>
        /// <returns></returns>
        public OrderBase QueryOrder(long orderId, string instId, bool updateMonitorBuffer)
        {
            OrderQuery api = new OrderQuery(instId, orderId);
            var apiResult = api.ExecSync();

            if (ApiExecuteResult(apiResult, out JToken result))
            {
                OrderBase order = OrderPool.Get();

                order.ParseFromJson(result);

                if (updateMonitorBuffer)
                {
                    this.monitor.AddUpdateOrder(order);
                }

                return order;
            }
            return null;
        }

        /// <summary>
        /// 修改挂单
        /// </summary>
        /// <param name="id">订单id</param>
        /// <param name="instId">币种</param>
        /// <param name="amount">新数量</param>
        /// <param name="price">新价格</param>
        /// <param name="cancelOrderWhenFailed">如果修改失败则撤销订单</param>
        /// <returns>如果成功返回true</returns>
        public Task<bool> ModifyOrderAsync(long id, string instId, decimal amount, decimal price, bool cancelOrderWhenFailed)
        {
            return Task.Run<bool>(() =>
            {
               return ModifyOrder(id,instId, amount, price, cancelOrderWhenFailed);
            });
        }

        /// <summary>
        /// 修改挂单
        /// </summary>
        /// <param name="id">订单id</param>
        /// <param name="instId">币种</param>
        /// <param name="amount">新数量</param>
        /// <param name="price">新价格</param>
        /// <param name="cancelOrderWhenFailed">如果修改失败则撤销订单</param>
        /// <returns>如果成功返回true</returns>
        public bool ModifyOrder(long id, string instId, decimal amount, decimal price, bool cancelOrderWhenFailed)
        {
            OrderModify api = new OrderModify(id, instId, amount, price);
            api.cxlOnFail = cancelOrderWhenFailed;

            var apiResult = api.ExecSync();
            if (this.ApiExecuteResult(apiResult, out JToken result))
            {
                return true;
            }
            return false;
        }

        public OrderBase GetOrder(long id, string instId)
        {
            OrderBase order = null;
            this.monitor.EachBuyOrder((odr) =>
            {
                if (order == null && odr.PublicId == id && string.Compare(instId, odr.InstId) == 0)
                {
                    order = OrderPool.Get().CopyFrom(odr);
                }
            });

            if (order == null)
            {
                this.monitor.EachSellOrder((odr) =>
                {
                    if (order == null && odr.PublicId == id && string.Compare(instId, odr.InstId) == 0)
                    {
                        order = OrderPool.Get().CopyFrom(odr);
                    }
                });
            }

            return order;
        }

        /// <summary>
        /// 发送现货订单
        /// </summary>
        /// <param name="instId">交易产品id</param>
        /// <param name="amount">数量</param>
        /// <param name="price">价格</param>
        /// <param name="side">方向</param>
        /// <param name="postOnly">是否是高级限价委托</param>
        /// <param name="addToMonitorBuffer">是否更新到数据监视器</param>
        /// <returns></returns>
        public Task<OrderBase> PlaceOrderAsync(string instId, decimal amount, decimal price, OrderSide side, bool postOnly, bool addToMonitorBuffer)
        {
            return Task.Run<OrderBase>(() =>
            {
                CreateOrder api = new SpotCreateOrder(instId, side);
                api.sz = amount.ToString();
                api.px = price.ToString();

                if (postOnly)
                {
                    api.ordType = OrderType.PostOnly;
                }

                long orderId = 0;
                var apiResult =  api.ExecSync();

                if (this.ApiExecuteResult(apiResult, out JToken result))
                {
                    orderId = result.Value<long>("ordId");
                    OrderBase order = this.QueryOrder(orderId, instId);
                    if (addToMonitorBuffer && order != null)
                    {           
                        this.monitor.AddUpdateOrder(order);
                        OrderBase ret = OrderPool.Get();
                        ret.CopyFrom(order);
                        order = ret;
                    }
                    return order;
                }

                return null;
            });
        }

        /// <summary>
        /// 发送现货订单
        /// </summary>
        /// <param name="instId">交易产品id</param>
        /// <param name="amount">数量</param>
        /// <param name="price">价格</param>
        /// <param name="side">方向</param>
        /// <param name="postOnly">是否是高级限价委托</param>
        /// <param name="addToMonitorBuffer">是否更新到数据监视器</param>
        /// <returns></returns>
        public OrderBase PlaceOrder(string instId, decimal amount, decimal price, OrderSide side, bool postOnly, bool addToMonitorBuffer)
        {
            CreateOrder api = new SpotCreateOrder(instId, side);
            api.sz = amount.ToString();
            api.px = price.ToString();

            if (postOnly)
            {
                api.ordType = OrderType.PostOnly;
            }

           
            var apiResult = api.ExecSync();

            if (this.ApiExecuteResult(apiResult, out JToken result))
            { 
                long orderId  = result.Value<long>("ordId");
                OrderBase order = this.QueryOrder(orderId, instId);
                if (addToMonitorBuffer && order != null)
                {
                    this.monitor.AddUpdateOrder(order);
                    OrderBase ret = OrderPool.Get();
                    ret.CopyFrom(order);
                    order = ret;
                }
                return order;
            }

            return null;
        }
        public void EachBuyOrder(Action<OrderBase> callback)
        {
            if(this.monitor != null) this.monitor.EachBuyOrder(callback);
        }

        /// <summary>
        /// 批量撤销订单
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchCancelOrder(string instId, IEnumerable<long> ids)
        {
            return  new BatchCancelOrder(instId, ids).ExecSync().success;
        }

        /// <summary>
        /// 批量撤销订单
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="ids"></param>
        public void BatchCancelOrderAsync(string instId, IEnumerable<long> ids)
        {
            new BatchCancelOrder(instId, ids).ExecAsync();
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="instrumentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CancelOrder(string instrumentId, long id)
        {
            return new CancelOrder(instrumentId, id.ToString()).ExecSync().success;
        }

        /// <summary>
        /// 取消订单异步
        /// </summary>
        /// <param name="instrumentId"></param>
        /// <param name="id"></param>
        public  void CancelOrderAsync(string instrumentId, long id)
        {
            new CancelOrder(instrumentId, id.ToString()).ExecAsync();
        }

        /// <summary>
        /// 强制进行一次订单同步
        /// </summary>
        public void SyncOrders()
        {
            monitor.ForceUpdate();
        }

        private void AddOrderMonitor()
        {
            if (monitor == null)
            {
                monitor = new MyOrderMonitor();
                monitorManager.AddMonotor(monitor);
            }
        }

        private static TradeOrderManager _instance = null;
        public static TradeOrderManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TradeOrderManager();

                return _instance;
            }
        }
    }
}
