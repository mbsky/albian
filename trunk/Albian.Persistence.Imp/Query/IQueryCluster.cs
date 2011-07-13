#region

using System.Collections.Generic;
using System.Data;
using Albian.Persistence.Context;

#endregion

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