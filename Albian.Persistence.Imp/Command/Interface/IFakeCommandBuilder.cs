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
    public interface IFakeCommandBuilder
    {
        IDictionary<string, IStorageContext> BuildCreateFakeCommandByRoutings<T>(T target, PropertyInfo[] properties, IObjectAttribute objectAttribute)
            where T : IAlbianObject;

        IFakeCommandAttribute BuildCreateFakeCommandByRouting<T>(PermissionMode permission, T target,
                                                                                 IRoutingAttribute routing,
                                                                                 IObjectAttribute objectAttribute,
                                                                                 PropertyInfo[] properties)
            where T : IAlbianObject;
    }
}
