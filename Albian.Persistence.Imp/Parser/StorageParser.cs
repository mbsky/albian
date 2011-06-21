using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Albian.Persistence.Imp.Cache;
using Albian.Persistence.Imp.Model;
using Albian.Persistence.Model;
using Albian.XmlParser;

namespace Albian.Persistence.Imp.Parser
{
    public class StorageParser : AbstractStorageParser
    {
        public static string DefaultStorageName
        {
            get { return "DefaultStorage"; }
        }
        protected override IDictionary<string, IStorageAttribute> ParserStorages(XmlNode node)
        {
            if (null == node)
            {
                throw new ArgumentNullException("node");
            }
            XmlNodeList nodes = node.SelectNodes("Storage");
            if (null == nodes || 0 == nodes.Count)
            {
                throw new Exception("the Storage node is empty in the Storage..config");
            }
            IDictionary<string, IStorageAttribute> dic = new Dictionary<string, IStorageAttribute>();
            int idx = 0;
            foreach (XmlNode n in nodes)
            {
                IStorageAttribute storageAttribute = ParserStorage(n);
                if (null != storageAttribute)
                {
                    dic.Add(storageAttribute.Name, storageAttribute);
                    if (0 == idx)
                    {
                        //insert the default storage
                        //the default is the first storage
                        StorageCache.InsertOrUpdate(DefaultStorageName,storageAttribute);
                    }
                    idx++;
                }
            }

            if (0 == dic.Count)
            {
                throw new Exception("the error in the storage.config");
            }
            return dic;
        }

        protected override IStorageAttribute ParserStorage(XmlNode node)
        {
            if (null == node)
            {
                throw new ArgumentNullException("node");
            }
            IStorageAttribute storageAttribute = GenerateStorageAttribute(node);
            if (string.IsNullOrEmpty(storageAttribute.Uid) || !storageAttribute.IntegratedSecurity)
            {
                throw new Exception("the database authentication mechanism is error.");
            }
            //string sConnectionString = BuildConnectionString(storageAttribute);
            StorageCache.InsertOrUpdate(storageAttribute.Name, storageAttribute);
            return storageAttribute;
        }

        private static IStorageAttribute GenerateStorageAttribute(XmlNode node)
        {
            IStorageAttribute storageAttribute = new StorageAttribute();
            object oName;
            object oServer;
            object oDateBase;
            object oUid;
            object oPassword;
            object oMinPoolSize;
            object oMaxPoolSize;
            object oTimeout;
            object oPooling;
            object oIntegratedSecurity;
            object oDbClass;
            if (XmlFileParser.TryGetAttributeValue(node, "Name", out oName) && null != oName)
            {
                storageAttribute.Name = oName.ToString().Trim();
            }
            else
            {
                throw new Exception("the name is empty in the storage.config");
            }
            if (XmlFileParser.TryGetAttributeValue(node, "DatabaseStyle", out oDbClass) && null != oDbClass)
            {
                storageAttribute.DatabaseStyle = ConvertToDatabaseStyle.Convert(oDbClass.ToString());
            }
            if (XmlFileParser.TryGetAttributeValue(node, "Server", out oServer) && null != oServer)
            {
                storageAttribute.Server = oServer.ToString().Trim();
            }
            else
            {
                throw new Exception("the Server is empty in the storage.config");
            }
            if (XmlFileParser.TryGetAttributeValue(node, "DateBase", out oDateBase) && null != oDateBase)
            {
                storageAttribute.Database = oDateBase.ToString().Trim();
            }
            else
            {
                throw new Exception("the Database is empty in the storage.config");
            }

            if (XmlFileParser.TryGetAttributeValue(node, "Uid", out oUid) && null != oUid)
            {
                storageAttribute.Uid = oUid.ToString().Trim();
            }
            if (XmlFileParser.TryGetAttributeValue(node, "Password", out oPassword) && null != oPassword)
            {
                storageAttribute.Password = oPassword.ToString().Trim();
            }
            if (XmlFileParser.TryGetAttributeValue(node, "MinPoolSize", out oMinPoolSize) && null != oMinPoolSize)
            {
                storageAttribute.MinPoolSize = Math.Abs(int.Parse(oMinPoolSize.ToString().Trim()));
            }
            if (XmlFileParser.TryGetAttributeValue(node, "MaxPoolSize", out oMaxPoolSize) && null != oMaxPoolSize)
            {
                storageAttribute.MaxPoolSize = Math.Abs(int.Parse(oMaxPoolSize.ToString().Trim()));
            }
            if (XmlFileParser.TryGetAttributeValue(node, "Timeout", out oTimeout) && null != oTimeout)
            {
                storageAttribute.Timeout = Math.Abs(int.Parse(oTimeout.ToString().Trim()));
            }
            if (XmlFileParser.TryGetAttributeValue(node, "Pooling", out oPooling) && null != oPooling)
            {
                storageAttribute.Pooling = bool.Parse(oPooling.ToString().Trim());
            }
            if (XmlFileParser.TryGetAttributeValue(node, "IntegratedSecurity", out oIntegratedSecurity) &&
                null != oIntegratedSecurity)
            {
                string sIntegratedSecurity = oIntegratedSecurity.ToString().Trim();
                if ("false" == sIntegratedSecurity.ToLower() || "no" == sIntegratedSecurity.ToLower())
                    storageAttribute.IntegratedSecurity = false;
                if ("true" == sIntegratedSecurity.ToLower()
                    || "yes" == sIntegratedSecurity.ToLower() || "sspi" == sIntegratedSecurity.ToLower())
                    storageAttribute.IntegratedSecurity = true;
            }
            return storageAttribute;
        }

        public static string BuildConnectionString(IStorageAttribute storageAttribute)
        {
            var sbString = new StringBuilder(150);
            sbString.AppendFormat("Server={0};Initial Catalog={1};", storageAttribute.Server, storageAttribute.Database);
            if (storageAttribute.IntegratedSecurity)
            {
                sbString.Append("Integrated Security=SSPI;");
            }
            else
            {
                sbString.AppendFormat("User ID={0};Password={1};", storageAttribute.Uid, storageAttribute.Password);
            }
            if (storageAttribute.Pooling)
            {
                sbString.AppendFormat("Max Pool Size={0};Min Pool Size{1}", storageAttribute.MaxPoolSize,
                                      storageAttribute.MinPoolSize);
            }
            return sbString.ToString();
        }
    }
}