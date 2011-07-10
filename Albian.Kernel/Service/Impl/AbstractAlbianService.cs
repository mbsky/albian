﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albian.Kernel.Service.Impl
{
    public abstract class AbstractAlbianService : IAlbianService
    {
        protected AbstractAlbianService()
        {

        }

        public ServiceState State { get; set; }

        public virtual void Loading()
        {
            State = ServiceState.Running;
            return;
        }

        public virtual void Unloading()
        {
            State = ServiceState.Unload;
            return;
        }
    }
}