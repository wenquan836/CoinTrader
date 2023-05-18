using CoinTrader.Strategies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CoinTrader.Forms.Strategies
{
    /// <summary>
    /// 策略组
    /// </summary>
    public class StrategyGroup
    {
        public string name;
        public StrategyType groupType;
        public List<Type> strategies = new List<Type>();
    }

    /// <summary>
    /// 策略类型
    /// </summary>
    public enum StrategyType
    {
        /// <summary>
        /// 资金管理策略
        /// </summary>
        Funds,
        /// <summary>
        /// 现货策略
        /// </summary>
        Spot,
        /// <summary>
        /// 合约策略
        /// </summary>
        Swap
    }

    internal class StrategyManager
    {
        private Dictionary< string, StrategyGroup> spotGroups = new Dictionary<string, StrategyGroup>();
        private Dictionary<string, StrategyGroup> swapGroups = new Dictionary<string, StrategyGroup>();
        private Dictionary<string, StrategyGroup> fundsGroups = new Dictionary<string, StrategyGroup>();
        private StrategyManager() { }

        private bool loadded = false;

        /// <summary>
        /// 加载策略，包括内置和外置
        /// </summary>
        /// <returns></returns>
        public string LoadStrategies()
        {
            if (!loadded)
            {
                loadded = true;
                AddStrategyFromAssembly(Assembly.GetExecutingAssembly());

                string err;
                var ass = StrategyCompiler.Compile(CustomizedStrategyPath, out err);
                if (ass != null)
                {
                    AddStrategyFromAssembly(ass);
                }

                return err;
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取策略组列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StrategyGroup> GetStrategyGroups(StrategyType strategyType)
        {
            switch(strategyType)
            {
                case StrategyType.Swap:
                    return swapGroups.Values.ToList();
                case StrategyType.Spot:
                    return spotGroups.Values.ToList();
                case StrategyType.Funds:
                    return fundsGroups.Values.ToList();
            }

            return new List<StrategyGroup>();
        }

        /// <summary>
        /// 获取策略列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Type> GetStrategyTypeList(StrategyType strategyType)
        {
            var result = new List<Type>();
            IList<StrategyGroup> groupList = null;
            switch (strategyType)
            {
                case StrategyType.Swap:
                    groupList = swapGroups.Values.ToList();
                    break;
                case StrategyType.Spot:
                    groupList = spotGroups.Values.ToList();
                    break;
                case StrategyType.Funds:
                    groupList = fundsGroups.Values.ToList();
                    break;
            }

            if(groupList != null)
            {
                foreach(var group in groupList)
                {
                    result.AddRange(group.strategies);
                }
            }

            return result;
        }

        private string CustomizedStrategyPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StrategiesCustomized");
            }
        }

        /// <summary>
        /// 从程序集加载策略
        /// </summary>
        /// <param name="assembly"></param>
        private void AddStrategyFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();
            
            StrategyGroup group;
            string groupName;
            StrategyAttribute attr;
            StrategyType strategyType = StrategyType.Funds;

            foreach (var type in types)
            {
                Dictionary<string, StrategyGroup> dict = null;
                if (type.IsSubclassOf(typeof(SpotStrategyBase)))
                {
                    strategyType = StrategyType.Spot;
                    dict = spotGroups;
                }
                else if (type.IsSubclassOf(typeof(SwapStrategyBase)))
                {
                    strategyType = StrategyType.Swap;
                    dict = swapGroups;
                }
                else if (type.IsSubclassOf(typeof(FundsStrategyBase)))
                {
                    strategyType = StrategyType.Funds;
                    dict = fundsGroups;
                }

                if (dict != null)
                {
                    attr = type.GetCustomAttribute<StrategyAttribute>();
                    if (attr == null)
                    {
                        attr = new StrategyAttribute();
                        attr.Name = type.Name;
                    }

                    groupName = string.IsNullOrEmpty(attr.Group) ? attr.Name : attr.Group;
                    if (!dict.TryGetValue(groupName, out group))
                    {
                        group = new StrategyGroup();
                        group.name = groupName;
                        group.groupType = strategyType;
                        dict.Add(groupName, group);
                    }
                    group.strategies.Add(type);
                }
            }
        }

        private static StrategyManager _instance = new StrategyManager();
        public static StrategyManager Instance => _instance;
    }
}
