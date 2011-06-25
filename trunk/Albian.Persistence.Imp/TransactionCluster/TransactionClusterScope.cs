using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Albian.Persistence.Context;
using log4net;

namespace Albian.Persistence.Imp.TransactionCluster
{
    public class TransactionClusterScope :ITransactionClusterScope
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private TransactionClusterState _state = TransactionClusterState.NoStarted; 
        public TransactionClusterState State 
        {
            get { return _state; }
        }

        public void Execute(ITask task)
        {
            IDictionary<string, IStorageContext> contexts = task.Context;
            _state = TransactionClusterState.NoStarted;
            try
            {
                _state = TransactionClusterState.Opening;

                PreLoadExecute(contexts);

                _state = TransactionClusterState.OpenedAndRuning;

                ExecuteHandler(contexts);

                _state = TransactionClusterState.RunnedAndCommiting;

                Executed(contexts);

                _state = TransactionClusterState.Commited;
            }
            catch (Exception exc)
            {
                if (null != Logger)
                {
                    Logger.ErrorFormat("Execute the cluster transaction scope is error.info:{0}",exc.Message);
                    return;
                }
                _state = TransactionClusterState.Rollbacking;
                ExceptionHandler(contexts);
                _state = TransactionClusterState.Rollbacked;
            }
            finally
            {
                UnLoadExecute(contexts);
            }
        }

        private void UnLoadExecute(IDictionary<string, IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {

                IStorageContext storageContext = context.Value;
                if(ConnectionState.Closed != storageContext.Connection.State)
                    storageContext.Connection.Close();
                storageContext.Connection.Dispose();
                try
                {
                    foreach (IDbCommand cmd in context.Value.Command)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                    }

                    storageContext.Transaction.Dispose();
                    storageContext.Transaction = null;
                    storageContext.FakeCommand = null;
                }
                catch
                {
                    if (null != Logger)
                        Logger.Warn("Clear the database resources is error.but must close the all connections");
                }

            }
        }

        private void Executed(IDictionary<string,IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {
                context.Value.Transaction.Commit();
            }
        }

        private void ExceptionHandler(IDictionary<string, IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {
                context.Value.Transaction.Rollback();
            }
        }

        private void ExecuteHandler(IDictionary<string, IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {
                IStorageContext storageContext = context.Value;
                foreach (IDbCommand cmd in context.Value.Command)
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void PreLoadExecute(IDictionary<string, IStorageContext> storageContexts)
        {
            foreach (KeyValuePair<string, IStorageContext> context in storageContexts)
            {
                IStorageContext storageContext = context.Value;
                if (ConnectionState.Open != storageContext.Connection.State)
                    storageContext.Connection.Open();
                storageContext.Transaction = storageContext.Connection.BeginTransaction(IsolationLevel.ReadUncommitted);;
                foreach (KeyValuePair<string, DbParameter[]> fake in storageContext.FakeCommand)
                {
                    IDbCommand cmd = storageContext.Connection.CreateCommand();
                    cmd.CommandText = fake.Key;
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = storageContext.Transaction;
                    foreach (DbParameter para in fake.Value)
                    {
                        cmd.Parameters.Add(para);
                    }
                    storageContext.Command.Add(cmd);
                }
            }
        }
    }
}