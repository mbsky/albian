using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Kernel.Service;

namespace Albian.Sercice.Imp.TestProject
{
    public class ServiceTest : IServiceTest
    {
        #region Implementation of IAlbianService

        private ServiceState _state = ServiceState.Normal;

        public ServiceState State
        {
            get { return _state; }
            set { _state = value; }
        }

        public void Loading()
        {
            return;
        }

        public void Unloading()
        {
            return;
        }

        public string Hello(string val)
        {
            return val;
        }

        #endregion
    }
}
