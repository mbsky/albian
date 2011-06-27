using System.Collections.Generic;
using System.Reflection;
using Albian.ObjectModel;
using Albian.Persistence.Context;
using Albian.Persistence.Model;

namespace Albian.Persistence.Imp.Command
{
   
    public interface IStorageContextBuilder
    {
        IDictionary<string, IStorageContext> GenerateCreateStorage<T>(T target)
            where T : IAlbianObject;
        //IDictionary<string, IStorageContext> GenerateCreateStorage<T>(IList<T> target)
        //    where T : IAlbianObject;
    }
}