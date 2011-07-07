using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Albian.Kernel.ThreadPool
{
    interface IThreadPool : IDisposable
    {
        void QueueUserWorkItem(WaitCallback work, object obj);
    }
}