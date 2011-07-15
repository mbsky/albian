using System;
using Albian.Persistence;

namespace AppTest.Model
{
    public interface ILogInfo : IAlbianObject
    {
        /// <summary>
        /// ��־����
        /// </summary>
        InfoStyle Style { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        DateTime CreateTime { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        string Creator { get; set; }
        /// <summary>
        /// ��־����
        /// </summary>
        string Content { get; set; }
        /// <summary>
        /// ��־��ע
        /// </summary>
        string Remark { get; set; }
    }
}