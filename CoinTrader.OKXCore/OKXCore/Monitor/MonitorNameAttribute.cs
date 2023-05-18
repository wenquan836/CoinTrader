using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Monitor
{
    public class MonitorNameAttribute : Attribute
    {
        public string Name { get; set; }
    }

}
