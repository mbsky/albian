using System;
using Albian.Persistence.Imp.Model;

namespace Albian.Persistence.Imp
{
    [Serializable]
    public abstract class AbstractAlbianObject : IAlbianObject
    {
        #region Implementation of IAlbianObject

        private string _id = string.Empty;
        private bool _isNew = false;

        [AlbianMemberAttribute(PrimaryKey=true)]
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [AlbianMemberAttribute(IsSave = false)]
        public virtual bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

        #endregion
    }
}