using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Kernel.Service;

namespace Albian.Kernel
{
    public interface IXmlParser : IAlbianService
    {
        void Init(string path);
    }
}
