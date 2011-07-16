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
        
        public static T GetService<T>()
            where T:class, IAlbianService
        {
            string typeFullName = AssemblyManager.GetFullTypeName(typeof(T));
            object service = ServiceCached.Get(typeFullName);
            if (null == service)
            {
                throw new ServiceException(string.Format("The {0} service is null.", typeFullName));
            }
            return (T)service;

        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <param name="reload">if set to <c>true</c> [reload].</param>
        /// <returns></returns>
        /// <remarks>
        /// 注意，该方法有未处理异常
        /// </remarks>
        public static T GetService<T>(bool reload)
            where T :class, IAlbianService
        {
            string typeFullName = AssemblyManager.GetFullTypeName(typeof(T));
            if (!reload)
                return GetService<T>();

            IDictionary<string, IAlbianServiceAttrbuite> serviceInfos = (IDictionary<string, IAlbianServiceAttrbuite>)ServiceInfoCached.Get(FreeServiceConfigParser.ServiceKey);
            if (serviceInfos.ContainsKey(typeFullName))
            {
                throw new ServiceException(string.Format("There is not {0} serice info.", typeFullName));
            }
            IAlbianServiceAttrbuite serviceInfo = serviceInfos[typeFullName];
            Type impl = Type.GetType(serviceInfo.Implement);
            IAlbianService service = (IAlbianService) Activator.CreateInstance(impl);
            service.BeforeLoading();
            service.Loading();
            service.AfterLoading();
            return (T) service;

        }

        /// <summary>
        /// Objects the generator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <returns></returns>
        public static I ObjectGenerator<T, I>()
            where T : class,I
        {
            I target = Activator.CreateInstance<T>();
            return target;
        }
    }
}