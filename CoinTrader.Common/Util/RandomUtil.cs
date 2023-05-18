using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Util
{
    public static class RandomUtil
    {
        static  Random rnd = new Random();

        public static double Value=> rnd.NextDouble();
        public static int Range(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public static double Range(double min, double max)
        {
            return rnd.NextDouble() * (max - min) + min;
        }
    }
}
