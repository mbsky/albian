using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using Albian.Persistence.Enum;
using MySql.Data.MySqlClient;

namespace Albian.Persistence.Imp.Text
{
    public class Util
    {
        public static string GetParameterName(DatabaseStyle databaseStyle, string name)
        {
            switch (databaseStyle)
            {
                case DatabaseStyle.Oracle:
                    {
                        return string.Format(":{0}", name);
                    }
                case DatabaseStyle.MySql:
                case DatabaseStyle.SqlServer:
                default:
                    {
                        return string.Format("@{0}", name);
                    }
            }
        }

        public static DbParameter GetDbParameter(DatabaseStyle databaseStyle, string name, DbType dbType, object value,
                                                 int size)
        {
            switch (databaseStyle)
            {
                case DatabaseStyle.MySql:
                    {
                        DbParameter para = new MySqlParameter
                                               {
                                                   ParameterName = GetParameterName(DatabaseStyle.MySql, name),
                                                   Value = value
                                               };
                        if (DbType.AnsiString == dbType || DbType.AnsiStringFixedLength == dbType ||
                            DbType.String == dbType || DbType.StringFixedLength == dbType)
                        {
                            para.Size = size;
                        }

                        return para;
                    }
                case DatabaseStyle.Oracle:
                    {
                        DbParameter para = new OracleParameter
                                               {
                                                   ParameterName = GetParameterName(DatabaseStyle.Oracle, name),
                                                   Value = value
                                               };
                        if (DbType.AnsiString == dbType || DbType.AnsiStringFixedLength == dbType ||
                            DbType.String == dbType || DbType.StringFixedLength == dbType)
                        {
                            para.Size = size;
                        }

                        return para;
                    }
                case DatabaseStyle.SqlServer:
                default:
                    {
                        DbParameter para = new SqlParameter
                                               {
                                                   ParameterName = GetParameterName(DatabaseStyle.SqlServer, name),
                                                   Value = value
                                               };
                        if (DbType.AnsiString == dbType || DbType.AnsiStringFixedLength == dbType ||
                            DbType.String == dbType || DbType.StringFixedLength == dbType)
                        {
                            para.Size = size;
                        }

                        return para;
                    }
            }
        }
    }
}