using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albian.Service
{
    public interface IAlbianService
    {
        ServiceState State { get; set; }

        void Loading();
        void Unloading();
    }
}
