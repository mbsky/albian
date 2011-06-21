using System;
using System.Collections.Generic;
using System.Xml;
using Albian.Persistence.Model;
using Albian.XmlParser;

namespace Albian.Persistence.Imp.Parser
{
    public abstract class AbstractStorageParser : IStorageParser
    {
        #region IStorageParser Members

        public void Init(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            try
            {
                XmlDocument doc = XmlFileParser.LoadXml(filePath);
                XmlNodeList nodes = XmlFileParser.Analyze(doc, "Storages");
                if (1 != nodes.Count) //root node
                {
                    throw new Exception("Analyze the Storages node is error in the Storage.config");
                }

                ParserStorages(nodes[0]);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #endregion

        protected abstract IDictionary<string, IStorageAttribute> ParserStorages(XmlNode node);
        protected abstract IStorageAttribute ParserStorage(XmlNode nodes);
    }
}