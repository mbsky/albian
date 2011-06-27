using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Persistence.Context;

namespace Albian.Persistence.Imp.TransactionCluster
{
    interface ITransactionClusterScope
    {
        TransactionClusterState State { get;}
        bool Execute(ITask task);
    }
}