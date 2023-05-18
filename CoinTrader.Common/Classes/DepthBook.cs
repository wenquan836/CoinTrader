using CoinTrader.Common.Interface;
using System;
using System.Collections.Generic;


namespace CoinTrader.Common.Classes
{

    public enum DepthBookList
    {
        ASK = 0,
        BID = 1,
    }
    public class DepthBook:IDepthProvider
    {
        private List<DepthInfo> askList = new List<DepthInfo>(5);
        private List<DepthInfo> bidList = new List<DepthInfo>(5);

        public DateTime Time
        {
            get;
            set;
        }

        public void EachDepthBook(DepthBookList listType, Action<DepthInfo> callback)
        {
            IList<DepthInfo> list = null;

            switch (listType)
            {
                case DepthBookList.ASK:
                    list = this.askList;
                    break;
                case DepthBookList.BID:
                    list = this.bidList;
                    break;
            }

            lock (list)
            {
                foreach (var d in list)
                {
                    callback(d);
                }
            }
        }

        public void Update(DepthBookList listType, IEnumerable<DepthInfo> datas)
        {
            List<DepthInfo> list = null;

            switch (listType)
            {
                case DepthBookList.ASK:
                    list = this.askList;
                    break;
                case DepthBookList.BID:
                    list = this.bidList;
                    break;
            }

            lock(list)
            {
                list.Clear();
                list.AddRange(datas);
            }
        }
    }
}
