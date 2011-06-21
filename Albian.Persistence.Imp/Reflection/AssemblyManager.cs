using System;

namespace Albian.Persistence.Imp.Reflection
{
    public class AssemblyManager
    {
        public static string GetFullTypeName(Type type)
        {
            if (null == type)
            {
                throw new ArgumentNullException("type");
            }
            string className = type.FullName;
            string assemblyName = type.Assembly.FullName;
            if (string.IsNullOrEmpty(assemblyName))
            {
                throw new Exception("the assembly name is empry or null.");
            }
            string[] strs = assemblyName.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            if (0 >= strs.Length)
            {
                throw new Exception("split the assembly is error.");
            }
            return string.Format("{0},{1}", className, strs[0]);
        }

        public static string GetFullTypeName<T>(T target)
        {
            return GetFullTypeName(target.GetType());
        }
    }
}