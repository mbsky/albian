using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Albian.Kernel.Service.Impl;

namespace Albian.Kernel
{
    [AlbianKernel]
    public class IdService : AbstractAlbianService,IIdService
    {
        private static int idx = 0;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string Generator()
        {
            return Generator(string.Empty);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string Generator(string mark)
        {
            if (10000 == idx) idx = 0;
            string id = string.IsNullOrEmpty(mark) 
                    ?
                        string.Format("{0}_{1}_{2:0000}",Settings.AppId,DateTime.Now.ToString("yyyyMMddHHmmssffff"),idx)
                    : 
                        string.Format("{0}_{1}_{2}_{3:0000}", mark,Settings.AppId,DateTime.Now.ToString("yyyyMMddHHmmssffff"),idx);
            idx++;
            return id; 
        }
    }
}
