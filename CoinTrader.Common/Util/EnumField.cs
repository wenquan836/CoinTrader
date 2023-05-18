using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Util
{
    /// <summary>
    /// 把枚举的字段转为下拉列表的项
    /// </summary>
    public class EnumField
    {
        public string Description { get; set; }
        public string ValueName { get; set; }
        public Type EnumType { get; set; }

        public EnumField(string name, string description, Type enumType)
        {
            this.ValueName = name;
            this.Description = description;
            this.EnumType = enumType;
        }

        public override string ToString()
        {
            return this.Description;
        }

        /// <summary>
        /// 获得枚举的所有项
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IList<EnumField> GetEnumFields(Type enumType)
        {
            var fileds = enumType.GetFields();

            List<EnumField> infos = new List<EnumField>();

            foreach (var f in fileds)
            {
                if (!f.IsStatic) continue;

                object[] objs = f.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
                if (objs.Length > 0)
                {
                    DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
                    infos.Add(new EnumField(f.Name, descriptionAttribute.Description, enumType));
                }
                else //当描述属性没有时，直接返回名称
                {
                    infos.Add(new EnumField(f.Name, f.Name, enumType));
                }
            }

            return infos;
        }

        /// <summary>
        /// 获得枚举的所有项
        /// </summary>
        /// <returns></returns>
        public static IList<EnumField> GetEnumFields<T>() where T:Enum
        {
            var type = typeof(T);
            return GetEnumFields(type);
        }
    }
}
