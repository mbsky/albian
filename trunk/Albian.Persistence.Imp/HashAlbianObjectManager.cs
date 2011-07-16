#region

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Albian.Kernel.Service;
using Albian.Persistence.Imp.Parser.Impl;

#endregion

namespace Albian.Persistence.Imp
{
    [Serializable]
    public delegate string HashAlbianObjectHandler<T>(T target) where T : IAlbianObject;

    public class HashAlbianObjectManager
    {
        private static readonly Hashtable _hashAlbianObjectHandlers = Hashtable.Synchronized(new Hashtable());

        public static void RegisterHandler<T>(string routingName,HashAlbianObjectHandler<T> splitHandler)
            where T : IAlbianObject
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            string typeFullName = AssemblyManager.GetFullTypeName<T>();
            if (null == splitHandler)
                return;
            string key = GetHashTableKey(routingName, typeFullName);
            if (!_hashAlbianObjectHandlers.ContainsKey(key))
            {
                _hashAlbianObjectHandlers.Add(key, splitHandler);
            }
            else
            {
                _hashAlbianObjectHandlers[key] = splitHandler;
            }
        }

        private static string GetHashTableKey(string routingName, string typeFullName)
        {
            return string.Format("{0}{1}", typeFullName, routingName);
        }
       
        public static void RegisterHandler<T>(HashAlbianObjectHandler<T> splitHandler)
           where T : IAlbianObject
        {
            string routingName = string.Empty;
            if (string.IsNullOrEmpty(routingName))
            {
                routingName = PersistenceParser.DefaultRoutingName;
            }
            RegisterHandler(routingName,splitHandler);
        }

        public static HashAlbianObjectHandler<T> GetHandler<T>()
            where T : IAlbianObject
        {
            string routingName = string.Empty;
            if (string.IsNullOrEmpty(routingName))
            {
                routingName = PersistenceParser.DefaultRoutingName;
            }
            string typeFullName = AssemblyManager.GetFullTypeName<T>();
            string key = GetHashTableKey(routingName, typeFullName);
            object target = _hashAlbianObjectHandlers[key];
            if (null != target)
                return (HashAlbianObjectHandler<T>)target;
            return null;
        }

        public static HashAlbianObjectHandler<T> GetHandler<T>(string routingName)
            where T : IAlbianObject
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            string typeFullName = AssemblyManager.GetFullTypeName<T>();
            string key = GetHashTableKey(routingName, typeFullName);
            object target = _hashAlbianObjectHandlers[key];
            if (null != target)
                return (HashAlbianObjectHandler<T>) target;
            return null;
        }
    }
}