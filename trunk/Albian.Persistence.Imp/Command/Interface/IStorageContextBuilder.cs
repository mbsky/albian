using System.Collections.Generic;
using Albian.ObjectModel;
using Albian.Persistence.Context;

namespace Albian.Persistence.Imp.Command
{
    public interface IStorageContextBuilder
    {
        IDictionary<string, IStorageContext> GenerateSingleCreateStorage<T>(T target)
            where T : IAlbianObject;
    }
}