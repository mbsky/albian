using System;
using Albian.Kernel.Service;
using Albian.Kernel.Service.Impl;

namespace Albian.Sercice.Imp.TestProject
{
    public class ServiceT : IService
    {

        #region IAlbianService ≥…‘±

        private ServiceState _state = ServiceState.Normal;
        IServiceTest service;

        public ServiceState State
        {
            get { return _state; }
            set { _state = value; }
        }

        public void Loading()
        {
           service = ServiceRouter.GetService<IServiceTest>("Test");
        }

        public void Unloading()
        {
            return;
        }

        public string Say(string val)
        {
            return val;
        }

        #endregion
    }
}