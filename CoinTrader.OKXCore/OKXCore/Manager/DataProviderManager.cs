using CoinTrader.OKXCore;
using System;
using System.Collections.Generic;


namespace CoinTrader.OKXCore.Manager
{   
    internal class DataProviderItem
    {
        public string InstrumentId { get; set; }
        public MarketDataProvider Provider { get; set; }
        public int ReferenceTimes { get; set; } 
    }

    public sealed class DataProviderManager
    {
        private readonly Dictionary<string, DataProviderItem> providers = new Dictionary<string, DataProviderItem>();

        private DataProviderManager() { }

        /// <summary>
        /// 获取一个数据驱动器
        /// </summary>
        /// <param name="instId"></param>
        /// <returns></returns>
        public MarketDataProvider GetProvider(string instId)
        {
            DataProviderItem item;
            if(!this.providers.TryGetValue(instId, out item))
            {
                var instrument = InstrumentManager.GetInstrument(instId);

                //不存在交易对
                if(instrument == null)
                {
                    return null;
                }

                MarketDataProvider provider = new MarketDataProvider(instId);
                item = new DataProviderItem();
                item.InstrumentId = instId;
                item.Provider = provider;
                this.providers.Add(instId, item);
            }

            item.ReferenceTimes++;

            return item.Provider;
        }

        /// <summary>
        /// 数据驱动器
        /// </summary>
        /// <param name="provider"></param>
        public void ReleaseProvider(MarketDataProvider provider)
        {
            if(provider == null)
                throw new ArgumentNullException("provider not by null");

            var instId = provider.InstrumentId;
            DataProviderItem item = this.providers.ContainsKey(instId) ? providers[instId] : null;

            if(item != null && item.Provider == provider)
            {
                item.ReferenceTimes--;
                
                if(item.ReferenceTimes <=0)
                {
                    item.Provider.Dispose(); 
                    providers.Remove(instId);
                }
            }
        }

        private static object lockObj = new object();
        private static DataProviderManager _instance;

        public static DataProviderManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock (lockObj)
                    {
                        if(_instance == null)
                            _instance = new DataProviderManager();
                    }
                }

                return _instance;
            }
        }
    }
}
