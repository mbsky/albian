using System;
using System.Data;
using System.Data.Common;
using Albian.Persistence.Imp.Command.Interface;

namespace Albian.Persistence.Imp.Command
{
    public abstract class DatabaseHelper : IDao
    {
        protected virtual int FillDataTable(DataTable dataTable, IDataReader dataReader, int? pageSize,
                                            int? currentPageIndex)
        {
            if (dataTable == null)
            {
                throw new ArgumentNullException("dataTable");
            }
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }
            if (!pageSize.HasValue && currentPageIndex.HasValue || pageSize.HasValue && !currentPageIndex.HasValue)
            {
                throw new Exception("pageSize and currentPageIndex is all null or is all not null");
            }

            bool isNeedCreateSchema = 0 == dataTable.Columns.Count;

            if (isNeedCreateSchema)
            {
                DataColumn dataColumn = null;
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    dataColumn = new DataColumn(dataReader.GetName(i), dataReader.GetFieldType(i));
                    dataTable.Columns.Add(dataColumn);
                }
            }

            int startIndex = (pageSize ?? 0)*((currentPageIndex ?? 1) - 1) + 1;
            int endIndex = (pageSize ?? 0)*(currentPageIndex ?? 0);
            int currentIndex = 0;

            dataTable.BeginLoadData();
            if (isNeedCreateSchema)
            {
                var values = new object[dataTable.Columns.Count];
                while (dataReader.Read())
                {
                    currentIndex += 1;
                    if (!pageSize.HasValue && !currentPageIndex.HasValue)
                    {
                        dataReader.GetValues(values);
                        dataTable.LoadDataRow(values, false);
                        continue;
                    }

                    if (currentIndex >= startIndex && currentIndex <= endIndex)
                    {
                        dataReader.GetValues(values);
                        dataTable.LoadDataRow(values, false);
                    }
                }
            }
            else
            {
                DataRow dataRow = null;
                while (dataReader.Read())
                {
                    currentIndex += 1;
                    if (!pageSize.HasValue && !currentPageIndex.HasValue)
                    {
                        dataRow = dataTable.NewRow();
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            if (dataTable.Columns.Contains(dataReader.GetName(i)))
                            {
                                dataRow[dataReader.GetName(i)] = dataReader.GetValue(i);
                            }
                        }
                        dataTable.Rows.Add(dataRow);
                        continue;
                    }

                    if (currentIndex >= startIndex && currentIndex <= endIndex)
                    {
                        dataRow = dataTable.NewRow();
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            if (dataTable.Columns.Contains(dataReader.GetName(i)))
                            {
                                dataRow[dataReader.GetName(i)] = dataReader.GetValue(i);
                            }
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }

            dataTable.EndLoadData();
            return currentIndex;
        }

        #region Implementation of IDao

        public abstract int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
                                            params DbParameter[] commandParameters);

        public abstract int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText,
                                            params DbParameter[] commandParameters);

        public abstract int ExecuteNonQuery(DbTransaction transaction, CommandType cmdType, string cmdText,
                                            params DbParameter[] commandParameters);

        public abstract DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
                                                   params DbParameter[] commandParameters);

        public abstract DbDataReader ExecuteReader(DbConnection connection, CommandType cmdType, string cmdText,
                                                   params DbParameter[] commandParameters);

        public abstract DbDataReader ExecuteReader(DbTransaction transaction, CommandType cmdType, string cmdText,
                                                   params DbParameter[] commandParameters);

        public abstract object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText,
                                             params DbParameter[] commandParameters);

        public abstract object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText,
                                             params DbParameter[] commandParameters);

        public abstract object ExecuteScalar(DbTransaction transaction, CommandType cmdType, string cmdText,
                                             params DbParameter[] commandParameters);

        public abstract int Fill(DataTable dataTable, DbTransaction transaction, CommandType cmdType, string cmdText,
                                 int pageSize, int currentPageIndex, params DbParameter[] commandParameters);

        public abstract int Fill(DataTable dataTable, string connectionString, CommandType cmdType, string cmdText,
                                 int pageSize, int currentPageIndex, params DbParameter[] commandParameters);

        public abstract int Fill(DataTable dataTable, DbConnection connection, CommandType cmdType, string cmdText,
                                 int pageSize, int currentPageIndex, params DbParameter[] commandParameters);

        public abstract void Fill(DataTable dataTable, DbTransaction transaction, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters);

        public abstract void Fill(DataTable dataTable, string connectionString, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters);

        public abstract void Fill(DataTable dataTable, DbConnection connection, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters);

        public abstract void Fill(DataSet dataSet, DbTransaction transaction, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters);

        public abstract void Fill(DataSet dataSet, string connectionString, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters);

        public abstract void Fill(DataSet dataSet, DbConnection connection, CommandType cmdType, string cmdText,
                                  params DbParameter[] commandParameters);

        public abstract int Insert(DbTransaction transaction, DataTable dataTable);
        public abstract int Insert(string connectionString, DataTable dataTable);
        public abstract int Insert(DbConnection connection, DataTable dataTable);
        public abstract int Update(DbTransaction transaction, DataTable dataTable);
        public abstract int Update(string connectionString, DataTable dataTable);
        public abstract int Update(DbConnection connection, DataTable dataTable);
        public abstract int Delete(DbTransaction transaction, DataTable dataTable);
        public abstract int Delete(string connectionString, DataTable dataTable);
        public abstract int Delete(DbConnection connection, DataTable dataTable);
        public abstract int InsertOrUpdate(DbTransaction transaction, DataTable dataTable);
        public abstract int InsertOrUpdate(string connectionString, DataTable dataTable);
        public abstract int InsertOrUpdate(DbConnection connection, DataTable dataTable);

        #endregion
    }
}