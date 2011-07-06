using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Albian.Kernel.Parser;

namespace Albian.Kernel
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
    }
}
