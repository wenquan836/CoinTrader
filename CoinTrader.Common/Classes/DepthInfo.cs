using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Classes
{
    public struct DepthInfo
    {
        public decimal Total { get; set; }
        public uint Orders { get; set; }
        public decimal Price { get; set; }
    }
}
