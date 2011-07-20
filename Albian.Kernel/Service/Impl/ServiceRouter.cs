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
                if(null != Logger)
                    Logger.WarnFormat("The {0} service is null.", typeFullName);
                return null;
            }
            return (T)service;

        }

        public static T GetService<T>(string serviceId)
           where T : class, IAlbianService
        {
            object service = ServiceCached.Get(serviceId);
            if (null == service)
            {
                if (null != Logger)
                    Logger.WarnFormat("The {0} service is null.", serviceId);
                return null;
            }
            return (T)service;

        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isNew">if set to <c>true</c> [reload].</param>
        /// <returns></returns>
        /// <remarks>
        /// 注意，该方法有未处理异常
        /// </remarks>
        public static T GetService<T>(bool isNew)
            where T : class, IAlbianService
        {
            string typeFullName = AssemblyManager.GetFullTypeName(typeof(T));
            return GetService<T>(typeFullName, isNew);
        }

        public static T GetService<T>(string serviceId,bool isNew)
            where T : class, IAlbianService
        {
            if (!isNew)
                return GetService<T>(serviceId);

            IDictionary<string, IAlbianServiceAttrbuite> serviceInfos = (IDictionary<string, IAlbianServiceAttrbuite>)ServiceInfoCached.Get(FreeServiceConfigParser.ServiceKey);
            if (serviceInfos.ContainsKey(serviceId))
            {
                if (null != Logger)
                    Logger.WarnFormat("There is not {0} serice info.", serviceId);
                return null;
            }
            IAlbianServiceAttrbuite serviceInfo = serviceInfos[serviceId];
            Type impl = Type.GetType(serviceInfo.Implement);
            IAlbianService service = (IAlbianService)Activator.CreateInstance(impl);
            service.BeforeLoading();
            service.Loading();
            service.AfterLoading();
            return (T)service;

        }

        /// <summary>
        /// Objects the generator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <returns></returns>
        public static I ObjectGenerator<T, I>()
            where T : class,I
            where I : class
        {
            try
            {
                I target = Activator.CreateInstance<T>();
                return target;
            }
            catch (Exception exc)
            {
                if (null != Logger)
                    Logger.WarnFormat("Create instance is error.Info{0},StackTrace:{1}", exc.Message, exc.StackTrace);
                return null;
            }
        }
    }
}