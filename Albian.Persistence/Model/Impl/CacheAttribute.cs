namespace Albian.Persistence.Model.Impl
{
    public class CacheAttribute : ICacheAttribute
    {
        #region Implementation of ICacheAttribute

        private bool _enable = true;

        private int _lifeTime = 300;

        /// <summary>
        /// �Ƿ����û���
        /// </summary>
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int LifeTime
        {
            get { return _lifeTime; }
            set { _lifeTime = value; }
        }

        #endregion
    }
}