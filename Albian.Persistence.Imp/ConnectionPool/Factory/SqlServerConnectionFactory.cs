#region

using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Albian.Persistence.ConnectionPool;
using log4net;

#endregion

namespace Albian.Persistence.Imp.ConnectionPool.Factory
{
    /// <summary>
    /// ���ӳش�������
    /// </summary>
    public class SqlServerConnectionFactory : IPoolableConnectionFactory<SqlConnection>
        //where T : IDbConnection, new()
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region IPoolableConnectionFactory<SqlConnection> Members

        /// <summary>
        /// ��������
        /// </summary>
        public SqlConnection CreateObject()
        {
            SqlConnection obj = Activator.CreateInstance<SqlConnection>();
            return obj;
        }

        /// <summary>
        /// ���ٶ���.
        /// </summary>
        public void DestroyObject(SqlConnection obj)
        {
            if (ConnectionState.Closed != obj.State)
            {
                obj.Close();
            }
            if (obj is IDisposable)
            {
                ((IDisposable) obj).Dispose();
            }
        }

        /// <summary>
        /// ��鲢ȷ������İ�ȫ
        /// </summary>
        public bool ValidateObject(SqlConnection obj)
        {
            return null != obj;
        }

        /// <summary>
        /// ���������д��ö���. 
        /// </summary>
        public void ActivateObject(SqlConnection obj, string connectionString)
        {
            try
            {
                if (ConnectionState.Open == obj.State) return;
                obj.ConnectionString = connectionString;
                if (ConnectionState.Open != obj.State)
                    obj.Open();
            }
            catch (Exception exc)
            {
                if (null != Logger) Logger.WarnFormat("���ӳؼ������ʱ�����쳣,�쳣��ϢΪ:{0}", exc.Message);
            }
        }

        /// <summary>
        /// ж���ڴ�������ʹ�õĶ���.
        /// </summary>
        public void PassivateObject(SqlConnection obj)
        {
            if (ConnectionState.Closed != obj.State) obj.Close();
        }

        #endregion
    }
}