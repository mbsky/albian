using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Albian.Persistence.Enum;
using Albian.Persistence.Model;

namespace Albian.Persistence.Context
{
    public interface IStorageContext
    {
        string StorageName { get; set; }
        IList<IFakeCommandAttribute> FakeCommand { get; set; }
        IDbConnection Connection { get; set; }
        IDbTransaction Transaction { get; set; }
        IList<IDbCommand> Command { get; set; }
        IStorageAttribute Storage { get; set; }
    }
}