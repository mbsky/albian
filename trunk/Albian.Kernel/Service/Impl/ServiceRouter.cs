using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections;
using Albian.Kernel.Cached.Impl;
using Albian.Kernel.Service.Parser;
using log4net;

namespace Albian.Kernel.Service.Impl
{
    [AlbianKernel]
    public class ServiceRouter
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public static T GetService<T>(string id)
            where T: IAlbianService
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }
            object service = ServiceCached.Get(id);
            if (null == service)
            {
                throw new ServiceException(string.Format("The {0} service is null.",id));
            }
            return (T)service;

        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <param name="isNew">if set to <c>true</c> [is new].</param>
        /// <remarks>注意，该方法有未处理异常</remarks>
        /// <returns></returns>
        public static T GetService<T>(string id,bool isNew)
            where T : IAlbianService
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }
            if (!isNew)
                return GetService<T>(id);

            IDictionary<string, IAlbianServiceAttrbuite> serviceInfos = (IDictionary<string, IAlbianServiceAttrbuite>)ServiceInfoCached.Get(AbstractServiceConfigParser.ServiceKey);
            if (serviceInfos.ContainsKey(id))
            {
                throw new ServiceException(string.Format("There is not {0} serice info.", id));
            }
            IAlbianServiceAttrbuite serviceInfo = serviceInfos[id];
            Type impl = Type.GetType(serviceInfo.Implement);
            IAlbianService service = (IAlbianService)Activator.CreateInstance(impl);
            service.State = ServiceState.Loading;
            service.Loading();
            service.State = ServiceState.Running;
            return (T) service;

        }
    }
}