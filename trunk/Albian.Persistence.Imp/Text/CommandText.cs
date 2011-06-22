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
using Albian.Persistence.Imp.Reflection;
using Albian.Persistence.Model;

namespace Albian.Persistence.Imp.Text
{
    public class CommandText
    {
        public ITask GenerateSingleCreateTask<T>(T target)
            where T : IAlbianObject
        {
            ITask task = new Task();
            IDictionary<string, IStorageContext> storageContexts = new Dictionary<string, IStorageContext>();
            Type type = typeof (T);
            string fullName = AssemblyManager.GetFullTypeName(type);
            object obj = PropertyCache.Get(fullName);
            PropertyInfo[] properties = null == obj ? type.GetProperties() : (PropertyInfo[]) obj;

            var objectAttribute = (IObjectAttribute) ObjectCache.Get(fullName);

            foreach (var routing in objectAttribute.RoutingAttributes.Values)
            {
               IFakeCommandAttribute fakeCommandAttrribute = BuildCreateFakeCommandByRouting(PermissionMode.W, target, routing, objectAttribute, properties);
               if (null == fakeCommandAttrribute)//the PermissionMode is not enough
               {
                   continue;
               }
                if (storageContexts.ContainsKey(fakeCommandAttrribute.StorageName))
               {
                   storageContexts[fakeCommandAttrribute.StorageName].FakeCommand.Add(fakeCommandAttrribute.CommandText, fakeCommandAttrribute.Paras);
               }
               else
               {
                   IStorageContext storageContext = new StorageContext();
                   storageContext.FakeCommand = new Dictionary<string, DbParameter[]>();
                   storageContext.FakeCommand.Add(fakeCommandAttrribute.CommandText, fakeCommandAttrribute.Paras);
                   storageContexts.Add(fakeCommandAttrribute.StorageName, storageContext);
               }
            }
            if (0 == storageContexts.Count)//no the storage context
            {
                return null;
            }
            task.Context = storageContexts;
            return task;
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
            if (null == target)
            {
                throw new ArgumentNullException("target");
            }
            if (null == objectAttribute)
            {
                throw new ArgumentNullException("objectAttribute");
            }
            if (0 == (permission & routing.Permission))
            {
                return null;
            }

            var sbInsert = new StringBuilder();
            var sbCols = new StringBuilder();
            var sbValues = new StringBuilder();

            IList<DbParameter> paras = new List<DbParameter>();
            //IStorageContext context = new StorageContext();

            //create the connection string
            var storageAttr = (IStorageAttribute) StorageCache.Get(routing.StorageName);
            if (null == storageAttr)
            {
                throw new Exception("no storageattribute in the cached");
            }
            //context.ConnectionString = StorageParser.BuildConnectionString(storageAttr);
            //create the hash table name
            SeparatedHandle<T> handler = SeparatedManager.GetEvent<T>(storageAttr.Name,
                                                                      AssemblyManager.GetFullTypeName(typeof (T)));
            string tableName = null == handler
                                   ? routing.TableName
                                   : string.Format("{0}{1}", routing.TableName, handler(target));
            string tableFullName = "dbo" == routing.Owner || string.IsNullOrEmpty(routing.Owner)
                                       ? tableName
                                       : string.Format("[{0}].[{1}]", routing.Owner, tableName);

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
                string paraName = Util.GetParameterName(storageAttr.DatabaseStyle, member.FieldName);
                sbValues.AppendFormat("{0},", paraName);
                paras.Add(Util.GetDbParameter(storageAttr.DatabaseStyle, paraName, member.DBType, value, member.Length));
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
    }
}