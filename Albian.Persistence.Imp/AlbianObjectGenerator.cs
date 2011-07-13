#region

using System;

#endregion

namespace Albian.Persistence.Imp
{
    public class AlbianObjectGenerator
    {
        public static T CreateInstance<T>()
            where T : IAlbianObject
        {
            T instance = Activator.CreateInstance<T>();
            instance.IsNew = true;
            return instance;
        }
    }
}