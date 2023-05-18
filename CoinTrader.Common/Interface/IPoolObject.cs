using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Interface
{
    /// <summary>
    /// 可复用对象池对象
    /// </summary>
    public interface IPoolObject
    {
        /// <summary>
        /// 当被复用时调用
        /// </summary>
        void PoolReserve();
    }
}
