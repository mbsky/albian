﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using log4net;

namespace Albian.Kernel.Parser
{
    public abstract class AbstractKernelParser : IKernelParser
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void ConfigParser(string path)
        {
            XmlDocument doc = XmlFileParser.LoadXml(path);
            XmlNodeList nodes = doc.SelectNodes("Kernel/Appid");
            if (null == nodes || 0 == nodes.Count)
            {
                if (null != Logger)
                    Logger.Error("No Kerenl config node in the Kernel.config file.");
                throw new AlbianKernelException("No Kerenl config node in the Kernel.config file.");
            }

            ParserAppId(nodes[0]);
        }

        protected abstract void ParserAppId(XmlNode node);
       
    }
}