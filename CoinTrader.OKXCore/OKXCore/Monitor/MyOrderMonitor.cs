
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.REST;
using CoinTrader.OKXCore.Enum;
using CoinTrader.OKXCore.Entity;
using CoinTrader.Common;

namespace CoinTrader.OKXCore.Monitor
{
    [MonitorName(Name = "交易挂单")]
    public class MyOrderMonitor : RESTMonitor
    {
        private List<long> ids = new List<long>();
        private Dictionary<long, OrderBase> orderDict = new Dictionary<long, OrderBase>();

        public void AddUpdateOrder(OrderBase newOrderData)
        {
            if(newOrderData == null)
                throw new ArgumentNullException("newOrderData not by null");

            if (System.Threading.Monitor.TryEnter(orderDict))
            {
                OrderBase order;

                if(!orderDict.TryGetValue(newOrderData.PublicId, out order))
                {
                    order = new OrderBase();
                    order.CopyFrom(newOrderData);
                    orderDict.Add(order.PublicId, order);
                }
                else
                {
                    order.CopyFrom(newOrderData);
                }
 
                System.Threading.Monitor.Exit(orderDict);
            }
        }
        public void RemoveOrder(long id)
        {
            if (System.Threading.Monitor.TryEnter(orderDict))
            {
                orderDict.Remove(id);
                System.Threading.Monitor.Exit(orderDict);
            }
        }

        private void EachOrder(OrderSide side, Action<OrderBase> callback)
        {
            lock (orderDict)
            {
                foreach (var kv in orderDict)
                {
                    if(kv.Value.Side == side)  callback(kv.Value);
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
            lock (orderDict)
            {
                OrderBase order;
                ids.AddRange(orderDict.Keys);
               
                foreach (JToken jt in orderData as JArray)
                {
                     long id = jt.Value<long>("ordId");

                    if(!orderDict.TryGetValue(id, out order))
                    {
                        order = new OrderBase();
                        orderDict.Add(id, order);
                    }

                    ids.Remove(id);

                    try
                    {
                        order.ParseFromJson(jt);
                    }
                    catch (Exception ex)
                    {
                        orderDict.Remove(id);
                        Logger.Instance.LogError(" Order Data Error : \r\n" + orderData.ToString());
                        Logger.Instance.LogException(ex);
                        return;
                    }
                 }

                if(ids.Count > 0)
                {
                    foreach(var id in ids) 
                        orderDict.Remove(id);
                    ids.Clear();
                }

                this.Feed(); 
            }
        }
    }
}
