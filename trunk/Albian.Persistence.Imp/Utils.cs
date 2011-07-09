using System;
using System.Collections.Generic;
using Albian.Persistence.Enum;
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

        public static string GetRelationalOperators(RelationalOperators opt)
        {
            switch (opt)
            {
                case RelationalOperators.And:
                    {
                        return "AND";
                    }
                case RelationalOperators.OR:
                    {
                        return "OR";
                    }
                default:
                    {
                        return "AND";
                    }

            }
        }

        public static string GetLogicalOperation(LogicalOperation opt)
        {
            switch (opt)
            {
                case LogicalOperation.Equal:
                    {
                        return "=";
                    }
                case LogicalOperation.Greater:
                    {
                        return ">";
                    }
                case LogicalOperation.GreaterOrEqual:
                    {
                        return ">=";
                    }
                case LogicalOperation.Is:
                    {
                        return "IS";
                    }
                case LogicalOperation.Less:
                    {
                        return "<";
                    }
                case LogicalOperation.LessOrEqual:
                    {
                        return "<=";
                    }
                case LogicalOperation.NotEqual:
                    {
                        return "<>";
                    }
                default:
                    {
                        return "=";
                    }
            }
        }
    }
}
