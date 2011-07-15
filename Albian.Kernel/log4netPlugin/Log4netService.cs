using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Albian.Kernel.Service.Impl;
using log4net.Config;

namespace Albian.Kernel.log4netPlugin
{
    public class Log4netService : FreeAlbianService
    {
        public override void Loading()
        {
            XmlConfigurator.Configure(new FileInfo(XmlFileParser.GetFullPath(@"config/log4net.config"))); 
            base.Loading();
        }
    }
}
