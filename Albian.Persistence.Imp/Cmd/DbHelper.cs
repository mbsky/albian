using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Albian.Persistence.Imp.Command
{
    public static class DbHelper
    {
        /// <summary>
        /// 插入整个DataTable对象数据脚本
        /// 静态方法，线程安装
        /// </summary>
        /// <param name="dt">需要插入的DataTable对象，参数对象必须有数据库中相应的Table名字，建议和类型化数据集配合使用</param>
        /// <remarks>对byte[]类型数据，插入时为null</remarks>
        /// <returns>执行插入数据脚本</returns>
        internal static string GetInsertSql(DataTable dt)
        {
            var sbUpdate = new StringBuilder();
            var sbItem = new StringBuilder();
            var sbValue = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sbUpdate.Append(" INSERT INTO ");
                sbUpdate.Append(dt.TableName);

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.AutoIncrement)
                    {
                        continue; //自动增长列
                    }
                    if (0 == sbItem.Length)
                    {
                        sbItem.Append("( ");
                    }
                    else
                    {
                        sbItem.Append(" , ");
                    }
                    sbItem.Append(dc.ColumnName);

                    if (0 == sbValue.Length)
                    {
                        sbValue.Append("( ");
                    }
                    else
                    {
                        sbValue.Append(" , ");
                    }

                    switch (dc.DataType.Name.ToLower())
                    {
                        case "string":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    sbValue.Append(" NULL ");
                                }
                                else
                                {
                                    sbValue.Append(" '");
                                    sbValue.Append(dr[dc.ColumnName].ToString().Replace("'", "''"));
                                    sbValue.Append("' ");
                                }
                                break;
                            }
                        case "decimal":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    sbValue.Append(" NULL ");
                                }
                                else
                                {
                                    sbValue.Append(" ");
                                    sbValue.Append(dr[dc.ColumnName].ToString());
                                    sbValue.Append(" ");
                                }
                                break;
                            }
                        case "boolean":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    sbValue.Append(" NULL ");
                                }
                                else
                                {
                                    if (Convert.ToBoolean(dr[dc.ColumnName]))
                                    {
                                        sbValue.Append(" ");
                                        sbValue.Append("1");
                                        sbValue.Append(" ");
                                    }
                                    else
                                    {
                                        sbValue.Append(" ");
                                        sbValue.Append("0");
                                        sbValue.Append(" ");
                                    }
                                }
                                break;
                            }
                        case "byte[]":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    sbValue.Append(" NULL ");
                                }
                                else
                                {
                                    sbValue.Append(" NULL ");
                                }
                                break;
                            }
                        default:
                            {
                                if (dr[dc.ColumnName] == DBNull.Value)
                                {
                                    sbValue.Append(" NULL ");
                                }
                                else
                                {
                                    sbValue.Append(" '");
                                    sbValue.Append(dr[dc.ColumnName].ToString().Replace("'", "''"));
                                    sbValue.Append("' ");
                                }
                                break;
                            }
                    }
                }

                sbItem.Append(")");
                sbValue.Append(")");
                sbUpdate.Append(sbItem);
                sbUpdate.Append(" VALUES ");
                sbUpdate.Append(sbValue);
                StringBuilderClean(sbItem);
                StringBuilderClean(sbValue);
            }

            return sbUpdate.ToString();
        }

        /// <summary>
        /// 据主键删除整个DataTable对象数据脚本
        /// 静态方法，线程安装
        /// </summary>
        /// <param name="dt">需要删除的DataTable对象，参数对象必须具有数据库中相应的Table名字，并且存在主键，建议和类型化数据集配合使用</param>
        /// <returns>执行删除数据脚本</returns>
        internal static string GetDeleteSql(DataTable dt)
        {
            var sbDelete = new StringBuilder();

            if (0 >= dt.PrimaryKey.Length)
            {
                throw new Exception("DataTable Is Not PrimaryKey");
            }
            foreach (DataRow dr in dt.Rows)
            {
                sbDelete.Append(" DELETE FROM ");
                sbDelete.Append(dt.TableName);
                sbDelete.Append(" WHERE 1=1 ");
                foreach (DataColumn dc in dt.PrimaryKey)
                {
                    sbDelete.Append(" AND ");
                    switch (dc.DataType.Name.ToLower())
                    {
                        case "string":
                            {
                                sbDelete.Append(dc.ColumnName);
                                sbDelete.Append(" = '");
                                sbDelete.Append(dr[dc.ColumnName].ToString().Replace("'", "''"));
                                sbDelete.Append("' ");
                                break;
                            }
                        case "decimal":
                            {
                                sbDelete.Append(dc.ColumnName);
                                sbDelete.Append(" = ");
                                sbDelete.Append(dr[dc.ColumnName].ToString());
                                break;
                            }
                        case "boolean":
                            {
                                if (Convert.ToBoolean(dr[dc.ColumnName]))
                                {
                                    sbDelete.Append(dc.ColumnName);
                                    sbDelete.Append(" = 1 ");
                                    break;
                                }
                                else
                                {
                                    sbDelete.Append(dc.ColumnName);
                                    sbDelete.Append(" = 0 ");
                                    break;
                                }
                            }
                        case "byte[]":
                            break;
                        default:
                            {
                                sbDelete.Append(dc.ColumnName);
                                sbDelete.Append(" = '");
                                sbDelete.Append(dr[dc.ColumnName].ToString().Replace("'", "''"));
                                sbDelete.Append("' ");
                                break;
                            }
                    }
                }
            }
            return sbDelete.ToString();
        }

        /// <summary>
        /// 插入或者更新DataTable对象数据脚本
        /// 静态方法，线程安装
        /// </summary>
        /// <param name="dt">需要插入或者更新的DataTable对象，参数对象必须具有数据库中相应的Table名字，并且存在主键，建议和类型化数据集配合使用</param>
        ///<remarks>对byte[]类型数据，插入或者更新时为null；更新时，只更新参数中不为null的字段值</remarks>
        /// <returns>执行插入或者更新数据脚本</returns>
        internal static string GetInsertOrUpdateSql(DataTable dt)
        {
            var sbUpdateOrInsert = new StringBuilder();

            DataColumn[] dcPrimaryKeyArray = dt.PrimaryKey;
            var sbPrimaryKey = new StringBuilder();
            var sbExists = new StringBuilder();
            var sbInsert = new StringBuilder();
            var sbUpdate = new StringBuilder();
            var sbUpdateTemp = new StringBuilder();
            bool isPrimaryKey = false;
            bool isUpdate = false;

            if (String.Empty.Equals(dt.TableName))
            {
                throw new Exception("域修改的表不存在表名，请赋予表名！");
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (sbExists.Length != 0)
                {
                    StringBuilderClean(sbExists);
                }
                if (sbUpdate.Length != 0)
                {
                    StringBuilderClean(sbUpdate);
                }
                if (sbInsert.Length != 0)
                {
                    StringBuilderClean(sbInsert);
                }

                sbExists.Append("IF EXISTS( SELECT 1 FROM ");
                sbExists.Append(dt.TableName);

                sbInsert.Append(" INSERT INTO ");
                sbInsert.Append(dt.TableName);
                sbInsert.Append(" VALUES (");

                sbUpdate.Append(" UPDATE ");
                sbUpdate.Append(dt.TableName);
                sbUpdate.Append(" SET ");


                if (sbPrimaryKey.Length != 0)
                {
                    StringBuilderClean(sbPrimaryKey);
                }
                sbPrimaryKey.Append(" WHERE 1=1 ");

                #region get the datatable's primar key

                foreach (DataColumn dcPrimaryKey in dcPrimaryKeyArray)
                {
                    switch (dcPrimaryKey.DataType.Name.ToLower())
                    {
                        case "string":
                            {
                                sbPrimaryKey.Append(" AND ");
                                sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                sbPrimaryKey.Append(" = '");
                                sbPrimaryKey.Append(dr[dcPrimaryKey.ColumnName].ToString().Replace("'", "''"));
                                sbPrimaryKey.Append("' ");
                                break;
                            }
                        case "decimal":
                            {
                                sbPrimaryKey.Append(" AND ");
                                sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                sbPrimaryKey.Append(" = ");
                                sbPrimaryKey.Append(dr[dcPrimaryKey.ColumnName].ToString());
                                break;
                            }
                        case "boolean":
                            {
                                sbPrimaryKey.Append(" AND ");
                                sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                if (Convert.ToBoolean(dr[dcPrimaryKey.ColumnName]))
                                {
                                    sbPrimaryKey.Append(" = 1 ");
                                }
                                else
                                {
                                    sbPrimaryKey.Append(" = 0 ");
                                }
                                break;
                            }
                        case "byte[]":
                            {
                                //sbPrimaryKey.Append(" AND ");
                                //sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                //sbPrimaryKey.Append(" = NULL ");
                                break;
                            }
                        default:
                            {
                                sbPrimaryKey.Append(" AND ");
                                sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                sbPrimaryKey.Append(" = '");
                                sbPrimaryKey.Append(dr[dcPrimaryKey.ColumnName].ToString());
                                sbPrimaryKey.Append("' ");
                                break;
                            }
                    }
                }

                #endregion

                sbExists.Append(sbPrimaryKey);
                sbExists.Append(" ) ");

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.AutoIncrement)
                    {
                        continue; //自动增长列
                    }
                    isUpdate = false;
                    isPrimaryKey = false;
                    foreach (DataColumn dcPrimaryKey in dcPrimaryKeyArray)
                    {
                        if (dcPrimaryKey == dc)
                        {
                            isPrimaryKey = true;
                            break;
                        }
                    }

                    switch (dc.DataType.Name.ToLower())
                    {
                        case "string":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    sbInsert.Append(" NULL ,");
                                }
                                else
                                {
                                    sbInsert.Append(" '");
                                    sbInsert.Append(dr[dc.ColumnName].ToString().Replace("'", "''"));
                                    sbInsert.Append("' ,");

                                    if (!isPrimaryKey)
                                    {
                                        isUpdate = true;
                                        sbUpdateTemp.Append(dc.ColumnName);
                                        sbUpdateTemp.Append(" = '");
                                        sbUpdateTemp.Append(dr[dc.ColumnName].ToString().Replace("'", "''"));
                                        sbUpdateTemp.Append("' ,");
                                    }
                                }
                                break;
                            }
                        case "decimal":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    sbInsert.Append(" NULL ,");
                                }
                                else
                                {
                                    sbInsert.Append(" '");
                                    sbInsert.Append(dr[dc.ColumnName].ToString());
                                    sbInsert.Append("' ,");

                                    if (!isPrimaryKey)
                                    {
                                        isUpdate = true;
                                        sbUpdateTemp.Append(dc.ColumnName);
                                        sbUpdateTemp.Append(" = '");
                                        sbUpdateTemp.Append(dr[dc.ColumnName].ToString());
                                        sbUpdateTemp.Append("' ,");
                                    }
                                }
                                break;
                            }
                        case "boolean":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    sbInsert.Append(" NULL ,");
                                }
                                else
                                {
                                    if (Convert.ToBoolean(dr[dc.ColumnName]))
                                    {
                                        sbInsert.Append(" '1' ,");
                                        if (!isPrimaryKey)
                                        {
                                            isUpdate = true;
                                            sbUpdateTemp.Append(dc.ColumnName);
                                            sbUpdateTemp.Append(" = '1' ,");
                                        }
                                    }
                                    else
                                    {
                                        sbInsert.Append(" '0' ,");
                                        if (!isPrimaryKey)
                                        {
                                            isUpdate = true;
                                            sbUpdateTemp.Append(dc.ColumnName);
                                            sbUpdateTemp.Append(" = '0' ,");
                                        }
                                    }
                                }
                                break;
                            }
                        case "byte[]":
                            {
                                sbInsert.Append(" NULL ,");
                                break;
                            }
                        default:
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    sbInsert.Append(" NULL ,");
                                }
                                else
                                {
                                    sbInsert.Append(" '");
                                    sbInsert.Append(dr[dc.ColumnName].ToString());
                                    sbInsert.Append("' ,");

                                    if (!isPrimaryKey)
                                    {
                                        isUpdate = true;
                                        sbUpdateTemp.Append(dc.ColumnName);
                                        sbUpdateTemp.Append(" = '");
                                        sbUpdateTemp.Append(dr[dc.ColumnName].ToString());
                                        sbUpdateTemp.Append("' ,");
                                    }
                                }
                                break;
                            }
                    }

                    if (isUpdate)
                    {
                        sbUpdate.Append(sbUpdateTemp);
                    }

                    if (sbUpdateTemp.Length != 0)
                    {
                        StringBuilderClean(sbUpdateTemp);
                    }
                }

                sbInsert.Remove(sbInsert.Length - 1, 1);
                sbInsert.Append(" ) ");

                sbUpdate.Remove(sbUpdate.Length - 1, 1);
                sbUpdate.Append(sbPrimaryKey);

                if (isUpdate)
                {
                    sbUpdateOrInsert.Append(sbExists);
                    sbUpdateOrInsert.Append(sbUpdate);
                    sbUpdateOrInsert.Append(" ELSE ");
                    sbUpdateOrInsert.Append(" ");
                    isUpdate = false;
                }

                sbUpdateOrInsert.Append(sbInsert);
            }

            return sbUpdateOrInsert.ToString();
        }

        /// <summary>
        /// 只更新DataTable对象中不为null的数据脚本
        /// 静态方法，线程安装
        /// </summary>
        /// <param name="dt">需要更新的DataTable对象，参数对象必须具有数据库中相应的Table名字，并且存在主键，建议和类型化数据集配合使用.</param>
        /// <remarks>对byte[]类型数据，插入时为null； 此脚本在执行更新只会更新不为null的字段值</remarks>
        /// <returns>执行更新数据脚本</returns>
        internal static string GetUpdateSql(DataTable dt)
        {
            var sbUpdateIncomplete = new StringBuilder();

            DataColumn[] dcPrimaryKeyArray = dt.PrimaryKey;
            var sbPrimaryKey = new StringBuilder();
            var sbUpdate = new StringBuilder();
            var sbUpdateTemp = new StringBuilder();
            bool isPrimaryKey = false;
            bool isUpdateTemp = false;
            bool isUpdate = false;

            if (String.Empty.Equals(dt.TableName))
            {
                throw new Exception("域修改的表不存在表名，请赋予表名！");
            }

            foreach (DataRow dr in dt.Rows)
            {
                isUpdate = false;

                if (sbUpdate.Length != 0)
                {
                    StringBuilderClean(sbUpdate);
                }

                sbUpdate.Append(" UPDATE ");
                sbUpdate.Append(dt.TableName);
                sbUpdate.Append(" SET ");

                if (sbPrimaryKey.Length != 0)
                {
                    StringBuilderClean(sbPrimaryKey);
                }
                sbPrimaryKey.Append(" WHERE 1=1 ");
                //get the datatable's primar key
                foreach (DataColumn dcPrimaryKey in dcPrimaryKeyArray)
                {
                    switch (dcPrimaryKey.DataType.Name.ToLower())
                    {
                        case "string":
                            {
                                sbPrimaryKey.Append(" AND ");
                                sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                sbPrimaryKey.Append(" = '");
                                sbPrimaryKey.Append(dr[dcPrimaryKey.ColumnName].ToString().Replace("'", "''"));
                                sbPrimaryKey.Append("' ");
                                break;
                            }
                        case "decimal":
                            {
                                sbPrimaryKey.Append(" AND ");
                                sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                sbPrimaryKey.Append(" = ");
                                sbPrimaryKey.Append(dr[dcPrimaryKey.ColumnName].ToString());
                                break;
                            }
                        case "boolean":
                            {
                                sbPrimaryKey.Append(" AND ");
                                sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                if (Convert.ToBoolean(dr[dcPrimaryKey.ColumnName]))
                                {
                                    sbPrimaryKey.Append(" = 1 ");
                                }
                                else
                                {
                                    sbPrimaryKey.Append(" = 0 ");
                                }
                                break;
                            }
                        case "byte[]":
                            {
                                sbPrimaryKey.Append(" AND ");
                                sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                sbPrimaryKey.Append(" = NULL ");
                                break;
                            }
                        default:
                            {
                                sbPrimaryKey.Append(" AND ");
                                sbPrimaryKey.Append(dcPrimaryKey.ColumnName);
                                sbPrimaryKey.Append(" = '");
                                sbPrimaryKey.Append(dr[dcPrimaryKey.ColumnName].ToString().Replace("'", "''"));
                                sbPrimaryKey.Append("' ");
                                break;
                            }
                    }
                }

                foreach (DataColumn dc in dt.Columns)
                {
                    //get update sql
                    if (dc.AutoIncrement)
                    {
                        continue; //自动增长列
                    }
                    isPrimaryKey = false;
                    foreach (DataColumn dcPrimaryKey in dcPrimaryKeyArray)
                    {
                        if (dcPrimaryKey == dc)
                        {
                            isPrimaryKey = true;
                            break;
                        }
                    }

                    if (isPrimaryKey)
                    {
                        continue;
                    }

                    isUpdateTemp = false;

                    switch (dc.DataType.Name.ToLower())
                    {
                        case "string":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    break;
                                }
                                else
                                {
                                    isUpdateTemp = true;
                                    isUpdate = true;
                                    sbUpdateTemp.Append(dc.ColumnName);
                                    sbUpdateTemp.Append(" = '");
                                    sbUpdateTemp.Append(dr[dc.ColumnName].ToString().Replace("'", "''"));
                                    sbUpdateTemp.Append("' ,");
                                }
                                break;
                            }
                        case "decimal":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    break;
                                }
                                else
                                {
                                    isUpdate = true;
                                    isUpdateTemp = true;
                                    sbUpdateTemp.Append(dc.ColumnName);
                                    sbUpdateTemp.Append(" = '");
                                    sbUpdateTemp.Append(dr[dc.ColumnName].ToString());
                                    sbUpdateTemp.Append("' ,");
                                }
                                break;
                            }
                        case "boolean":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    break;
                                }
                                else
                                {
                                    isUpdate = true;
                                    isUpdateTemp = true;
                                    if (Convert.ToBoolean(dr[dc.ColumnName]))
                                    {
                                        sbUpdateTemp.Append(dc.ColumnName);
                                        sbUpdateTemp.Append(" = '1' ,");
                                    }
                                    else
                                    {
                                        sbUpdateTemp.Append(dc.ColumnName);
                                        sbUpdateTemp.Append(" = '0' ,");
                                    }
                                }
                                break;
                            }
                        case "byte[]":
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        default:
                            {
                                if (DBNull.Value == dr[dc.ColumnName])
                                {
                                    break;
                                }
                                else
                                {
                                    isUpdate = true;
                                    isUpdateTemp = true;
                                    sbUpdateTemp.Append(dc.ColumnName);
                                    sbUpdateTemp.Append(" = '");
                                    sbUpdateTemp.Append(dr[dc.ColumnName].ToString().Replace("'", "''"));
                                    sbUpdateTemp.Append("' ,");
                                }
                                break;
                            }
                    }

                    if (isUpdateTemp)
                    {
                        sbUpdate.Append(sbUpdateTemp);
                    }

                    if (sbUpdateTemp.Length != 0)
                    {
                        StringBuilderClean(sbUpdateTemp);
                    }
                    isUpdateTemp = false;
                }

                sbUpdate.Remove(sbUpdate.Length - 1, 1).Append(sbPrimaryKey);

                sbUpdateIncomplete.Append(" ");
                if (isUpdate)
                {
                    sbUpdateIncomplete.Append(sbUpdate);
                }
            }

            return sbUpdateIncomplete.ToString();
        }

        /// <summary>
        /// 根据不同的数据库类型返回日期时间类型的 SQL 语句字符串。
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Globalization", "CA1305")]
        public static string DateTimeString(DateTime dateTime, string format)
        {
            return String.Format("'{0}'",
                                 dateTime.ToString(String.IsNullOrEmpty(format) ? @"yyyyMMdd HH\:mm\:ss" : format));
        }

        /// <summary>
        /// 根据参数取得数据类型，返回符合SQL语法的字符串。如：参数为字符串“value”，则返回“'value'”.
        /// 当参数为DateTime数据类型时，返回的字符串取决于 DateTimeString() 方法的实现。
        /// </summary>
        /// <param name="value">实际数据值。</param>
        /// <returns>符合SQL语法的字符串。如果无法转换则返回 null，例如二进制数组.</returns>
        public static string ParamentValue(object value)
        {
            return ParamentValue(value, null);
        }

        /// <summary>
        /// 根据参数取得数据类型，返回符合SQL语法的字符串。如：参数为字符串“value”，则返回“'value'”.
        /// 当参数为DateTime数据类型时，返回的字符串取决于 GetDateTimeSQLString() 方法的实现。
        /// </summary>
        /// <param name="value">实际数据值。</param>
        /// <param name="format"></param>
        /// <returns>符合SQL语法的字符串。如果无法转换则返回 null，例如二进制数组.</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        [SuppressMessage("Microsoft.Globalization", "CA1305")]
        public static string ParamentValue(object value, string format)
        {
            if (null == value || DBNull.Value == value)
            {
                return "NULL";
            }
            if (value is DateTime)
            {
                DateTimeString((DateTime) value, format);
            }
            if (value is Boolean)
            {
                return (bool) value ? "1" : "0";
            }
            if (value is Decimal || value is Int16 || value is Int32 || value is Int64
                || value is Double || value is Byte || value is SByte
                || value is Single || value is UInt16 || value is UInt32 || value is UInt64)
            {
                return value.ToString();
            }
            if (value is String || value is Char || value is Guid)
            {
                return String.Format("'{0}'", value);
            }

            return String.Empty;
        }

        /// <summary>
        /// 清空StringBuilder对象
        /// </summary>
        /// <param name="sb">需要清空的StringBuilder对象</param>
        internal static void StringBuilderClean(StringBuilder sb)
        {
            if (sb.Length == 0)
            {
                return;
            }
            sb.Remove(0, sb.Length);
        }
    }
}