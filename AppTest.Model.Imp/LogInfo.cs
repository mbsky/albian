using System;
using Albian.Persistence.Model.Impl;

namespace AppTest.Model.Imp
{
    public class LogInfo : FreeAlbianObject,ILogInfo
    {
        #region Implementation of ILogInfo

        private InfoStyle _style;

        private DateTime _createTime;

        private string _creator;

        private string _content;

        private string _remark;

        /// <summary>
        /// ��־����
        /// </summary>
        public InfoStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        /// <summary>
        /// ��־����
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// ��־��ע
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        #endregion
    }
}