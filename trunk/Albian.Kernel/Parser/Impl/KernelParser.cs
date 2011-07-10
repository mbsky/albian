using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Albian.Kernel.Parser.Impl
{
    public class KernelParser : AbstractKernelParser
    {
        protected override void ParserAppId(XmlNode node)
        {
            object oAppId;
            if (XmlFileParser.TryGetNodeValue(node, out oAppId))
            {
                Settings.AppId = oAppId.ToString().Trim();
            }
            return;
        }

        public override void Loading()
        {
            ConfigParser("config\\Kernel.config");
            base.Loading();
        }
    }
}