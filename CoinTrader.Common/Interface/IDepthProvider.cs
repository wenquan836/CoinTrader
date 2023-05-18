using CoinTrader.Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Interface
{
    public interface IDepthProvider
    {
        void EachDepthBook(DepthBookList side, Action<DepthInfo> callback);
    }
}
