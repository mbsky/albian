#region

using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Albian.Kernel.Cached;
using Albian.Kernel.Service.Impl;
using Albian.Persistence.Imp.Reflection;
using Albian.Persistence.Model;
using log4net;

#endregion

namespace Albian.Persistence.Imp.Cache
{
    public class ResultCache
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public static void CachingObject<T>(string routingName, IFilterCondition[] where, T target)
            where T : class, IAlbianObject
        {
            ICacheAttribute cache = GetCacheAttribute<T>();
            if (null == cache || !cache.Enable) return;
            string cachedKey = null != where && 1 == where.Length && "id" == where[0].PropertyName.ToLower()
                                   ? Utils.GetCacheKey<T>(where[0].Value.ToString()) //find by pk id
                                   : Utils.GetCacheKey<T>(routingName, 0, where, null);
            IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("ExpiredCachedService");

            cachedService.InsertOrUpdate(cachedKey, target, cache.LifeTime);
        }


        public static void CachingObject<T>(IDbCommand cmd, T target)
            where T : class, IAlbianObject
        {
            ICacheAttribute cache = GetCacheAttribute<T>();
            if (null == cache || !cache.Enable) return;

            string cachedKey = Utils.GetCacheKey<T>(cmd);
            IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("ExpiredCachedService");
            cachedService.InsertOrUpdate(cachedKey, target, cache.LifeTime);
        }

        public static void CachingObjects<T>(string routingName, int top, IFilterCondition[] where,
                                             IOrderByCondition[] orderby, IList<T> target)
            where T : class, IAlbianObject
        {
            ICacheAttribute cache = GetCacheAttribute<T>();
            if (null == cache || !cache.Enable) return;
            string cachedKey = Utils.GetCacheKey<T>(routingName, top, where, orderby);
            IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("ExpiredCachedService");
            cachedService.InsertOrUpdate(cachedKey, target, cache.LifeTime);
        }


        public static void CachingObjects<T>(IDbCommand cmd, IList<T> target)
            where T : class, IAlbianObject
        {
            ICacheAttribute cache = GetCacheAttribute<T>();
            if (null == cache || !cache.Enable) return;
            string cachedKey = Utils.GetCacheKey<T>(cmd);
            IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("ExpiredCachedService");
            cachedService.InsertOrUpdate(cachedKey, target, cache.LifeTime);
        }


        public static T GetCachingObject<T>(string routingName, IFilterCondition[] where)
            where T : class, IAlbianObject
        {
            ICacheAttribute cache = GetCacheAttribute<T>();
            if (null == cache || !cache.Enable) return null;
            string cachedKey = null != where && 1 == where.Length && "id" == where[0].PropertyName.ToLower()
                                   ? Utils.GetCacheKey<T>(where[0].Value.ToString()) //find by pk id
                                   : Utils.GetCacheKey<T>(routingName, 0, where, null);

            IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("ExpiredCachedService");

            object oTarget = cachedService.Get(cachedKey);
            if (null == oTarget) return null;
            return (T) oTarget;
        }


        public static T GetCachingObject<T>(IDbCommand cmd)
            where T : class, IAlbianObject
        {
            ICacheAttribute cache = GetCacheAttribute<T>();
            if (null == cache || !cache.Enable) return null;

            string cachedKey = Utils.GetCacheKey<T>(cmd);
            IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("ExpiredCachedService");

            object oTarget = cachedService.Get(cachedKey);
            if (null == oTarget) return null;
            return (T) oTarget;
        }

        public static IList<T> GetCachingObjects<T>(string routingName, int top, IFilterCondition[] where,
                                                    IOrderByCondition[] orderby)
            where T : class, IAlbianObject
        {
            ICacheAttribute cache = GetCacheAttribute<T>();
            if (null == cache || !cache.Enable) return null;
            string cachedKey = Utils.GetCacheKey<T>(routingName, top, where, orderby);
            IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("ExpiredCachedService");

            object oTarget = cachedService.Get(cachedKey);
            if (null == oTarget) return null;
            return (IList<T>) oTarget;
        }


        public static IList<T> GetCachingObjects<T>(IDbCommand cmd)
            where T : class, IAlbianObject
        {
            ICacheAttribute cache = GetCacheAttribute<T>();
            if (null == cache || !cache.Enable) return null;
            string cachedKey = Utils.GetCacheKey<T>(cmd);
            IExpiredCached cachedService = ServiceRouter.GetService<IExpiredCached>("ExpiredCachedService");
            object oTarget = cachedService.Get(cachedKey);
            if (null == oTarget) return null;
            return (IList<T>) oTarget;
        }

        private static ICacheAttribute GetCacheAttribute<T>(T target)
            where T : IAlbianObject
        {
            string fullName = AssemblyManager.GetFullTypeName(target);
            object oAttribute = ObjectCache.Get(fullName);
            if (null == oAttribute)
            {
                if (null != Logger)
                    Logger.ErrorFormat("The {0} object attribute is null in the object cache.", fullName);
                return null;
            }
            IObjectAttribute objectAttribute = (IObjectAttribute) oAttribute;
            return objectAttribute.Cache;
        }

        private static ICacheAttribute GetCacheAttribute<T>()
            where T : class, IAlbianObject
        {
            string fullName = AssemblyManager.GetFullTypeName(typeof (T));
            object oAttribute = ObjectCache.Get(fullName);
            if (null == oAttribute)
            {
                if (null != Logger)
                    Logger.ErrorFormat("The {0} object attribute is null in the object cache.", fullName);
                return null;
            }
            IObjectAttribute objectAttribute = (IObjectAttribute) oAttribute;
            return objectAttribute.Cache;
        }
    }
}