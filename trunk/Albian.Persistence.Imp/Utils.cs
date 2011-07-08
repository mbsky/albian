using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Persistence.Imp.Reflection;
using Albian.Persistence.Model;

namespace Albian.Persistence.Imp
{
    public class Utils
    {
        public static IList<T> Concat<T>(IList<T> target,IList<T> source)
        {
            foreach (T o in source)
            {
                target.Add(o);
            }
            return target;
        }

        public static string GetTableFullName<T>(IRoutingAttribute routing, T target) where T : IAlbianObject
        {
            HashAlbianObjectHandler<T> handler = HashAlbianObjectManager.GetHandler<T>(routing.Name,
                                                                                       AssemblyManager.GetFullTypeName(typeof (T)));
            string tableName = null == handler
                                   ? routing.TableName
                                   : String.Format("{0}{1}", routing.TableName, handler(target));
            return "dbo" == routing.Owner || String.IsNullOrEmpty(routing.Owner)
                       ? tableName
                       : String.Format("[{0}].[{1}]", routing.Owner, tableName);
        }

        public static bool IsNullableType(Type type)
        {
            return (type.IsGenericType && type.
                                              GetGenericTypeDefinition().Equals
                                              (typeof (Nullable<>)));
        }
    }
}
