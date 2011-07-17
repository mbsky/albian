using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Albian.Kernel.Cached.Impl;
using Albian.Kernel.Service;
using Albian.Kernel.Service.Parser;
using log4net;

namespace Albian.Kernel
{
    [AlbianKernel]
    public class AlbianBootService
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Start()
        {
            IXmlParser parser = new ServiceConfigParser();
            parser.Init("config/Service.config");
            IDictionary<string, IAlbianServiceAttrbuite> serviceInfos = (IDictionary<string, IAlbianServiceAttrbuite>) ServiceInfoCached.Get(FreeServiceConfigParser.ServiceKey);
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
                                Logger.ErrorFormat("Refer to each other!id:{0},impl:{1}", kv.Value.Id, kv.Value.Implement);
                            }
                            Logger.Error("Please examine the service id above the line!");
                        }
                        throw new ServiceException("Refer to each other!");
                    }

                    failCountBeforeTimes = failServicesInfos.Count;
                    serviceInfos.Clear();
                    foreach (KeyValuePair<string, IAlbianServiceAttrbuite> kv in failServicesInfos)
                    {
                        serviceInfos.Add(kv.Key, kv.Value);
                    }
                    failServicesInfos.Clear();
                }

                foreach (KeyValuePair<string, IAlbianServiceAttrbuite> kv in serviceInfos)
                {
                    try
                    {
                        Type impl = Type.GetType(kv.Value.Implement);
                        IAlbianService service = (IAlbianService)Activator.CreateInstance(impl);
                        service.State = ServiceState.Loading;
                        service.Loading();
                        service.State = ServiceState.Running;
                        ServiceCached.InsertOrUpdate(kv.Key, service);
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

    }
}
