using System;
using System.Web;
using Albian.Kernel.Service.Impl;
using System.Web.Caching;

namespace Albian.Kernel.Cached.Impl
{
    public class ExpiredCached :FreeAlbianService, IExpiredCached
    {
        #region IExpiredCached 成员

        public void Insert(string key, object value, int seconds)
        {
            HttpRuntime.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, seconds), CacheItemPriority.NotRemovable, null);
        }

        public void Update(string key, object value, int seconds)
        {
            HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, seconds), CacheItemPriority.NotRemovable, null);
            
        }

        public void InsertOrUpdate(string key, object value, int seconds)
        {
            HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, seconds), CacheItemPriority.NotRemovable, null);
        }

        #endregion

        #region IExpiredCached Members

        /// <summary>
        /// allways return true
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exist(string key)
        {
            return true;
        }

        public object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        public void Insert(string key, object value)
        {
            HttpRuntime.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 300), CacheItemPriority.NotRemovable, null);
        }

        public void Update(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 300), CacheItemPriority.NotRemovable, null);
        }

        public void InsertOrUpdate(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 300), CacheItemPriority.NotRemovable, null);
            
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}