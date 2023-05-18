using CoinTrader.Common;
using CoinTrader.Common.Classes;
using CoinTrader.Common;
using CoinTrader.Common.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CoinTrader.Common.Util;

namespace CoinTrader.OKXCore.REST
{
    public abstract class RestAPIBase
    {
        private volatile  bool isBusy = false;//避免同一个API复用的时候连续的请求造成阻塞
        private bool useSelfApi = false;
        private API apiInfo;

        static Pool<Dictionary<string, string>> headPool = null;
        static Pool<Dictionary<string, string>> HeadPool
        {
            get
            {
                if (headPool == null)
                    headPool = Pool<Dictionary<string, string>>.GetPool();
                return headPool;
            }
        }

        /// <summary>
        /// 单独设置API验证信息的接口
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="apiKey"></param>
        /// <param name="passphrase"></param>
        /// <param name="isSimulated"></param>
        public void SetAPI(string secretKey, string apiKey, string passphrase, bool isSimulated)
        {
            this.apiInfo.ApiKey= apiKey;
            this.apiInfo.Passphrase= passphrase;
            this.apiInfo.SecretKey= secretKey;
            this.apiInfo.IsSimulated= isSimulated;
            useSelfApi = true;
        }

        protected bool NeedAuthentication { get; set; }

        /// <summary>
        /// 生成请求头
        /// </summary>
        /// <param name="apiInfo">Api设置</param>
        /// <param name="buildAuthentication">是否生成认证签名</param>
        /// <param name="url">请求的地址</param>
        /// <param name="method">post还是get</param>
        /// <param name="body">post的数据体</param>
        /// <returns></returns>
        internal static void GenerateHeader(API apiInfo,bool buildAuthentication, string url, string method, string body, ref Dictionary<string,string> result)
        {
            if (buildAuthentication)
            {
                url = url.Substring(APIUrl.UrlRoot.Length);
                string timestamp = DateUtil.GetServerTimeISO8601();
                result["OK-ACCESS-KEY"] = apiInfo.ApiKey;
                result["OK-ACCESS-SIGN"] = Crypt.GenareteSign(apiInfo.SecretKey, timestamp, method, url, body != null ? body : string.Empty);
                result["OK-ACCESS-PASSPHRASE"] = apiInfo.Passphrase;
                result["OK-ACCESS-TIMESTAMP"] = timestamp.ToString();
                if (apiInfo.IsSimulated) //加入模拟盘参数
                    result["x-simulated-trading"] = "1";
            }
        }
        protected virtual string GetPostBody()
        {
            var s = JsonSerializer.Create();
            using (StringWriter sw = new StringWriter())
            {
                s.Serialize(sw, this);
                sw.Flush();
                var args = sw.GetStringBuilder().ToString();
                sw.Close();
                return args;
            }
        }

        /// <summary>
        /// 属性反射缓存
        /// </summary>
        private PropertyInfo[] queryProperties = null;

        /// <summary>
        /// 同步调用
        /// </summary>
        /// <returns></returns>
        public APIResult ExecSync()
        {
            APIResult result = default;

            if (isBusy)
            {
                result.code = -1;
                result.message = "busy";
                return result;
            }

            isBusy = true;
            string postData = null;

            var address = this.Address;

            if (Method == Http.Method_Post)
            {
                postData = this.GetPostBody();
            }
            else
            {
                if (this.AutoBuildAddress)
                {
                    if (queryProperties == null)
                    {
                        PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                        this.queryProperties = properties;
                    }

                    bool hasQuery = address.Contains("?");

                    foreach (PropertyInfo p in this.queryProperties)
                    {
                        object val = p.GetValue(this);

                        if (val != null)
                        {
                            var strVal = val.ToString();
                            if (!string.IsNullOrEmpty(strVal))
                            {
                                address = string.Concat(address, (hasQuery ? "&" : "?"), HttpUtility.UrlEncode(p.Name), "=", HttpUtility.UrlEncode(strVal));
                                hasQuery = true;
                            }
                        }
                    }
                }
            }

            Http.RequestEncoding = Encoding.ASCII; //这里要用ascii编码， 否则会出现签名错误的问题；
            API api = useSelfApi ? this.apiInfo :APIStorage.APIInfo;
            Dictionary<string, string> head = HeadPool.Get();
            head.Clear();

            GenerateHeader(api, NeedAuthentication, address, Method, postData, ref head);
            JObject response = Http.HttpSend(address, this.Method, postData, head);

            result.code = response.Value<int>("code");
            if (result.code == 0)
            {
                result.message = string.Empty;
                result.data = response["data"];
            }
            else
            {
                result.message = response.Value<string>("msg");
            }

            head.Clear();
            HeadPool.Put(head);

            isBusy = false;
            return result;
        }

        /// <summary>
        /// 异步Task调用
        /// </summary>
        /// <returns></returns>
        public Task<APIResult> Exec()
        {
            return Task.Run(() => { return this.ExecSync(); });
        }

        /// <summary>
        /// 异步调用但不等待结果，而是通过回调的方式通知接收方
        /// </summary>
        public async void ExecAsync()
        {
            var result = await this.Exec();
            this.OnReceiveData(result);
        }

        protected string Method = Http.Method_Post;
        protected string Address { get; set; }
        protected bool AutoBuildAddress = true;

        public event Action<APIResult> OnData;

        public RestAPIBase()
        {

        }

        public RestAPIBase(string address)
            : this(address, Http.Method_Get)
        {

        }

        public RestAPIBase(string address, string method)
        {
            this.NeedAuthentication = true;
            this.Address = address;
            this.Method = method;
        }

        protected virtual void OnReceiveData(APIResult data)
        {
            this.OnData?.Invoke(data);
        }
    }
}
