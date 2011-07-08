using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Service;

namespace Albian.Sercice.Imp.TestProject
{
    public interface IServiceTest : IAlbianService
    {
        string Hello(string val);
    }
}
