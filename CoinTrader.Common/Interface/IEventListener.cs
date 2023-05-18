using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinTrader.Common.Interface
{
    public class EventListenItem
    {
        public EventListenItem()
        {

        }

        public EventListenItem(string name, Action<object> callback)
        {
            this.Name = name;
            this.Callback = callback;
        }
        public string Name { get; set; } = "";
        public Action<object> Callback { get; set; }
    }
    public interface IEventListener
    {
        IEnumerable<EventListenItem> GetEvents();
    }
}
