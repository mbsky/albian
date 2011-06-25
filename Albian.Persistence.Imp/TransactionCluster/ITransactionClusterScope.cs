﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Persistence.Context;

namespace Albian.Persistence.Imp.DTC
{
    interface ITransactionClusterScope
    {
        TransactionClusterState State { get;}
        void Execute(ITask task);
    }
}