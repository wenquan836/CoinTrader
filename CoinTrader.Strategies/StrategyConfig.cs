
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CoinTrader.Common.Util;
using Newtonsoft.Json.Linq;

namespace CoinTrader.Strategies
{
    public class FileCopyResult
    {
        public bool success;
        public string error;

        public FileCopyResult()
        {
            success = false;
            error = "";
        }
    }

    public static class StrategyConfig
    {
        /// <summary>
        /// 保存配置到文件
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static string SaveStrategyConfig(string instId, StrategyBase strategy)
        {

            var type = strategy.GetType();

            string configPath = GetConfigPath(instId, strategy);

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            JObject json = new JObject();

            foreach (var p in properties)
            {
                StrategyParameterAttribute attribute = p.GetCustomAttribute<StrategyParameterAttribute>();

                if (attribute == null)
                    continue;

                Type valueType = p.PropertyType;
                object value = p.GetValue(strategy);
                if (valueType == typeof(string))
                {
                    json[p.Name] = value.ToString();
                }
                else if (valueType == typeof(bool))
                {
                    json[p.Name] = (bool)value;
                }
                else if (valueType == typeof(DateTime))
                {
                    json[p.Name] = DateUtil.GetTimestampSec((DateTime)value);
                }
                else if(valueType.BaseType == typeof(Enum))
                {
                    json[p.Name] = value.ToString();
                }
                else
                {
                    double number = 0;

                    number = double.Parse(value.ToString());

                    json[p.Name] = number;
                }
            }

            try
            {
                File.WriteAllText(configPath, json.ToString());
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public static string GetDirectory()
        {
            string startPath = AppContext.BaseDirectory;
             
            var dir = Path.Combine(startPath, "StrategyConfigs");

            return dir;
        }

        private static string GetConfigPath(string instId, StrategyBase strategy)
        {
            var type = strategy.GetType();
            bool isEmulationMode =  strategy .IsEmulationMode;
            var dir = GetConfigDir(instId);

            if(isEmulationMode)
            {
                dir = Path.Combine(dir, "_emulation_");
            }

            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return Path.Combine(dir,  string.Format("{0}.json", type.Name));
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="instId"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static bool LoadStrategyConfig(string instId, StrategyBase strategy)
        {
            string configPath = GetConfigPath(instId, strategy);

            if(File.Exists(configPath))
            {
                string text = File.ReadAllText(configPath);
                JObject json = null;
                try
                {
                    json = JObject.Parse(text);
                }
                catch
                {
                    return false;
                }

                var type = strategy.GetType();

                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var p in properties)
                {
                    StrategyParameterAttribute attribute = p.GetCustomAttribute<StrategyParameterAttribute>();

                    if (attribute == null)
                        continue;

                    if (!json.ContainsKey(p.Name))
                        continue;

                    string name = p.Name;

                    Type valueType = p.PropertyType;
                    JToken value = json[name];


                    if (valueType == typeof(bool))
                    {
                        p.SetValue(strategy, value.Value<bool>());
                    }
                    else if (valueType == typeof(DateTime))
                    {
                        long timestamp = value.Value<long>();
                        p.SetValue(strategy, DateUtil.TimestampSecToDateTime(timestamp));
                    }
                    else if (valueType == typeof(string))
                    {
                        p.SetValue(strategy, value.Value<string>());
                    }
                    else if (valueType == typeof(int))
                    {
                        p.SetValue(strategy, value.Value<int>());
                    }
                    else if (valueType == typeof(uint))
                    {
                        p.SetValue(strategy, value.Value<uint>());
                    }
                    else if (valueType == typeof(ushort))
                    {
                        p.SetValue(strategy, value.Value<ushort>());
                    }
                    else if (valueType == typeof(byte))
                    {
                        p.SetValue(strategy, value.Value<byte>());
                    }
                    else if (valueType == typeof(short))
                    {
                        p.SetValue(strategy, value.Value<short>());
                    }
                    else if (valueType == typeof(char))
                    {
                        p.SetValue(strategy, value.Value<char>());
                    }
                    else if (valueType == typeof(float))
                    {
                        p.SetValue(strategy, value.Value<float>());
                    }
                    else if (valueType == typeof(decimal))
                    {
                        p.SetValue(strategy, value.Value<decimal>());
                    }

                    else if(valueType.BaseType == typeof(Enum))
                    {
                        p.SetValue(strategy, Enum.Parse(valueType, value.ToString()));// uint.Parse(value.ToString()));
                    }
                    else if (valueType == typeof(long))
                    {
                        p.SetValue(strategy, value.Value<long>());
                    }
                    else
                    {
                        p.SetValue(strategy,  value.Value<double>());
                    }
                }

                return true;
            }

            return false;
        }

        private static string GetConfigDir(string instId)
        {
            return Path.Combine(GetDirectory(), instId.ToLower());
        }

        public static Task<FileCopyResult> CopyConfigs(string sourceInstId, List<string> sourceFiles, List<string> targetInstIds)
        {
            return Task.Run<FileCopyResult>(() => {
                FileCopyResult result = new FileCopyResult();

                int fileCount = targetInstIds.Count * sourceFiles.Count;

                string configDir = GetDirectory();

                foreach (string s in targetInstIds)
                {
                    if(string.Compare(s,sourceInstId,true) == 0)
                    {
                        continue;
                    }

                    foreach (var f in sourceFiles)
                    {
                        string targetDir = GetConfigDir(s);


                        if (!Directory.Exists(targetDir))
                        {
                            try
                            {
                                Directory.CreateDirectory(targetDir);
                            }
                            catch (Exception ex)
                            {
                                result.error = ex.Message;

                                return result;
                            }
                        }

                        string sourceFilePath = Path.Combine(GetConfigDir(sourceInstId), f);

                        string targetPath = Path.Combine(targetDir, f);


                        try
                        {
                            File.Copy(sourceFilePath, targetPath, true);
                        }
                        catch (Exception ex)
                        {
                            result.error = ex.Message;

                            return result;
                        }
                    }
                }

                result.success = true;
                return result;
            });
        }
    }
}
