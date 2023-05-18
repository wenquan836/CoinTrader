using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Extend
{
    public static class JTokenExtend
    {
        /// <summary>
        /// 如果json里获取不到对应key的值就返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jtoken"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ValueWithDefault<T>(this JToken jtoken, string key, T defaultValue = default)
        {
            var jv = jtoken[key];
            return !(jv == null || string.IsNullOrEmpty((string)jv)) ? jv.Value<T>() : defaultValue;
        }
    }
}
