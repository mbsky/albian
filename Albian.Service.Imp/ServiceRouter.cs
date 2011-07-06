using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Albian.Service.Imp
{
    public class ServiceRouter : AbstractAlbianService
    {
        private static Hashtable _sevices = Hashtable.Synchronized(new Hashtable());

        public static T GetService<T>()
        {
            return default(T);
        }

        public static T GetService<T>(bool reload)
        {
            return default(T);
        }

        public override void Loading()
        {
            
        }
    }
}
