using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Forms.Entity
{
    /// <summary>
    /// 存储api信息
    /// </summary>
    public class ApiInfo
    {
        public static readonly string WSSPublicDefaultAddress = "wss://ws.okx.com:8443/ws/v5/public";
        public static readonly string WSSPrivateDefaultAddress = "wss://ws.okx.com:8443/ws/v5/private";

        public static readonly string WSSPublicSimulatedAddress = "wss://wspap.okx.com:8443/ws/v5/public?brokerId=9999";
        public static readonly string WSSPrivateSimulatedAddress = "wss://wspap.okx.com:8443/ws/v5/private?brokerId=9999";
        public string ApiKey { get;  set; } //
        public string Passphrase { get;  set; }// 
        public string SecretKey { get;  set; }//
        public string ApiAddress
        {
            get
            {
                return IsSimulated ? WSSPublicSimulatedAddress : WSSPublicDefaultAddress;
            }
        }

        public string PrivateAddress
        {
            get
            {
                return IsSimulated ? WSSPrivateSimulatedAddress : WSSPrivateDefaultAddress;
            }
        }

        public ApiInfo()
        {
            //this.ApiAddress = WSSPublicDefaultAddress;
            //this.PrivateAddress = WSSPrivateDefaultAddress;
        }

        /// <summary>
        /// 是否是模拟盘
        /// </summary>
        public bool IsSimulated { get; set; } 

        public bool ParseFromJson(JToken json)
        {
            try
            {
                this.ApiKey = json["ApiKey"] != null ? json["ApiKey"].Value<string>(): json["Key"].Value<string>();
                this.Passphrase = json["Passphrase"].Value<string>();
                this.SecretKey = json["SecretKey"].Value<string>();
                //this.ApiAddress = json["ApiAddress"].Value<string>();
                this.IsSimulated = json["IsSimulated"] != null ? json["IsSimulated"].Value<bool>():false;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
