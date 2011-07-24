#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Albian.Persistence.Context;
using Albian.Persistence.Imp.Command;
using Albian.Persistence.Imp.ConnectionPool;
using Albian.Persistence.Imp.Parser.Impl;
using Albian.Persistence.Model;
using log4net;

#endregion

namespace Albian.Persistence.Imp.TransactionCluster
{
    public class TransactionClusterScope : ITransactionClusterScope
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private TransactionClusterState _state = TransactionClusterState.NoStarted;

        #region ITransactionClusterScope Members

        /// <summary>
        /// 当前事务集群的状态
        /// </summary>
        public virtual TransactionClusterState State
        {
            get { return _state; }
        }

        /// <summary>
        /// 自动执行事务，并且提交或者回滚
        /// </summary>
        /// <param name="task"></param>
        public virtual bool Execute(ITask task)
        {
            bool isSuccess = false;
            IDictionary<string, IStorageContext> contexts = task.Context;
            _state = TransactionClusterState.NoStarted;
            try
            {
                _state = TransactionClusterState.Opening;

                PreLoadExecute(contexts);

                _state = TransactionClusterState.OpenedAndRuning;

                ExecuteHandler(contexts);

                _state = TransactionClusterState.Runned;

                Executed(contexts);

                _state = TransactionClusterState.Commited;
                isSuccess = true;
            }
            catch (Exception exc)
            {
                isSuccess = false;
                if (null != Logger)
                {
                    Logger.ErrorFormat("Execute the cluster transaction scope is error.info:{0}", exc.Message);
                }
                _state = TransactionClusterState.Rollbacking;
                ExceptionHandler(contexts);
                _state = TransactionClusterState.Rollbacked;
            }
            finally
            {
                UnLoadExecute(contexts);
            }
            return isSuccess;
        }

        #endregion

        protected virtual void UnLoadExecute(IDictionary<string, IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {
                IStorageContext storageContext = context.Value;
                try
                {
                    if (null != context.Value.Command)
                    {
                        foreach (IDbCommand cmd in context.Value.Command)
                        {
                            cmd.Parameters.Clear();
                            cmd.Dispose();
                        }
                    }
                    if (null != storageContext.Transaction)
                    {
                        storageContext.Transaction.Dispose();
                    }
                    storageContext.Transaction = null;
                    storageContext.FakeCommand = null;

                    if (null != storageContext.Connection &&
                        ConnectionState.Closed != storageContext.Connection.State)
                    {
                        storageContext.Connection.Close();

                        if (storageContext.Storage.Pooling)
                        {
                            DbConnectionPoolManager.RetutnConnection(storageContext.StorageName,
                                                                     storageContext.Connection);
                        }
                        else
                        {
                            storageContext.Connection.Dispose();
                        }
                        storageContext.Connection = null;
                    }
                }
                catch
                {
                    if (null != Logger)
                        Logger.Warn("Clear the database resources is error.but must close the all connections");
                }
            }
        }

        protected virtual void Executed(IDictionary<string, IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {
                context.Value.Transaction.Commit();
                _state = TransactionClusterState.Commiting;
            }
        }

        protected virtual void ExceptionHandler(IDictionary<string, IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {
                context.Value.Transaction.Rollback();
            }
        }

        protected virtual void ExecuteHandler(IDictionary<string, IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {
                foreach (IDbCommand cmd in context.Value.Command)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected virtual void PreLoadExecute(IDictionary<string, IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {
                IStorageContext storageContext = context.Value;
                string sConnection = StorageParser.BuildConnectionString(storageContext.Storage);
                storageContext.Connection =
                    storageContext.Storage.Pooling
                        ?
                            DbConnectionPoolManager.GetConnection(storageContext.StorageName, sConnection)
                        :
                            DatabaseFactory.GetDbConnection(storageContext.Storage.DatabaseStyle, sConnection);

                if (ConnectionState.Open != storageContext.Connection.State)
                    storageContext.Connection.Open();
                storageContext.Transaction = storageContext.Connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                foreach (IFakeCommandAttribute fc in storageContext.FakeCommand)
                {
                    IDbCommand cmd = storageContext.Connection.CreateCommand();
                    cmd.CommandText = fc.CommandText;
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = storageContext.Transaction;
                    foreach (DbParameter para in fc.Paras)
                    {
                        cmd.Parameters.Add(para);
                    }
                    storageContext.Command.Add(cmd);
                }
            }
        }
    }
}