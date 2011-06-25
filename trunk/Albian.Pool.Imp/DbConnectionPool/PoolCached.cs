using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Albian.Cached;
using Albian.Cached.Imp;

namespace Albian.Pool.Imp.DbConnectionPool
{
    namespace Albian.Persistence.Imp.Cache
    {
        public class PoolCache
        {
            private static readonly ICached _cache = new StandingCached();

            public static bool Exist(string key)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                return _cache.Exist(key);
            }

            public static object Get(string key)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                return _cache.Get(key);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void Insert(string key, object value)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }
                _cache.Insert(key, value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void Remove(string key)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                _cache.Remove(key);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void Remove()
            {
                _cache.Remove();
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void Update(string key, object value)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }
                _cache.Update(key, value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static void InsertOrUpdate(string key, object value)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("key");
                }
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }
                _cache.InsertOrUpdate(key, value);
            }
        }
    }
}
