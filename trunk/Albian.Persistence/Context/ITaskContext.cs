using System.Collections.Generic;

namespace Albian.Persistence.Context
{
    public interface ITask
    {
        IDictionary<string, IStorageContext> Context { get; set; }
    }
}