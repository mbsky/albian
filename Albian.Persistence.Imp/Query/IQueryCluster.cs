using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Albian.Persistence.Context;

namespace Albian.Persistence.Imp.Query
{
    public interface IQueryCluster
    {
        T QueryObject<T>(ITask task)
          where T : IAlbianObject;

        IList<T> QueryObjects<T>(ITask task)
               where T : IAlbianObject;

        T QueryObject<T>(IDbCommand cmd)
          where T : IAlbianObject;

        IList<T> QueryObjects<T>(IDbCommand cmd)
               where T : IAlbianObject;
    }
}