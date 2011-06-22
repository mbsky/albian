using System;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Diagnostics.CodeAnalysis;

namespace Albian.Persistence.Imp.Command
{
    /// <summary>
    /// Oracle数据库执行类
    /// </summary>
    public sealed class Oracle : DatabaseHelper
    {
        //private static readonly Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public override int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
                                            params DbParameter[] commandParameters)
        {
            var cmd = new OracleCommand();

            using (var conn = new OracleConnection(connectionString))
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
            var cmd = new OracleCommand();
            PrepareCommand(cmd, (OracleConnection) connection, null, cmdType, cmdText,
                           commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public override int ExecuteNonQuery(DbTransaction transaction, CommandType cmdType, string cmdText,
                                            params DbParameter[] commandParameters)
        {
            var cmd = new OracleCommand();
            PrepareCommand(cmd, ((OracleTransaction) transaction).Connection, (OracleTransaction) transaction, cmdType,
                           cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public override DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
                                                   params DbParameter[] commandParameters)
        {
            var cmd = new OracleCommand();
            var conn = new OracleConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            var cmd = new OracleCommand();
            try
            {
                PrepareCommand(cmd, (OracleConnection) connection, null, cmdType, cmdText,
                               commandParameters);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            var cmd = new OracleCommand();
            try
            {
                PrepareCommand(cmd, ((OracleTransaction) transaction).Connection, (OracleTransaction) transaction,
                               cmdType, cmdText, commandParameters);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            var cmd = new OracleCommand();
            using (var connection = new OracleConnection(connectionString))
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
            var cmd = new OracleCommand();

            PrepareCommand(cmd, (OracleConnection) connection, null, cmdType, cmdText,
                           commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public override object ExecuteScalar(DbTransaction transaction, CommandType cmdType, string cmdText,
                                             params DbParameter[] commandParameters)
        {
            var cmd = new OracleCommand();
            PrepareCommand(cmd, ((OracleTransaction) transaction).Connection, (OracleTransaction) transaction, cmdType,
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
                try
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                        dataReader.Dispose();
                    }
                }
                catch (DbException exc)
                {
                    throw exc;
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
                try
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                        dataReader.Dispose();
                    }
                }
                catch
                {
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
            var cmd = new OracleCommand();
            PrepareCommand(cmd, ((OracleTransaction) transaction).Connection, (OracleTransaction) transaction, cmdType,
                           cmdText, commandParameters);
            var da = new OracleDataAdapter(cmd);
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
            using (var connection = new OracleConnection(connectionString))
            {
                var cmd = new OracleCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                var da = new OracleDataAdapter(cmd);
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
            var cmd = new OracleCommand();
            PrepareCommand(cmd, (OracleConnection) connection, null, cmdType, cmdText,
                           commandParameters);
            var da = new OracleDataAdapter(cmd);
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
            var cmd = new OracleCommand();
            PrepareCommand(cmd, ((OracleTransaction) transaction).Connection, (OracleTransaction) transaction, cmdType,
                           cmdText, commandParameters);
            var da = new OracleDataAdapter(cmd);
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
            using (var connection = new OracleConnection(connectionString))
            {
                var cmd = new OracleCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                var da = new OracleDataAdapter(cmd);
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
            var cmd = new OracleCommand();
            PrepareCommand(cmd, (OracleConnection) connection, null, cmdType, cmdText,
                           commandParameters);
            var da = new OracleDataAdapter(cmd);
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


        ///// <summary>
        ///// Add a set of parameters to the cached
        ///// </summary>
        ///// <param name="cacheKey">Key value to look up the parameters</param>
        ///// <param name="commandParameters">Actual parameters to cached</param>
        //public static void CacheParameters(string cacheKey, params OracleParameter[] commandParameters)
        //{
        //    parmCache[cacheKey] = commandParameters;
        //}

        ///// <summary>
        ///// Fetch parameters from the cache
        ///// </summary>
        ///// <param name="cacheKey">Key to look up the parameters</param>
        ///// <returns></returns>
        //public static OracleParameter[] GetCachedParameters(string cacheKey)
        //{
        //    var cachedParms = (OracleParameter[]) parmCache[cacheKey];

        //    if (cachedParms == null)
        //    {
        //        return null;
        //    }

        //    // If the parameters are in the cache
        //    var clonedParms = new OracleParameter[cachedParms.Length];

        //    // return a copy of the parameters
        //    for (int i = 0, j = cachedParms.Length; i < j; i++)
        //    {
        //        clonedParms[i] = (OracleParameter) ((ICloneable) cachedParms[i]).Clone();
        //    }

        //    return clonedParms;
        //}

        /// <summary>
        /// Internal function to prepare a command for execution by the database
        /// </summary>
        /// <param name="cmd">Existing command object</param>
        /// <param name="conn">DatabaseHelper connection object</param>
        /// <param name="trans">Optional transaction object</param>
        /// <param name="cmdType">Command type, e.g. stored procedure</param>
        /// <param name="cmdText">Command test</param>
        /// <param name="commandParameters">Parameters for the command</param>
        public static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans,
                                          CommandType cmdType, string cmdText, DbParameter[] commandParameters)
        {
            //Open the connection if required
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            //Set up the command
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //Bind it to the transaction if it exists
            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            // Bind the parameters passed in
            if (commandParameters != null)
            {
                foreach (DbParameter parm in commandParameters)
                {
                    cmd.Parameters.Add((OracleParameter) parm);
                }
            }
        }

        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static string OracleBit(bool value)
        {
            return value ? "Y" : "N";
        }

        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static bool OracleBool(string value)
        {
            if (value.Equals("Y"))
            {
                return true;
            }
            return false;
        }
    }
}