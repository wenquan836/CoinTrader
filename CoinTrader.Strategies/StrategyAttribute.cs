using System;

namespace CoinTrader.Strategies
{
    /// <summary>
    /// 策略定义
    /// </summary>
    public class StrategyAttribute : Attribute
    {
        public StrategyAttribute()
        :this("","")
        {

        }
        public StrategyAttribute(string name, string group = "")
        {
            Name = name;
            Group = group;
        }

        /// <summary>
        /// 策略名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 策略组名称
        /// </summary>
        public string Group { get; set; }
    }

    /// <summary>
    /// 策略参数
    /// </summary>
    public class StrategyParameterAttribute : Attribute
    {
        public string Name { get; set; }
        public string Group { get; set; } //参数分组
        /**
         * 依赖另外一个值，才有效
         */
        public string Dependent { get; set; }
        public object DependentValue { get; set; }

        public double Min = double.MinValue;
        public double Max = double.MaxValue;

        public string Unit = "";

        /// <summary>
        /// 参数说明
        /// </summary>
        public string Intro { get; set; }
    }
}
