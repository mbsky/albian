//===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//===============================================================================

using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using MySql.Data.MySqlClient;

namespace Albian.Persistence.Imp.Command
{
    /// <summary>
    ///MySql数据库执行类
    /// </summary>
    public sealed class MySql : DatabaseHelper
    {
        //private static readonly Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public override int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
                                            params DbParameter[] commandParameters)
        {
            var cmd = new MySqlCommand();

            using (var conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public override int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText,
                                            params DbParameter[] commandParameters)
        {
            var cmd = new MySqlCommand();
            PrepareCommand(cmd, (MySqlConnection) connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public override int ExecuteNonQuery(DbTransaction transaction, CommandType cmdType, string cmdText,
                                            params DbParameter[] commandParameters)
        {
            var cmd = new MySqlCommand();
            PrepareCommand(cmd, ((MySqlTransaction) transaction).Connection, (MySqlTransaction) transaction, cmdType,
                           cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public override DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
                                                   params DbParameter[] commandParameters)
        {
            var cmd = new MySqlCommand();
            var conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public override DbDataReader ExecuteReader(DbConnection connection, CommandType cmdType, string cmdText,
                                                   params DbParameter[] commandParameters)
        {
            var cmd = new MySqlCommand();
            try
            {
                PrepareCommand(cmd, (MySqlConnection) connection, null, cmdType, cmdText,
                               commandParameters);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public override DbDataReader ExecuteReader(DbTransaction transaction, CommandType cmdType, string cmdText,
                                                   params DbParameter[] commandParameters)
        {
            var cmd = new MySqlCommand();
            try
            {
                PrepareCommand(cmd, ((MySqlTransaction) transaction).Connection, (MySqlTransaction) transaction, cmdType,
                               cmdText, commandParameters);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                transaction.Connection.Close();
                throw;
            }
        }

        public override object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText,
                                             params DbParameter[] commandParameters)
        {
            var cmd = new MySqlCommand();
            using (var connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public override object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText,
                                             params DbParameter[] commandParameters)
        {
            var cmd = new MySqlCommand();

            PrepareCommand(cmd, (MySqlConnection) connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public override object ExecuteScalar(DbTransaction transaction, CommandType cmdType, string cmdText,
                                             params DbParameter[] commandParameters)
        {
            var cmd = new MySqlCommand();
            PrepareCommand(cmd, ((MySqlTransaction) transaction).Connection, (MySqlTransaction) transaction, cmdType,
                           cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override int Fill(DataTable dataTable, DbTransaction transaction, CommandType cmdType, string cmdText,
                                 int pageSize, int currentPageIndex, params DbParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            IDataReader dataReader = null;
            try
            {
                dataReader = ExecuteReader(transaction, cmdType, cmdText, commandParameters);
                return FillDataTable(dataTable, dataReader, pageSize, currentPageIndex);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override int Fill(DataTable dataTable, string connectionString, CommandType cmdType, string cmdText,
                                 int pageSize, int currentPageIndex, params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            IDataReader dataReader = null;
            try
            {
                dataReader = ExecuteReader(connectionString, cmdType, cmdText, commandParameters);
                return FillDataTable(dataTable, dataReader, pageSize, currentPageIndex);
            }

            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override int Fill(DataTable dataTable, DbConnection connection, CommandType cmdType, string cmdText,
                                 int pageSize, int currentPageIndex, params DbParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            IDataReader dataReader = null;
            try
            {
                dataReader = ExecuteReader(connection, cmdType, cmdText, commandParameters);
                return FillDataTable(dataTable, dataReader, pageSize, currentPageIndex);
            }

            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override void Fill(DataTable dataTable, DbTransaction transaction, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            var cmd = new MySqlCommand();
            PrepareCommand(cmd, ((MySqlTransaction) transaction).Connection, (MySqlTransaction) transaction, cmdType,
                           cmdText, commandParameters);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);
            cmd.Parameters.Clear();
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override void Fill(DataTable dataTable, string connectionString, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (var connection = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                var da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                cmd.Parameters.Clear();
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override void Fill(DataTable dataTable, DbConnection connection, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            var cmd = new MySqlCommand();
            PrepareCommand(cmd, (MySqlConnection) connection, null, cmdType, cmdText, commandParameters);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);
            cmd.Parameters.Clear();
        }

        public override void Fill(DataSet dataSet, DbTransaction transaction, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            var cmd = new MySqlCommand();
            PrepareCommand(cmd, ((MySqlTransaction) transaction).Connection, (MySqlTransaction) transaction, cmdType,
                           cmdText, commandParameters);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dataSet);
            cmd.Parameters.Clear();
        }

        public override void Fill(DataSet dataSet, string connectionString, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (var connection = new MySqlConnection(connectionString))
            {
                var cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                var da = new MySqlDataAdapter(cmd);
                da.Fill(dataSet);
                cmd.Parameters.Clear();
            }
        }

        public override void Fill(DataSet dataSet, DbConnection connection, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            var cmd = new MySqlCommand();
            PrepareCommand(cmd, (MySqlConnection) connection, null, cmdType, cmdText, commandParameters);
            var da = new MySqlDataAdapter(cmd);
            da.Fill(dataSet);
            cmd.Parameters.Clear();
        }


        public override int Insert(DbTransaction transaction, DataTable dataTable)
        {
            return ExecuteNonQuery(transaction, CommandType.Text, DbHelper.GetInsertSql(dataTable));
        }

        public override int Insert(string connectionString, DataTable dataTable)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, DbHelper.GetInsertSql(dataTable));
        }

        public override int Insert(DbConnection connection, DataTable dataTable)
        {
            return ExecuteNonQuery(connection, CommandType.Text, DbHelper.GetInsertSql(dataTable));
        }


        public override int Update(DbTransaction transaction, DataTable dataTable)
        {
            return ExecuteNonQuery(transaction, CommandType.Text, DbHelper.GetUpdateSql(dataTable));
        }

        public override int Update(string connectionString, DataTable dataTable)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, DbHelper.GetUpdateSql(dataTable));
        }

        public override int Update(DbConnection connection, DataTable dataTable)
        {
            return ExecuteNonQuery(connection, CommandType.Text, DbHelper.GetUpdateSql(dataTable));
        }


        public override int Delete(DbTransaction transaction, DataTable dataTable)
        {
            return ExecuteNonQuery(transaction, CommandType.Text, DbHelper.GetDeleteSql(dataTable));
        }

        public override int Delete(string connectionString, DataTable dataTable)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, DbHelper.GetDeleteSql(dataTable));
        }

        public override int Delete(DbConnection connection, DataTable dataTable)
        {
            return ExecuteNonQuery(connection, CommandType.Text, DbHelper.GetDeleteSql(dataTable));
        }


        public override int InsertOrUpdate(DbTransaction transaction, DataTable dataTable)
        {
            return ExecuteNonQuery(transaction, CommandType.Text, DbHelper.GetInsertOrUpdateSql(dataTable));
        }

        public override int InsertOrUpdate(string connectionString, DataTable dataTable)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, DbHelper.GetInsertOrUpdateSql(dataTable));
        }

        public override int InsertOrUpdate(DbConnection connection, DataTable dataTable)
        {
            return ExecuteNonQuery(connection, CommandType.Text, DbHelper.GetInsertOrUpdateSql(dataTable));
        }


        //public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        //{
        //    parmCache[cacheKey] = commandParameters;
        //}

        //public static MySqlParameter[] GetCachedParameters(string cacheKey)
        //{
        //    var cachedParms = (MySqlParameter[])parmCache[cacheKey];

        //    if (cachedParms == null)
        //    {
        //        return null;
        //    }

        //    var clonedParms = new MySqlParameter[cachedParms.Length];

        //    for (int i = 0, j = cachedParms.Length; i < j; i++)
        //    {
        //        clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();
        //    }

        //    return clonedParms;
        //}

        public static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans,
                                          CommandType cmdType,
                                          string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                {
                    cmd.Parameters.Add((MySqlParameter) parm);
                }
            }
        }
    }
}