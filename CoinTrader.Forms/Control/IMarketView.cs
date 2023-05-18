using CoinTrader.Forms.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Forms.Control
{
    public interface IMarketView
    {
        decimal TotalAmount { get; }
        string InstId { get; }

        void SetInstId(string instId);
        void RefreshStrategies();
    }
}
