using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Util
{
    public static class StringUtil
    {
        /// <summary>
        /// 带单位的端字符串
        /// </summary>
        /// <param name="num"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string ToShortNumber(decimal num, int precision)
        {
            if (num >= 1000000)
                return string.Format("{0}M", Math.Round(num / 1000000, 3));
            if (num >= 10000)
                return string.Format("{0}K", Math.Round(num / 1000, 2));
            return Math.Round(num, precision).ToString();
        }
    }
}
