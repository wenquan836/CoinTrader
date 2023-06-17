
using CoinTrader.Common.Classes;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.Entity;
using CoinTrader.OKXCore.REST;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using CoinTrader.Common;

//https://www.okx.com/docs-v5/zh/#rest-api-account-get-positions

namespace CoinTrader.OKXCore.Monitor
{
    [MonitorName(Name = "合约持仓")]
    public class SWPPositionMonitor: RESTMonitor
    {
        private bool disposed = false;
        private List<long> ids = new List<long>();

        private Dictionary<long, Position> posDict = new Dictionary<long, Position>();
 
        public SWPPositionMonitor()
        : base(new SWPPositions(),300)
        {

        }

        public void RemovePosition(long id)
        {
            if (System.Threading.Monitor.TryEnter(posDict))
            {
                posDict.Remove(id);
                System.Threading.Monitor.Exit(posDict);
            }
        }
        public void EachPostion(Action<Position> callback)
        {
            lock(posDict)
            {
                foreach(var kv in posDict)
                {
                    callback(kv.Value);
                }
            }
        }

        public override void Dispose()
        {
            disposed = true;
            base.Dispose();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        protected override void OnDataUpdate(JToken data)
        {
            if (disposed) return;
            var array = data as JArray;

            lock (this.posDict)
            {
                Position pos; long posId;
                ids.AddRange(posDict.Keys);

                foreach (var item in array)
                {
                    posId = item.Value<long>("posId");
                    if (!posDict.TryGetValue(posId, out pos))
                    {
                        pos = new Position();
                        posDict.Add(posId, pos);
                    }

                    try
                    {
                        pos.ParseFromJson(item);
                    }
                    catch (Exception ex)
                    {
                        posDict.Remove(posId);
                        Logger.Instance.LogError(" Position Data Error : \r\n" + item.ToString());
                        Logger.Instance.LogException(ex);
                        return;
                    }
                    
                    ids.Remove(posId);
                }

                if (ids.Count > 0)
                {
                    foreach (var id in ids)
                    {
                        posDict.Remove(id);
                    }
                    ids.Clear();
                }
            }

            this.Feed();
        }
    }
}
