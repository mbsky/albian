using System;
using System.Collections.Generic;
using Albian.Persistence.Model;

namespace Albian.Persistence.Imp.Model
{
    [Serializable]
    public class ObjectAttribute : IObjectAttribute
    {
        private string _implement = string.Empty;
        private string _interface = string.Empty;
        private IDictionary<string, IMemberAttribute> _memberAttributes; //new Dictionary<string, IMemberAttribute>();
        private IDictionary<string, IMemberAttribute> _primaryKeys; // new Dictionary<string, IMemberAttribute>();
        private IRoutingAttribute _rountingTemplate;
        private IDictionary<string, IRoutingAttribute> _routingAttributes; // new List<IRoutingAttribute>();

        #region IObjectAttribute Members

        /// <summary>
        /// 存储上下文
        /// </summary>
        public virtual IDictionary<string, IRoutingAttribute> RoutingAttributes
        {
            get { return _routingAttributes; }
            set { _routingAttributes = value; }
        }

        /// <summary>
        /// 属性的成员
        /// </summary>
        public virtual IDictionary<string, IMemberAttribute> MemberAttributes
        {
            get { return _memberAttributes; }
            set { _memberAttributes = value; }
        }

        public virtual IDictionary<string, IMemberAttribute> PrimaryKeys
        {
            get { return _primaryKeys; }
            set { _primaryKeys = value; }
        }

        public virtual string Implement
        {
            get { return _implement; }
            set { _implement = value; }
        }

        public virtual string Interface
        {
            get { return _interface; }
            set { _interface = value; }
        }

        public virtual IRoutingAttribute RountingTemplate
        {
            get { return _rountingTemplate; }
            set { _rountingTemplate = value; }
        }

        #endregion
    }
}