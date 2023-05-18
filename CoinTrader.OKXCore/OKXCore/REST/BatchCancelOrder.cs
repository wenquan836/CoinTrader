using CoinTrader.Common;
using CoinTrader.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.REST
{
    /// <summary>
    /// 批量取消订单
    /// </summary>
    public class BatchCancelOrder : RestAPIBase
    {
        private JArray arrPost = new JArray();
        public BatchCancelOrder(string instId, IEnumerable<long> ids)
        : base(APIUrl.BatchOrdersCancel, Http.Method_Post)
        {
            foreach (var id in ids)
            {
                JObject obj = new JObject();
                obj["instId"] = instId;
                obj["ordId"] = id.ToString();
                arrPost.Add(obj);
            }
        }

        protected override string GetPostBody()
        {
            return arrPost.ToString();
        }
    }
}
