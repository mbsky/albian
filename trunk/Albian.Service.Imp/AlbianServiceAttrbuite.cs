using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albian.Service.Imp
{
    public class AlbianServiceAttrbuite : IAlbianServiceAttrbuite
    {
        private string _implement = string.Empty;

        private string _interface = string.Empty;

        public string Implement
        {
            get { return _implement; }
            set { _implement = value; }
        }

        public string Interface
        {
            get { return _interface; }
            set { _interface = value; }
        }
    }
}
