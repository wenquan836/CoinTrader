using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using CoinTrader.Common.Interface;

namespace CoinTrader.Common.Classes
{

    public class Pool<T> where T : new()
    {
        private readonly ConcurrentBag<T> _objects = new ConcurrentBag<T>();
        public virtual T Get() => _objects.TryTake(out T item) ? item : new T();

        public void Put(T item)
        {
            if (item != null)
                _objects.Add(item);
        }

        public void Put(IEnumerable<T> list)
        {
            if (list != null)
            {
                foreach (T o in list)
                {
                    Put(o);
                }
            }
        }

        private readonly static Dictionary<string, object> pools = new Dictionary<string, object>();
        public static Pool<T> GetPool()
        {
            Type type = typeof(T);
            string name = type.FullName;

            lock (pools)
            {
                if (pools.TryGetValue(name, out var pool))
                    return pool as Pool<T>;

                Pool<T> p = new Pool<T>();

                pools[name] = p;
                return p;
            }
        }
    }

    /*

    /// <summary>
    /// 通用对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pool<T> where T :new() // IPoolObject, new()
    {
      
        readonly Queue<T> pool = new Queue<T>();

        public virtual T Get()
        {
            lock (this)
            {
                if (this.pool.Count > 0)
                {
                    return this.pool.Dequeue();
                }
            }

            return new T();
        }

        public void Put(T obj)
        {
            lock (this)
            {
                this.pool.Enqueue(obj);
            }
        }

        public void Put(IEnumerable<T> list)
        {
            lock (this)
            {
                foreach (T o in list)
                {
                    this.pool.Enqueue(o);
                }
            }
        }

            private readonly static Dictionary<string, object> pools = new Dictionary<string, object>();
        public static Pool<T> GetPool()
        {
            Type type = typeof(T);

            string name = type.FullName;

            lock (pools)
            {
                if (pools.ContainsKey(name))
                {
                    return pools[name] as Pool<T>;
                }

                Pool<T> p = new Pool<T>();

                pools[name] = p;
                return p;
            }
        }

    */




    public class VOPool<T> : Pool<T> where T : IPoolObject, new()
    {
        private static readonly Dictionary<string, object> pools = new Dictionary<string, object>();

        public override T Get()
        {
            T val = base.Get();
            val.PoolReserve();

            return val;
        }

        public new static VOPool<T> GetPool()
        {
            Type type = typeof(T);

            string name = type.FullName;

            lock (pools)
            {
                if (pools.TryGetValue(name, out var pool))
                {
                    return pool as VOPool<T>;
                }

                VOPool<T> p = new VOPool<T>();

                pools[name] = p;
                return p;
            }
        }
    }
}