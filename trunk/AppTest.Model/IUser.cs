using System;
using Albian.Persistence;

namespace AppTest.Model
{
    public interface IUser : IAlbianObject
    {
        string UserName { get; set; }
        string Password { get; set; }
        /// <summary>
        /// ע������
        /// </summary>
        DateTime RegistrDate { get; set; }
        DateTime CreateTime { get; set; }
        DateTime LastMofidyTime { get; set; }
        string Creator { get; set; }
        string LastModifier { get; set; }
        /// <summary>
        /// �ǳ�
        /// </summary>
        string Nickname { get; set; }
        /// <summary>
        /// �ֻ�
        /// </summary>
        string Mobile { get; set; }
        string Mail { get; set; }
    }
}