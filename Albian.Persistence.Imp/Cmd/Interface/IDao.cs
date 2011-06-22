using System.Data;
using System.Data.Common;

namespace Albian.Persistence.Imp.Command.Interface
{
    /// <summary>
    /// 数据库执行方法接口
    /// </summary>
    public interface IDao
    {
        int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
                            params DbParameter[] commandParameters);

        int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText,
                            params DbParameter[] commandParameters);

        int ExecuteNonQuery(DbTransaction transaction, CommandType cmdType, string cmdText,
                            params DbParameter[] commandParameters);

        DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
                                   params DbParameter[] commandParameters);

        DbDataReader ExecuteReader(DbConnection connection, CommandType cmdType, string cmdText,
                                   params DbParameter[] commandParameters);

        DbDataReader ExecuteReader(DbTransaction transaction, CommandType cmdType, string cmdText,
                                   params DbParameter[] commandParameters);

        object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText,
                             params DbParameter[] commandParameters);

        object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText,
                             params DbParameter[] commandParameters);

        object ExecuteScalar(DbTransaction transaction, CommandType cmdType, string cmdText,
                             params DbParameter[] commandParameters);

        int Fill(DataTable dataTable, DbTransaction transaction, CommandType cmdType, string cmdText, int pageSize,
                 int currentPageIndex, params DbParameter[] commandParameters);

        int Fill(DataTable dataTable, string connectionString, CommandType cmdType, string cmdText, int pageSize,
                 int currentPageIndex, params DbParameter[] commandParameters);

        int Fill(DataTable dataTable, DbConnection connection, CommandType cmdType, string cmdText, int pageSize,
                 int currentPageIndex, params DbParameter[] commandParameters);

        void Fill(DataTable dataTable, DbTransaction transaction, CommandType cmdType, string cmdText,
                  params DbParameter[] commandParameters);

        void Fill(DataTable dataTable, string connectionString, CommandType cmdType, string cmdText,
                  params DbParameter[] commandParameters);

        void Fill(DataTable dataTable, DbConnection connection, CommandType cmdType, string cmdText,
                  params DbParameter[] commandParameters);

        void Fill(DataSet dataSet, DbTransaction transaction, CommandType cmdType, string cmdText,
                  params DbParameter[] commandParameters);

        void Fill(DataSet dataSet, string connectionString, CommandType cmdType, string cmdText,
                  params DbParameter[] commandParameters);

        void Fill(DataSet dataSet, DbConnection connection, CommandType cmdType, string cmdText,
                  params DbParameter[] commandParameters);

        int Insert(DbTransaction transaction, DataTable dataTable);
        int Insert(string connectionString, DataTable dataTable);
        int Insert(DbConnection connection, DataTable dataTable);

        int Update(DbTransaction transaction, DataTable dataTable);
        int Update(string connectionString, DataTable dataTable);
        int Update(DbConnection connection, DataTable dataTable);

        int Delete(DbTransaction transaction, DataTable dataTable);
        int Delete(string connectionString, DataTable dataTable);
        int Delete(DbConnection connection, DataTable dataTable);

        int InsertOrUpdate(DbTransaction transaction, DataTable dataTable);
        int InsertOrUpdate(string connectionString, DataTable dataTable);
        int InsertOrUpdate(DbConnection connection, DataTable dataTable);
    }
}