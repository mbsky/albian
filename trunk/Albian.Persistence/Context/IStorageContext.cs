using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Albian.Persistence.Enum;

namespace Albian.Persistence.Context
{
    public interface IStorageContext
    {
        string StorageName { get; set; }
        IDictionary<string, DbParameter[]> FakeCommand { get; set; }
        string ConnectionString { get; set; }
        IDbConnection Connection { get; set; }
        IDbTransaction Transaction { get; set; }
        IList<IDbCommand> Command { get; set; }
        //DatabaseStyle DBStyle { get; set; }
    }
}