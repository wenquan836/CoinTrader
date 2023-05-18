using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.OKXCore.VO
{
    public struct BalanceVO
    {

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency;

        /// <summary>
        /// 冻结
        /// </summary>
        public decimal Frozen;

        /// <summary>
        /// 有效
        /// </summary>
        public decimal Avalible;

        /// <summary>
        /// 总余额
        /// </summary>
        public decimal Total =>  Avalible + Frozen;
             
         
    }
}

