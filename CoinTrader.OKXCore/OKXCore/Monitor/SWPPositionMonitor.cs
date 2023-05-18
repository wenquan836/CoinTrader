
using CoinTrader.Common.Classes;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.REST;
using System;
using System.Collections.Generic;

//https://www.okx.com/docs-v5/zh/#rest-api-account-get-positions

namespace CoinTrader.OKXCore.Monitor
{
    [MonitorName(Name = "合约持仓")]
    public class SWPPositionMonitor: RESTMonitor
    {
        private bool disposed = false;

        private Pool<Position> positionPool = null;

        private Pool<Position> PositionPool
        {
            get
            {
                if(positionPool == null)
                    positionPool = Pool<Position>.GetPool();

                return positionPool;
            }
        }

        public SWPPositionMonitor()
        : base(new SWPPositions(),300)
        {

        }

        public void RemovePosition(long id)
        {
            if(System.Threading.Monitor.TryEnter(posList))
            {
                for (int i = 0; i < posList.Count; i++)
                {
                    if (posList[i].PosId == id)
                    {
                        posList.RemoveAt(i);
                        break;
                    }
                }

                System.Threading.Monitor.Exit(posList);
            }
        }
        public void EachPostion(Action<Position> callback)
        {
            lock(posList)
            {
                foreach(var item in posList)
                {
                    callback(item);
                }
            }
        }

        public override void Dispose()
        {
            lock (posList)
            {
                disposed = true;
                PositionPool.Put(posList);
                posList.Clear();
            }

            base.Dispose();
        }
       
        private List<Position> posList = new List<Position>();
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        protected override void OnDataUpdate(JToken data)
        {
            
            var array = data as JArray;
            lock (this.posList)
            {
                if(disposed) return;

                PositionPool.Put(this.posList);
                this.posList.Clear();
                
                foreach (var item in array)
                {
                    var pos = PositionPool.Get();

                    pos.ParseFromJson(item);
                    posList.Add(pos);
                }

                this.Feed();
            }
        }
    }
}
