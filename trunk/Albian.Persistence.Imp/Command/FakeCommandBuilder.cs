using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;
using Albian.ObjectModel;
using Albian.Persistence.Context;
using Albian.Persistence.Enum;
using Albian.Persistence.Imp.Cache;
using Albian.Persistence.Imp.Context;
using Albian.Persistence.Imp.Model;
using Albian.Persistence.Imp.Parser;
using Albian.Persistence.Imp.Reflection;
using Albian.Persistence.Model;
using log4net;

namespace Albian.Persistence.Imp.Command
{
    public class FakeCommandBuilder
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IDictionary<string,IStorageContext> GenerateSingleCreateStorage<T>(T target)
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
                PropertyCache.InsertOrUpdate(fullName,properties);
            }
            properties = (PropertyInfo[])oProperties;
            object oAttribute = ObjectCache.Get(fullName);
            if (null == oAttribute)
            {
                if (null != Logger)
                    Logger.ErrorFormat("The {0} object attribute is null in the object cache.",fullName);
                throw new Exception("The object attribute is null");
            }
            IObjectAttribute objectAttribute = (IObjectAttribute)oAttribute;
            IDictionary<string, IStorageContext> storageContexts = BuildCreateFakeCommandByRoutings(target, properties, objectAttribute);

            if (0 == storageContexts.Count)//no the storage context
            {
                if (null != Logger)
                    Logger.Warn("There is no storage contexts of the object.");
                return null;
            }
            return storageContexts;
        }

        public IDictionary<string, IStorageContext> BuildCreateFakeCommandByRoutings<T>(T target, PropertyInfo[] properties, IObjectAttribute objectAttribute) 
            where T : IAlbianObject
        {
            if (null == properties || 0 == properties.Length)
            {
                throw new ArgumentNullException("properties");
            }
            if (null == objectAttribute)
            {
                throw new ArgumentNullException("objectAttribute");
            }
            if (null == objectAttribute.RoutingAttributes || null == objectAttribute.RoutingAttributes.Values || 0 == objectAttribute.RoutingAttributes.Values.Count)
            {
                if (null != Logger)
                    Logger.Error("The routing attributes or routings is null");
                throw new Exception("The routing attributes or routing is null");
            }

            IDictionary<string, IStorageContext> storageContexts = new Dictionary<string, IStorageContext>();
            foreach (var routing in objectAttribute.RoutingAttributes.Values)
            {
                IFakeCommandAttribute fakeCommandAttrribute = BuildCreateFakeCommandByRouting(PermissionMode.W, target, routing, objectAttribute, properties);
                if (null == fakeCommandAttrribute)//the PermissionMode is not enough
                {
                    if (null != Logger)
                        Logger.WarnFormat("The permission is not enough in the {0} routing.",routing.Name);
                    continue;
                }
                if (storageContexts.ContainsKey(fakeCommandAttrribute.StorageName))
                {
                    storageContexts[fakeCommandAttrribute.StorageName].FakeCommand.Add(fakeCommandAttrribute.CommandText, fakeCommandAttrribute.Paras);
                }
                else
                {
                    IStorageContext storageContext = new StorageContext
                                                         {
                                                             FakeCommand = new Dictionary<string, DbParameter[]>
                                                                               {
                                                                                   {
                                                                                       fakeCommandAttrribute.CommandText,
                                                                                       fakeCommandAttrribute.Paras
                                                                                       }
                                                                               }
                                                         };
                    storageContexts.Add(fakeCommandAttrribute.StorageName, storageContext);
                }
            }
            return storageContexts;
        }

        public IFakeCommandAttribute BuildCreateFakeCommandByRouting<T>(PermissionMode permission, T target,
                                                                        IRoutingAttribute routing,
                                                                        IObjectAttribute objectAttribute,
                                                                        PropertyInfo[] properties)
            where T : IAlbianObject
        {
            if (null == routing)
            {
                throw new ArgumentNullException("routing");
            }
            if (null == properties || 0 == properties.Length)
            {
                throw new ArgumentNullException("properties");
            }
            if (null == objectAttribute)
            {
                throw new ArgumentNullException("objectAttribute");
            }
            if (0 == (permission & routing.Permission))
            {
                if (null != Logger)
                    Logger.WarnFormat("The routing permission {0} is no enough.",permission);
                return null;
            }

            var sbInsert = new StringBuilder();
            var sbCols = new StringBuilder();
            var sbValues = new StringBuilder();

            IList<DbParameter> paras = new List<DbParameter>();

            //create the connection string
            IStorageAttribute storageAttr = (IStorageAttribute)StorageCache.Get(routing.StorageName);
            if (null == storageAttr)
            {
                if (null != Logger)
                    Logger.WarnFormat("No {0} rounting mapping storage attribute in the sotrage cache.Use default storage.",routing.Name);
                storageAttr = (IStorageAttribute)StorageCache.Get(StorageParser.DefaultStorageName);
            }

            //create the hash table name
            string tableFullName = GetTableFullName(routing, target);

            //build the command text
            IDictionary<string, IMemberAttribute> members = objectAttribute.MemberAttributes;
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(target, null);
                if (null == value)
                {
                    continue;
                }
                IMemberAttribute member = members[property.Name];
                if (!member.IsSave)
                {
                    continue;
                }
                sbCols.AppendFormat("{0},", member.FieldName);
                string paraName = DatabaseFactory.GetParameterName(storageAttr.DatabaseStyle, member.FieldName);
                sbValues.AppendFormat("{0},", paraName);
                paras.Add(DatabaseFactory.GetDbParameter(storageAttr.DatabaseStyle, paraName, member.DBType, value, member.Length));
            }
            int colsLen = sbCols.Length;
            if (0 < colsLen)
            {
                sbCols.Remove(colsLen - 1, 1);
            }
            int valLen = sbValues.Length;
            if (0 < valLen)
            {
                sbValues.Remove(valLen - 1, 1);
            }
            sbInsert.AppendFormat("INSERT INTO {0} ({1}) VALUES({2}) ", tableFullName, sbCols, sbValues);
            IFakeCommandAttribute fakeCmd = new FakeCommandAttribute
                                                {
                                                    CommandText = sbInsert.ToString(),
                                                    Paras = ((List<DbParameter>) paras).ToArray(),
                                                    StorageName = routing.StorageName
                                                };
            return fakeCmd;
        }

        private string GetTableFullName<T>(IRoutingAttribute routing, T target) where T : IAlbianObject
        {
            SeparatedHandle<T> handler = SeparatedManager.GetEvent<T>(routing.Name,
                                                                      AssemblyManager.GetFullTypeName(typeof (T)));
            string tableName = null == handler
                                   ? routing.TableName
                                   : string.Format("{0}{1}", routing.TableName, handler(target));
            return "dbo" == routing.Owner || string.IsNullOrEmpty(routing.Owner)
                       ? tableName
                       : string.Format("[{0}].[{1}]", routing.Owner, tableName);
        }
    }
}