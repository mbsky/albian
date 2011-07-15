using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Albian.Kernel.Service.Impl;

namespace Albian.Kernel
{
    [AlbianKernel]
    public class IdService : FreeAlbianService,IIdService
    {
        private static int _idx = 0;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string Generator()
        {
            return Generator(string.Empty);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string Generator(string mark)
        {
            if (10000 == _idx) _idx = 0;
            string id = string.Format("{0}{1}{2}{3:0000}",Settings.AppId, mark.PadLeft(6, '0'), DateTime.Now.ToString("yyyyMMddHHmmssffff"), _idx);
            _idx++;
            return id; 
        }
    }
}
