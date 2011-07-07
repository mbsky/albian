using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections;
using Albian.Kernel;
using log4net;

namespace Albian.Service.Imp
{
    public class ServiceRouter
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static Hashtable _services = Hashtable.Synchronized(new Hashtable());

        public static void Start()
        {
            IXmlParser parser = new ServiceConfigParser();
            parser.Init("config/Service.config");
            IDictionary<string, IAlbianServiceAttrbuite> serviceInfos = (IDictionary<string, IAlbianServiceAttrbuite>)ServiceCached.Get(AbstractServiceConfigParser.ServiceKey);
            bool isSuccess = true;
            IDictionary<string, IAlbianServiceAttrbuite> failServicesInfos = new Dictionary<string, IAlbianServiceAttrbuite>();
            int failCountBeforeTimes = 0;
            while (true)
            {
                isSuccess = true;
                if (0 != failServicesInfos.Count)
                {
                    if (failCountBeforeTimes == failServicesInfos.Count)
                    {
                        if (null != Logger)
                        {
                            Logger.ErrorFormat("Refer to each other when service loading!");
                            foreach (KeyValuePair<string, IAlbianServiceAttrbuite> kv in failServicesInfos)
                            {
                                Logger.ErrorFormat("Refer to each other!Id:{0},impl:{1}", kv.Value.Id, kv.Value.Implement);
                            }
                            Logger.Error("Please examine the service id above the line!");
                        }
                        throw new ServiceException("Refer to each other!");
                    }

                    failCountBeforeTimes = failServicesInfos.Count;
                    serviceInfos.Clear();
                    foreach(KeyValuePair<string,IAlbianServiceAttrbuite> kv in failServicesInfos)
                    {
                        serviceInfos.Add(kv.Key,kv.Value);
                    }
                    failServicesInfos.Clear();
                }

                foreach (KeyValuePair<string, IAlbianServiceAttrbuite> kv in serviceInfos)
                {
                    try
                    {
                        Type impl = Type.GetType(kv.Value.Implement);
                        IAlbianService service = (IAlbianService) Activator.CreateInstance(impl);
                        service.State = ServiceState.Loading;
                        service.Loading();
                        service.State = ServiceState.Running;
                        _services.Add(kv.Key, service);
                    }
                    catch
                    {
                        isSuccess = false;
                        failServicesInfos.Add(kv.Key, kv.Value);
                    }
                }
                if (isSuccess)
                {
                    break;
                }
            }
        }

        public static T GetService<T>(string id)
            where T: IAlbianService
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id");
            }
            object service = _services[id];
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

            IDictionary<string, IAlbianServiceAttrbuite> serviceInfos = (IDictionary<string, IAlbianServiceAttrbuite>)ServiceCached.Get(AbstractServiceConfigParser.ServiceKey);
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
