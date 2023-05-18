
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace CoinTrader.Common
{
    /// <summary>
    /// 本地存储类
    /// </summary>
    public static class LocalStorage
    {
        static JObject storageObj = null;
        static object LockObj = new object();
        public static T GetValue<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                return default(T);
            JObject storage = GetStorageObject();
            JToken token = storage[key];
            return token != null ? token.ToObject<T>() : default(T);
        }

        private static JObject GetStorageObject()
        {
            if (storageObj == null)
            {
                lock (LockObj)
                {
                    if (storageObj == null)
                    {
                        var path = GetStoreFilePath();

                        if (File.Exists(path))
                        {
                            try
                            {
                                string str = File.ReadAllText(path);
                                JObject obj = null;
                                try
                                {
                                    obj = JObject.Parse(str);
                                }
                                catch
                                {

                                }

                                storageObj = obj;
                            }
                            catch
                            {

                            }
                        }

                        if (storageObj == null)
                        {
                            storageObj = new JObject();
                        }
                    }
                }
            }

            return storageObj;
        }

        private static string GetStoreFilePath()
        {
            return Path.Combine(AppContext.BaseDirectory, "Storage.json");
        }

        public static void SetValue(string key, object value)
        {
            JObject storage = GetStorageObject();
            storage[key] = JToken.FromObject(value);
            Save(storage);
        }

        private static void Save(JObject storage)
        {
            var path = GetStoreFilePath();
            try
            {
                File.WriteAllText(path, storage.ToString());
            }
            catch(Exception ex)
            {
                Logger.Instance.LogException(ex);
            }
        }
    }
}
