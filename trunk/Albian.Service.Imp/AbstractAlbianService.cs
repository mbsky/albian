using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albian.Service.Imp
{
    public abstract class AbstractAlbianService
    {
        protected AbstractAlbianService()
        {

        }

        public ServiceState State { get; set; }

        public abstract void Loading();

        public virtual void Unloading()
        {
            State = ServiceState.Unload;
            return;
        }
    }
}
