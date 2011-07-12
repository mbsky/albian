using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Albian.Persistence.Imp.Reflection;
using Albian.Persistence.Model;
using log4net;

namespace Albian.Persistence.Imp.Cache
{
    public class ResultCache
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Save<T>(T target)
            where T : IAlbianObject
        {
        }

        public void Save<T>(IList<T> target)
                where T : IAlbianObject
        {
        }

        public static void Save<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby,T target)
             where T : IAlbianObject
        {
            
        }

        public static void Save<T>(IDbCommand cmd,T target)
             where T : IAlbianObject
        {
           
        }

        public static void Save<T>(string idValue,T target)
            where T : IAlbianObject
        {
        }

        public static void Save<T>(string routingName, int top, IFilterCondition[] where, IOrderByCondition[] orderby,IList<T> targets)
            where T : IAlbianObject
        {

        }

        public static void Save<T>(IDbCommand cmd,IList<T> targets)
             where T : IAlbianObject
        {

        }

        public static void Save<T>(string idValue,IList<T> targets)
            where T : IAlbianObject
        {
        }

        public ICacheAttribute GetCacheAttribute<T>(T target)
            where T :IAlbianObject
        {
            string fullName = AssemblyManager.GetFullTypeName(target);
            object oAttribute = ObjectCache.Get(fullName);
            if (null == oAttribute)
            {
                if (null != Logger)
                    Logger.ErrorFormat("The {0} object attribute is null in the object cache.", fullName);
                throw new Exception("The object attribute is null");
            }
            IObjectAttribute objectAttribute = (IObjectAttribute)oAttribute;
            return objectAttribute.Cache;
        }
    }
}