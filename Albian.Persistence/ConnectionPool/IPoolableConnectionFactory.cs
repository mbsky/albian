using System.Data;

namespace Albian.Persistence.ConnectionPool
{
    /// <summary>
    /// ����ػ�����
    /// </summary>
    public interface IPoolableConnectionFactory<T>
        where T :IDbConnection,new()
    {
        /// <summary>
        /// ��������
        /// </summary>
        T CreateObject();

        /// <summary>
        /// ���ٶ���.
        /// </summary>
        void DestroyObject(T obj);

        /// <summary>
        /// ��鲢ȷ������İ�ȫ
        /// </summary>
        bool ValidateObject(T obj);

        /// <summary>
        /// ���������еĴ��ö���. 
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="port">The port.</param>
        void ActivateObject(T obj, string connectionStirng);

        /// <summary>
        /// ж���ڴ�������ʹ�õĶ���.
        /// </summary>
        void PassivateObject(T obj);

    }
}