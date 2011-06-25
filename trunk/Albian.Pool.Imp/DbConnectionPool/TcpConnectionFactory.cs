using System;
using System.Data;
using System.Reflection;
using Albian.Pool.DbConnectionPool;
using log4net;

namespace FastDFS.Client.Component
{
    /// <summary>
    /// 连接池创建工厂
    /// </summary>
    public class TcpConnectionFactory<T> : IPoolableObjectFactory<T>
        where T : IDbConnection, new()
    {

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 创建对象
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
        /// 销毁对象.
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
        /// 检查并确保对象的安全
        /// </summary>
        public bool ValidateObject(T obj)
        {
            return null != obj;
        }

        /// <summary>
        /// 激活对象池中待用对象. 
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
                if (null != Logger) Logger.WarnFormat("连接池激活对象时发生异常,异常信息为:{0}", exc.Message);
            }
        }

        /// <summary>
        /// 卸载内存中正在使用的对象.
        /// </summary>
        public void PassivateObject(T obj)
        {
            if (ConnectionState.Closed != obj.State) obj.Close();
        }
    }
}