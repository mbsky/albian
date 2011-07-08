using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
