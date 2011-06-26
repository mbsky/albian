using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Albian.ObjectModel;
using Albian.Persistence.Context;
using Albian.Persistence.Enum;
using Albian.Persistence.Model;

namespace Albian.Persistence.Imp.Command
{
    public delegate IDictionary<string, IStorageContext> BuildFakeCommandByRoutingsHandler<T>(T target, 
                                                                                    PropertyInfo[] properties, 
                                                                                    IObjectAttribute objectAttribute,
                                                                                    BuildFakeCommandByRoutingHandler<T> routingHandler)
           where T : IAlbianObject;

    public delegate IFakeCommandAttribute BuildFakeCommandByRoutingHandler<T>(PermissionMode permission, T target,
                                                                        IRoutingAttribute routing,
                                                                        IObjectAttribute objectAttribute,
                                                                        PropertyInfo[] properties)
            where T : IAlbianObject;


    public interface IFakeCommandBuilder
    {

        IDictionary<string, IStorageContext> GenerateStorageContexts<T>(T target, BuildFakeCommandByRoutingsHandler<T> buildFakeCommandByRoutingsHandler, BuildFakeCommandByRoutingHandler<T> buildFakeCommandByRoutingHandler)
            where T : IAlbianObject;

        IDictionary<string, IStorageContext> GenerateFakeCommandByRoutings<T>(T target, PropertyInfo[] properties, IObjectAttribute objectAttribute,BuildFakeCommandByRoutingHandler<T> buildFakeCommandByRoutingHandler)
            where T : IAlbianObject;

        IFakeCommandAttribute BuildCreateFakeCommandByRouting<T>(PermissionMode permission, T target,
                                                                                 IRoutingAttribute routing,
                                                                                 IObjectAttribute objectAttribute,
                                                                                 PropertyInfo[] properties)
            where T : IAlbianObject;
    }
}
