using System;
using System.Collections;
using Albian.ObjectModel;

namespace Albian.Persistence.Imp
{
    [Serializable]
    public delegate string SeparatedHandle<T>(T target)
        where T : IAlbianObject;

    public class SeparatedManager
    {
        private static readonly Hashtable _separatedEvents = Hashtable.Synchronized(new Hashtable());

        public static void Register<T>(string routingName, string typeFullName, SeparatedHandle<T> separatedEvent)
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
            if (null == separatedEvent)
                return;
            _separatedEvents.Add(string.Format("{0}{1}", typeFullName, routingName), separatedEvent);
        }

        public static SeparatedHandle<T> GetEvent<T>(string routingName, string typeFullName)
            where T : IAlbianObject
        {
            object target = _separatedEvents[string.Format("{0}{1}", typeFullName, routingName)];
            if (null != target)
                return (SeparatedHandle<T>) target;
            return null;
        }
    }
}