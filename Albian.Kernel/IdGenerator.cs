using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Albian.Kernel
{
    [AlbianKernel]
    public class IdGenerator
    {
        private static int idx = 0;
        public static string Generator()
        {
            return Generator(string.Empty);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string Generator(string mark)
        {
            if (10000 == idx) idx = 0;
            string id = string.IsNullOrEmpty(mark) 
                    ?
                        string.Format("{0}_{1}_{2:0000}",Settings.AppId,DateTime.Now.ToString("YYYYMMDDHHmmssffff"),idx)
                    : 
                        string.Format("{0}_{1}_{2}_{3:0000}", mark,Settings.AppId,DateTime.Now.ToString("YYYYMMDDHHmmssffff"),idx);
            idx++;
            return id; 
        }
    }
}
