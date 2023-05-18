
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.REST;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Entity;
using CoinTrader.Common.Classes;
using CoinTrader.Common;
using System.Diagnostics;

namespace CoinTrader.OKXCore.Monitor
{
    [MonitorName(Name = "交易挂单")]
    public class MyOrderMonitor : RESTMonitor
    {
        private object locker = new object();
        private List<OrderBase> orders = new List<OrderBase>();
        private List<OrderBase> temp = new List<OrderBase>();

        Pool<OrderBase> orderPool = null;

        private Pool<OrderBase> OrderPool
        {
            get
            {
                if (orderPool == null)
                    orderPool = Pool<OrderBase>.GetPool();

                return orderPool;
            }
        }
        public void AddUpdateOrder(OrderBase newOrderData)
        {
            if (System.Threading.Monitor.TryEnter(locker))
            {
                OrderBase finddedOrder = null;

                foreach (var order in orders)
                {
                    if (order.PublicId == newOrderData.PublicId && string.Compare(newOrderData.InstId, order.InstId, true) == 0)
                    {
                        finddedOrder = order;
                        break;
                    }
                }

                if (finddedOrder != null)
                {
                    finddedOrder.CopyFrom(newOrderData);
                }
                else
                {
                    var order = OrderPool.Get();
                    order.CopyFrom(newOrderData);
                    orders.Add(order);
                }

                System.Threading.Monitor.Exit(locker);
            }
        }
        public void RemoveOrder(long id)
        {
            if (System.Threading.Monitor.TryEnter(locker))
            {
                for (int i = 0; i < orders.Count; i++)
                {
                    if (orders[i].PublicId == id)
                    {
                        orders.RemoveAt(i);
                        break;
                    }
                }

                System.Threading.Monitor.Exit(locker);
            }
        }

        private void EachOrder(OrderSide side, Action<OrderBase> callback)
        {

            lock (locker)
            {
                foreach (var o in orders)
                {
                    if(o.Side == side)  callback(o);
                }
            }
        }

        public void EachSellOrder(Action<OrderBase> callback)
        {
            EachOrder(OrderSide.Sell, callback);
        }

        public void EachBuyOrder(Action<OrderBase> callback)
        {
            EachOrder(OrderSide.Buy, callback);
        }

        public MyOrderMonitor()
            : base(new OrderList(), 300)
        {

        }

        protected override void OnDataUpdate(JToken orderData)
        {            
            lock (locker)
            {
                OrderPool.Put(this.orders);
                this.orders.Clear();

                foreach (JToken jt in orderData as JArray)
                {
                    OrderBase order = OrderPool.Get();

                    try
                    {
                        order.ParseFromJson(jt);
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.LogError(" Order Data Error : \r\n" + orderData.ToString());
                        Logger.Instance.LogException(ex);
                        return;
                    }

                    orders.Add(order);
                }

                this.Feed(); 
            }
        }
    }
}
