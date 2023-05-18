using CoinTrader.Forms.Event;
using CoinTrader.Common;
using CoinTrader.Common.Classes;
using CoinTrader.Common.Extend;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Forms.Entity
{
    /// <summary>
    /// 用于显示当前登录的账号,无其他作用
    /// </summary>
    public class LoginAccount
    { 
        public string RealName { get;  set; }
        public string LoginName { get; set; }

        public void SetLoginName(string val)
        {
            this.LoginName = val;
        }

        public bool ParseFromJson(JToken json)
        {
            try
            {
                this.RealName = json["RealName"].Value<string>();
                this.LoginName = json["LoginName"].Value<string>();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
