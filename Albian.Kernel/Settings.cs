using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albian.Kernel
{
    public class Settings
    {
        private static string _appid = string.Empty;
        public static string AppId
        {
            get { return _appid; }
            set { _appid = value; }
        }


    }
}
