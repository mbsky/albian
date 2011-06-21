using System;
using System.Collections;

namespace Albian.Cached.Imp
{
    /// <summary>
    /// 不过期缓存
    /// </summary>
    /// <remarks>注意：缓存项永不过期，需要手动更新</remarks>
    public class StandingCached : ICached
    {
        private readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());

        #region ICached Members

        public bool Exist(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            return cache.ContainsKey(key);
        }

        public object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            return cache[key];
        }

        public void Insert(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            cache.Add(key, value);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            cache.Remove(key);
        }

        public void Remove()
        {
            cache.Clear();
        }


        public void Update(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            cache[key] = value;
        }

        public void InsertOrUpdate(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }
            if (cache.ContainsKey(key))
            {
                cache[key] = value;
            }
            else
            {
                cache.Add(key, value);
            }
        }

        #endregion
    }
}