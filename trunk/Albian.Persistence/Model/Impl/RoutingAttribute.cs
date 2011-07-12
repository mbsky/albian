using System;
using Albian.Persistence.Enum;

namespace Albian.Persistence.Model.Impl
{
    [Serializable]
    public class RoutingAttribute : IRoutingAttribute
    {
        private string _name = string.Empty;
        private string _owner = string.Empty;
        private PermissionMode _permission = PermissionMode.WR;
        private string _storageName = string.Empty;
        private string _tableName = string.Empty;

        #region IRoutingAttribute Members

        /// <summary>
        /// storagecontext名称
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// table名称
        /// </summary>
        public virtual string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        /// <summary>
        /// 所属对象
        /// </summary>
        public virtual string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public virtual string StorageName
        {
            get { return _storageName; }
            set { _storageName = value; }
        }

        public PermissionMode Permission
        {
            get { return _permission; }
            set { _permission = value; }
        }

        #endregion
    }
}