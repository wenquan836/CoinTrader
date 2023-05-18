using CoinTrader.Common.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CoinTrader.Common
{
    /// <summary>
    /// 事件中心
    /// </summary>
    public class EventCenter
    {
        private EventCenter()
        {

        }

        private readonly ConcurrentDictionary<string, HashSet<Action<object>>> eventListners = new ConcurrentDictionary<string, HashSet<Action<object>>>();

        /// <summary>
        /// 推送事件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="argument"></param>
        public void Send(string name, object argument = null)
        {
            if (eventListners.TryGetValue(name, out var listeners))
            {
                lock (listeners)
                {
                    //List<Action<Object>> temp = listeners.ToList();
                    foreach (var l in listeners)
                    {
                        l(argument);
                    }
                }
            }
        }


        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="eventListener"></param>
        public void RemoveListener(IEventListener eventListener)
        {
            IEnumerable<EventListenItem> items = eventListener.GetEvents();

            foreach (EventListenItem item in items)
            {
                this.RemoveListener(item.Name, item.Callback);
            }
        }

        /// <summary>
        /// 插入监听器
        /// </summary>
        /// <param name="eventListener"></param>
        public void AddListener(IEventListener eventListener)
        {
           IEnumerable< EventListenItem> items = eventListener.GetEvents();

            foreach(EventListenItem item in items)
            {
                this.AddListener(item.Name, item.Callback);
            }
        }

        private void AddListener(string name, Action<object> listener)
        {
            lock(this)
            {
                HashSet<Action<object>> listeners = null;
                if(eventListners.TryGetValue(name,out listeners))
                {
                    listeners = eventListners[name];
                }
                else
                {
                    listeners = new HashSet<Action<object>>();
                    eventListners[name] = listeners;
                }


                lock (listeners)
                {
                    if (!listeners.Contains(listener))
                    {
                        listeners.Add(listener);
                    }
                }
            }
        }

        private void RemoveListener(string name, Action<object> listener)
        {
            if (eventListners.TryGetValue(name, out var listeners))
            {
                lock (listeners)
                {
                    listeners.Remove(listener);
                }
            }
        }

        private static EventCenter _instance = null;
        private static object locker = new object();
        public static EventCenter Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(locker)
                    {
                        if(_instance == null)
                        {
                            var ins = new EventCenter();

                            _instance = ins;
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
