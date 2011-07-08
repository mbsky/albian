using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Albian.Persistence.Context;
using Albian.Persistence.Imp.Cache;
using Albian.Persistence.Imp.Reflection;
using Albian.Persistence.Model;
using log4net;

namespace Albian.Persistence.Imp.Command
{
    public class StorageContextBuilder : IStorageContextBuilder
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IDictionary<string, IStorageContext> GenerateCreateStorage<T>(T target)
            where T : IAlbianObject
        {
            if (null == target)
            {
                throw new ArgumentNullException("target");
            }

            Type type = target.GetType();
            string fullName = AssemblyManager.GetFullTypeName(target);
            object oProperties = PropertyCache.Get(fullName);
            PropertyInfo[] properties;
            if (null == oProperties)
            {
                if (null != Logger)
                    Logger.Warn("Get the object property info from cache is null.Reflection now and add to cache.");
                properties = type.GetProperties();
                PropertyCache.InsertOrUpdate(fullName, properties);
            }
            properties = (PropertyInfo[])oProperties;
            object oAttribute = ObjectCache.Get(fullName);
            if (null == oAttribute)
            {
                if (null != Logger)
                    Logger.ErrorFormat("The {0} object attribute is null in the object cache.", fullName);
                throw new Exception("The object attribute is null");
            }
            IObjectAttribute objectAttribute = (IObjectAttribute)oAttribute;
            IFakeCommandBuilder builder = new FakeCommandBuilder();
            IDictionary<string, IStorageContext> storageContexts = builder.GenerateFakeCommandByRoutings(target, properties, objectAttribute, builder.BuildCreateFakeCommandByRouting);

            if (null == storageContexts || 0 == storageContexts.Count)//no the storage context
            {
                if (null != Logger)
                    Logger.Warn("There is no storage contexts of the object.");
                return null;
            }
            return storageContexts;
        }

        //public IDictionary<string, IStorageContext> GenerateCreateStorage<T>(IList<T> target)
        //     where T : IAlbianObject
        //{
        //    return null;
        //}
    }
}