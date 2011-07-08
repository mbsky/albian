using System;

namespace Albian.ObjectModel.Imp
{
    [Serializable]
    public abstract class AbstractAlbianObject : IAlbianObject
    {
        #region Implementation of IAlbianObject

        private string _id = string.Empty;
        private bool _isNew = false;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }

        #endregion
    }
}