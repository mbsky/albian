using System.Collections;
using System.Threading;

namespace Albian.Kernel.Cached.Impl
{
    /// <summary>
    /// 单线程内数据缓存
    /// </summary>
    /// <remarks>吃类中的方法线程安全</remarks>
    public class PreThreadCached
    {
        private static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());

        private static string GetKey(string key)
        {
            return string.Format("{0}_{1}", Thread.CurrentThread.ManagedThreadId, key);
        }


        public static bool Exist(string key)
        {
            return cache.ContainsKey(GetKey(key));
        }

        public static object GetData(string key)
        {
            return cache[GetKey(key)];
        }

        public static void SetData(string key, object value)
        {
            cache.Add(GetKey(key), value);
        }

        public static void Delete(string key)
        {
            cache.Remove(GetKey(key));
        }

        public static void Clear()
        {
            cache.Clear();
        }
    }
}