using System;
using System.Collections;
using Albian.ObjectModel;

namespace Albian.Persistence.Imp
{
    [Serializable]
    public delegate string HashAlbianObjectHandler<T>(T target) where T : IAlbianObject;

    public class HashAlbianObjectManager
    {
        private static readonly Hashtable _hashAlbianObjectHandlers = Hashtable.Synchronized(new Hashtable());

        public static void RegisterHandler<T>(string routingName, string typeFullName, HashAlbianObjectHandler<T> splitHandler)
            where T : IAlbianObject
        {
            if (string.IsNullOrEmpty(routingName))
            {
                throw new ArgumentNullException("routingName");
            }
            if (string.IsNullOrEmpty(typeFullName))
            {
                throw new ArgumentNullException("typeFullName");
            }
            if (null == splitHandler)
                return;
            _hashAlbianObjectHandlers.Add(string.Format("{0}{1}", typeFullName, routingName), splitHandler);
        }

        public static HashAlbianObjectHandler<T> GetHandler<T>(string routingName, string typeFullName)
            where T : IAlbianObject
        {
            object target = _hashAlbianObjectHandlers[string.Format("{0}{1}", typeFullName, routingName)];
            if (null != target)
                return (HashAlbianObjectHandler<T>) target;
            return null;
        }
    }
}