
using CoinTrader.Common;
using CoinTrader.Common;
using Newtonsoft.Json.Linq;
using CoinTrader.OKXCore.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Monitor
{

    /// <summary>
    /// REST接口的数据监视器
    /// </summary>
    public abstract class RESTMonitor : MonitorBase
    {
        protected RestAPIBase api = null;
        public RESTMonitor(RestAPIBase restApi, uint interval)
        {
            base.Interval = interval;
            this.api = restApi;
            this.api.OnData += Api_OnData;
        }

        private void Api_OnData(APIResult result)
        {
            if(isDisposed) return;

            if (!result.success)
            {
                string msg = result.message;
                InvokeOnError(result.code, msg);
                Logger.Instance.LogError(string.Format("{0} {1} {2} {3}", result.code, msg, this.GetType().Name, api.GetType().Name));
            }
            else
            {
                this.OnDataUpdate(result.data);
            }
        }

        protected virtual void OnDataUpdate(JToken data)
        {

        }

        protected override void RunInvoke()
        {
            api.ExecAsync();
        }

        public override void Dispose()
        {
            this.api.OnData -= this.Api_OnData;
            base.Dispose();
        }
    }
}
