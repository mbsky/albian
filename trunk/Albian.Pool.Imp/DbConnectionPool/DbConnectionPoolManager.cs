using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Reflection;
using Albian.Persistence.Enum;
using Albian.Pool.DbConnectionPool;
using log4net;
using MySql.Data.MySqlClient;
using Albian.Pool.Imp.DbConnectionPool.Albian.Persistence.Imp.Cache;

namespace Albian.Pool.Imp.DbConnectionPool
{
    internal sealed class DbConnectionPoolManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 创建连接池.
        /// </summary>
        /// <remarks>
        /// isTrackerPool,isStoragePool不能同时为true或者false
        /// </remarks>
        public static void CreatePool(string storageName,DatabaseStyle dbStyle,int minSize,int maxSize)
        {
            switch(dbStyle)
            {
                case DatabaseStyle.MySql:
                    {
                        IConnectionPool<MySqlConnection> pool = 
                            new ConnectionPool<MySqlConnection>(new MySqlConnectionFactory(),minSize,maxSize);
                        ConnectionPoolCached.InsertOrUpdate(storageName,pool);
                        break;
                    }
                case DatabaseStyle.Oracle:
                    {
                        IConnectionPool<OracleConnection> pool =
                            new ConnectionPool<OracleConnection>(new OracleConnectionFactory(), minSize, maxSize);
                        ConnectionPoolCached.InsertOrUpdate(storageName, pool);
                        break;
                    }
                case DatabaseStyle.SqlServer:
                default:
                    {
                        IConnectionPool<SqlConnection> pool =
                            new ConnectionPool<SqlConnection>(new SqlServerConnectionFactory(), minSize, maxSize);
                        ConnectionPoolCached.InsertOrUpdate(storageName, pool);
                        break;
                    }
            }
        }

        /// <summary>
        /// 得到指定的连接池.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public static IConnectionPool<IDbConnection> GetPool(string stroageName)
        {
             object obj = ConnectionPoolCached.Get(stroageName);
             if (null == obj)
             {
                 Logger.Warn("the pool is null.");
                 return null; ;
             }
             return (IConnectionPool<IDbConnection>) obj;
        }
  }
}