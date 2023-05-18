using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Util
{
    public static class JsonUtil
    {
        private static JsonSerializer serializer = null;

        private static JsonSerializer GetSerializer()
        {
            if (serializer == null)
                serializer = JsonSerializer.Create();

            return serializer;
        }

        public static string ObjectToJsonString(object obj)
        {
            using (StringWriter sw = new StringWriter())
            {
                var s = JsonSerializer.Create();
                s.Serialize(sw, obj);

                sw.Flush();

                var sb = sw.GetStringBuilder();

                sw.Close();

                return sb.ToString();
            }
        }

        public static T JsonStringToObject<T>(string json)
        {
            T result = default(T);

            using (StringReader sr = new StringReader(json))
            {
                var s = JsonSerializer.Create();
                JsonReader jr = new JsonTextReader(sr);
                result = s.Deserialize<T>(jr);
            }

            return result;
        }
    }
}
