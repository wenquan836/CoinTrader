using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.Interface
{
    public interface IMonitor
    {
        bool Effective { get;}
        void Update(int deltaTime);
    }
}
