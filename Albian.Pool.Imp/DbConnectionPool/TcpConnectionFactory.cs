using System;
using System.Data;
using System.Reflection;
using Albian.Pool.DbConnectionPool;
using log4net;

namespace FastDFS.Client.Component
{
    /// <summary>
    /// ���ӳش�������
    /// </summary>
    public class TcpConnectionFactory<T> : IPoolableObjectFactory<T>
        where T : IDbConnection, new()
    {

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// ��������
        /// </summary>
        public T CreateObject()
        {
            T obj = Activator.CreateInstance<T>();
            //obj.ConnectionString = connectionString;
            //obj.IsFromPool = true;
            //obj.BatchId = FastDFSService.BatchId;
            return obj;
        }

        /// <summary>
        /// ���ٶ���.
        /// </summary>
        public void DestroyObject(T obj)
        {
            if (ConnectionState.Closed != obj.State)
            {
                obj.Close();
                //obj.Close();
            }
            if (obj is IDisposable)
            {
                ((IDisposable)obj).Dispose();
            }
        }

        /// <summary>
        /// ��鲢ȷ������İ�ȫ
        /// </summary>
        public bool ValidateObject(T obj)
        {
            return null != obj;
        }

        /// <summary>
        /// ���������д��ö���. 
        /// </summary>
        public void ActivateObject(T obj,string connectionString)
        {
            try
            {
                if (ConnectionState.Open == obj.State) return;
                obj.ConnectionString = connectionString;
                if (ConnectionState.Open != obj.State)
                    obj.Open();
            }
            catch(Exception exc)
            {
                if (null != Logger) Logger.WarnFormat("���ӳؼ������ʱ�����쳣,�쳣��ϢΪ:{0}", exc.Message);
            }
        }

        /// <summary>
        /// ж���ڴ�������ʹ�õĶ���.
        /// </summary>
        public void PassivateObject(T obj)
        {
            if (ConnectionState.Closed != obj.State) obj.Close();
        }
    }
}