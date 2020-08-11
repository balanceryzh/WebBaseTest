using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using WebApi1.Entity;

namespace WebApi1.MSDao
{
    public class DBHelper
    {
        private string _connectionStr { get; set; }
        public DBHelper()
        {
        }

        public DBHelper(string connStr)
        {
            _connectionStr = connStr;
        }

        public DbConnection GetConnection()
        {
            DbConnection connection = new MySql.Data.MySqlClient.MySqlConnection();
            connection.ConnectionString = _connectionStr;
            connection.Open();
            return connection;
        }

        public DbCommand GetStoredProcCommond(DbConnection connection, string storedProcedure)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }

        public DbCommand GetSqlStringCommond(DbConnection connection, string sqlQuery)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }

        #region 增加参数
        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }
        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }
        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        #endregion

        #region 执行
        public List<List<object>> ExecuteReader(string sql)
        {
            using (DbConnection connection = GetConnection())
            {
                DbCommand cmd = null;
                DbDataReader reader = null;
                try
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = sql;

                    //Console.WriteLine(sql);

                    reader = cmd.ExecuteReader();

                    List<List<object>> table = new List<List<object>>();
                    while (reader.Read())
                    {
                        //Application.DoEvents();
                        List<object> row = new List<object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            object s = null;
                            try
                            {
                                string type = reader.GetDataTypeName(i);
                                Type name = reader.GetFieldType(i);
                                if (type.StartsWith("VARCHAR"))
                                {
                                    s = reader.GetString(i);
                                }
                                else if (type.StartsWith("DECIMAL"))
                                {
                                    s = reader.GetInt64(i);
                                }
                                else if (type.StartsWith("FLOAT"))
                                {
                                    s = reader.GetFloat(i);
                                }
                                else if (type.Equals("Int64"))
                                {
                                    s = reader.GetInt64(i);
                                }
                                else if (type.Equals("INTEGER"))
                                {
                                    s = reader.GetInt32(i);
                                }
                                else if (type.Equals("Int32"))
                                {
                                    s = reader.GetInt32(i);
                                }
                                else if (type.Equals("DateTime"))
                                {
                                    s = String.Format("{g}", reader.GetDateTime(i));
                                }
                                else
                                {
                                    Console.WriteLine("DataTypeException ");
                                }

                            }
                            catch (Exception ex)
                            {
                                string error = ex.Message;
                                //Console.WriteLine("DataTypeException " + e.Message);
                            }
                            row.Add(s);
                        }
                        table.Add(row);
                    }
                    return table;
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Exception！" + this.GetType() + e.Message), e);
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Dispose();
                    }
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">存储过程</param>
        /// <param name="list">返回实体中的集合</param>
        /// <param name="total">返回实体中的总数</param>
        /// <param name="oDictionary"></param>
        public void GetCallData<T>(string callName, out List<T> list, out long total, Dictionary<string, object> oDictionary = null)
        {
            if (oDictionary == null)
                oDictionary = new Dictionary<string, object>();

            DataSet ds = this.ExecuteDataSet(callName, oDictionary);
            total = long.Parse(ds.Tables[1].Rows[0][0].ToString());
            list = new List<T>();
            object[] objs = typeof(T).GetCustomAttributes(typeof(DBAttribute), true);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Columns.Count == 1)
                {
                    list.Add((T)dr[0]);
                }
                else
                {
                    object obj = ConvertDataRow<T>(dr);
                    list.Add((T)obj);
                }
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callName"></param>
        /// <param name="oDictionary"></param>
        /// <returns>集合[0]为总条数 集合[1]为集合 </returns>
        public ArrayList GetCall<T>(string callName, Dictionary<string, object> oDictionary = null)
        {
            ArrayList array = new ArrayList();
            if (oDictionary == null)
                oDictionary = new Dictionary<string, object>();

            DataSet ds = this.ExecuteDataSet(callName, oDictionary);
            long total = long.Parse(ds.Tables[1].Rows[0][0].ToString());
            List<T> list = new List<T>();
            object[] objs = typeof(T).GetCustomAttributes(typeof(DBAttribute), true);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Columns.Count == 1)
                {
                    list.Add((T)dr[0]);
                }
                else
                {
                    object obj = ConvertDataRow<T>(dr);
                    list.Add((T)obj);
                }
            }

            array.Add(total);
            array.Add(list);
            return array;



        }
        public DataSet ExecuteDataSet(string sql, Dictionary<string, object> di)
        {
            using (DbConnection connection = GetConnection())
            {
                DbCommand cmd = GetSqlStringCommond(connection, sql);
                if (di != null)
                {
                    foreach (KeyValuePair<string, object> kvp in di)
                    {
                        DbParameter DP = cmd.CreateParameter();
                        DP.ParameterName = kvp.Key;
                        if (kvp.Value != null)
                        {
                            DP.Value = kvp.Value;
                        }
                        else
                        {
                            DP.Value = DBNull.Value;
                        }

                        cmd.Parameters.Add(DP);
                    }
                }

                return ExecuteDataSet(cmd);
            }
        }

        public DataSet ExecuteDataSet(string sql)
        {
            using (DbConnection connection = GetConnection())
            {
                DbCommand cmd = GetSqlStringCommond(connection, sql);
                return ExecuteDataSet(cmd);
            }
        }

        #region private
        private DataSet ExecuteDataSet(DbCommand cmd)
        {
            DbDataAdapter dbDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        private DataTable ExecuteDataTable(DbCommand cmd)
        {
            using (DbConnection connection = GetConnection())
            {
                DbDataAdapter dbDataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter();
                dbDataAdapter.SelectCommand = cmd;
                DataTable dataTable = new DataTable();
                try
                {
                    dbDataAdapter.Fill(dataTable);

                }
                catch (Exception)
                {

                    throw;
                }
                return dataTable;
            }
        }
        #endregion

        public DataTable ExecuteDataTable(string sql)
        {
            using (DbConnection connection = GetConnection())
            {
                DbCommand cmd = GetSqlStringCommond(connection, sql);
                return ExecuteDataTable(cmd);
            }
        }

        public DataTable ExecuteDataTable(string sql, Dictionary<string, object> di)
        {
            using (DbConnection connection = GetConnection())
            {
                DbCommand cmd = GetSqlStringCommond(connection, sql);

                if (di != null)
                {
                    foreach (KeyValuePair<string, object> kvp in di)
                    {
                        DbParameter DP = cmd.CreateParameter();
                        DP.ParameterName = kvp.Key;
                        if (kvp.Value != null)
                        {
                            DP.Value = kvp.Value;
                        }
                        else
                        {
                            DP.Value = DBNull.Value;
                        }

                        cmd.Parameters.Add(DP);
                    }
                }

                return ExecuteDataTable(cmd);
            }
        }

        //public DbDataReader ExecuteReader(DbCommand cmd)
        //{
        //    using (DbConnection connection = GetConnection())
        //    {
        //        cmd.Connection.Open();
        //        DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        return reader;
        //    }
        //}

        public string GetInIds(string[] ids)
        {
            string result = "";
            foreach (var id in ids)
            {
                result += "'" + id + "',";
            }
            return result.TrimEnd(',');
        }

        public int ExecuteNonQuery(string sql)
        {
            using (DbConnection connection = GetConnection())
            {
                DbCommand cmd = GetSqlStringCommond(connection, sql);
                return cmd.ExecuteNonQuery();
            }
        }

        public int ExecuteNonQuery(string sql, Dictionary<string, object> di)
        {
            using (DbConnection connection = GetConnection())
            {
                DbCommand cmd = GetSqlStringCommond(connection, sql);

                if (di != null)
                {
                    foreach (KeyValuePair<string, object> kvp in di)
                    {
                        DbParameter DP = cmd.CreateParameter();
                        DP.ParameterName = kvp.Key;
                        if (kvp.Value != null)
                        {
                            DP.Value = kvp.Value;
                        }
                        else
                        {
                            DP.Value = DBNull.Value;
                        }

                        cmd.Parameters.Add(DP);
                    }
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public int ExecuteNonQuery(string sql, Dictionary<string, object> di, DbConnection connection, DbTransaction transaction)
        {
            var dbCommand = connection.CreateCommand();
            dbCommand.CommandText = sql;
            dbCommand.CommandType = CommandType.Text;
            var cmd = GetSqlStringCommond(connection, sql);
            cmd.Transaction = transaction;
            if (di != null)
            {
                foreach (var kvp in di)
                {
                    var dp = cmd.CreateParameter();
                    dp.ParameterName = kvp.Key;
                    dp.Value = kvp.Value ?? DBNull.Value;
                    cmd.Parameters.Add(dp);
                }
            }
            return cmd.ExecuteNonQuery();
        }
        //public int ExecuteNonQuery(DbCommand cmd)
        //{
        //    using (DbConnection connection = GetConnection())
        //    {
        //        cmd.Connection.Open();
        //        int ret = cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //        return ret;
        //    }
        //}

        //public object ExecuteScalar(DbCommand cmd)
        //{
        //    using (DbConnection connection = GetConnection())
        //    {
        //        cmd.Connection.Open();
        //        object ret = cmd.ExecuteScalar();
        //        cmd.Connection.Close();
        //        return ret;
        //    }
        //}

        public object ExecuteScalar(string sql)
        {
            using (DbConnection connection = GetConnection())
            {
                DbCommand cmd = GetSqlStringCommond(connection, sql);
                return cmd.ExecuteScalar();
            }
        }
        public object ExecuteScalar(string sql, Dictionary<string, object> oDictionary)
        {
            using (var connection = GetConnection())
            {
                var cmd = GetSqlStringCommond(connection, sql);
                if (oDictionary != null)
                {
                    foreach (var kvp in oDictionary)
                    {
                        var dp = cmd.CreateParameter();
                        dp.ParameterName = kvp.Key;
                        dp.Value = kvp.Value ?? DBNull.Value;
                        cmd.Parameters.Add(dp);
                    }
                }
                return cmd.ExecuteScalar();
            }
        }
        #endregion

        #region 执行事务
        public void BatchExecuteNonQuery(List<string> sqllist)
        {
            using (DbConnection connection = GetConnection())
            {
                DbTransaction dbTrans = connection.BeginTransaction();
                DbCommand cmd = connection.CreateCommand();
                try
                {
                    cmd.Transaction = dbTrans;
                    foreach (string sql in sqllist)
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    try
                    {
                        dbTrans.Rollback();
                    }
                    catch
                    {
                        throw new Exception(string.Format("Exception:" + this.GetType() + e.Message), e);
                    }
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                    }
                    if (dbTrans != null)
                    {
                        dbTrans.Dispose();
                    }
                }
            }
        }
        #endregion

        //#region 释放资源
        //public void Release()
        //{
        //    if (this.connection != null && this.connection.State == ConnectionState.Open)
        //    {
        //        this.connection.Close();
        //        this.connection.Dispose();
        //    }
        //}
        //#endregion

        #region 填充对象
        /// <summary>
        /// 根据SQL和参数获取对象
        /// </summary>
        /// <param name="Type_ReadObject"></param>
        /// <param name="sql"></param>
        /// <param name="oDictionary"></param>
        /// <param name="IsLIMIT"></param>
        /// <returns></returns>
        public List<T> ReadObjectList<T>(string sql, Dictionary<string, object> oDictionary = null)
        {
            if (oDictionary == null)
                oDictionary = new Dictionary<string, object>();
            List<T> ReturnList = new List<T>();
            object[] objs = typeof(T).GetCustomAttributes(typeof(DBAttribute), true);
            string strTableName = string.Empty;

            DataTable dt = this.ExecuteDataTable(sql, oDictionary);
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Columns.Count == 1)
                {
                    ReturnList.Add((T)dr[0]);
                }
                else
                {
                    object obj = ConvertDataRow<T>(dr);
                    ReturnList.Add((T)obj);
                }
            }
            return ReturnList;
        }

        private object ConvertDataRow<T>(DataRow dr)
        {
            object obj = Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (object o in objAttrs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        if (attr.IsMapping)
                        {
                            string strPIName = pi.Name;
                            if (dr.Table.Columns.Contains(strPIName))
                            {
                                try
                                {
                                    if (dr[strPIName] != DBNull.Value)
                                    {
                                        if (pi.PropertyType == typeof(bool))
                                        {
                                            pi.SetValue(obj, Convert.ToBoolean(dr[strPIName]));
                                        }
                                        else if (pi.PropertyType == typeof(int))
                                            pi.SetValue(obj, Convert.ToInt32(dr[strPIName], null));
                                        else if (pi.PropertyType == typeof(Int64))
                                            pi.SetValue(obj, Convert.ToInt64(dr[strPIName], null));
                                        else if (pi.PropertyType == typeof(DateTime))
                                            pi.SetValue(obj, Convert.ToDateTime(dr[strPIName], null));
                                        else
                                            pi.SetValue(obj, dr[strPIName], null);

                                    }
                                    else
                                        pi.SetValue(obj, default(object), null);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(string.Format("Exception:" + ex.Message), ex);
                                }
                            }
                        }

                        break;
                    }
                }
            }
            return obj;
        }


        /// <summary>  
        /// 利用反射和泛型  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public List<T> ConvertTo<T>(DataTable dt) where T : new()
        {
            // 定义集合  
            List<T> ts = new List<T>();

            // 获得此模型的类型  
            Type type = typeof(T);
            //定义一个临时变量  
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行  
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性  
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性  
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量  
                    //检查DataTable是否包含此列（列名==对象的属性名）    
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter  
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出  
                        //取值  
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性  
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中  
                ts.Add(t);
            }

            return ts;
        }
        public T ReadObject<T>(string sql, Dictionary<string, object> oDictionary = null)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            List<T> list = ReadObjectList<T>(sql, oDictionary);
            if (list.Count > 0)
                result = list[0];
            return result;
        }

        private string[] Keywords = new string[] { "KEY", "VALUE", "ORDER", "GROUP", "INDEX", "PASSWORD", "MASTER", "DESCRIBE", "NO" };

        /// <summary>
        /// (+1重载)插入对象
        /// </summary>
        /// <param name="DB"></param>
        /// <param name="obj"></param>
        public object Insert(object obj)
        {
            Type t = obj.GetType();//获得该类的Type
            string sql = @" INSERT INTO {0}({1}) VALUES({2});select @@IDENTITY ";
            object[] objs = t.GetCustomAttributes(typeof(DBAttribute), true);
            string strTableName = string.Empty;
            foreach (object o in objs)
            {
                DBAttribute attr = o as DBAttribute;
                if (attr != null)
                {
                    strTableName = attr.TableName;//表名只有获取一次
                    break;
                }
            }
            StringBuilder sbMember = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (object o in objAttrs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        if (attr.IsMapping)
                        {
                            string strPIName = pi.Name;
                            object objPIValue = pi.GetValue(obj, null);//用pi.GetValue获得值

                            //获得属性的类型,进行判断然后进行以后的操作
                            if (objPIValue == null)
                            {
                                objPIValue = "NULL";
                            }

                            else if (objPIValue.GetType() == typeof(string))
                            {
                                //进行你想要的操作
                                objPIValue = FormateValue((string)objPIValue);
                                objPIValue = '\'' + (string)objPIValue + '\'';
                            }
                            else if (objPIValue.GetType() == typeof(DateTime))
                            {
                                objPIValue = '\'' + ((DateTime)objPIValue).ToString() + '\'';
                            }

                            if (Keywords.Contains(pi.Name.ToUpper()))
                                sbMember.Append("`" + pi.Name + "`,");
                            else
                                sbMember.Append(pi.Name + ',');
                            sbValue.Append(objPIValue);
                            sbValue.Append(',');
                        }

                        break;
                    }
                }
            }
            sbMember.Remove(sbMember.Length - 1, 1);
            sbValue.Remove(sbValue.Length - 1, 1);

            sql = string.Format(sql, strTableName, sbMember.ToString(), sbValue.ToString());

            return this.ExecuteScalar(sql);
        }

        public string LoadInsertSql(object obj)
        {
            Type t = obj.GetType();//获得该类的Type
            string sql = @"INSERT INTO {0}({1}) VALUES({2})";
            object[] objs = t.GetCustomAttributes(typeof(DBAttribute), true);
            string strTableName = string.Empty;
            foreach (object o in objs)
            {
                DBAttribute attr = o as DBAttribute;
                if (attr != null)
                {
                    strTableName = attr.TableName;//表名只有获取一次
                    break;
                }
            }
            StringBuilder sbMember = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (object o in objAttrs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        if (attr.IsMapping)
                        {
                            string strPIName = pi.Name;
                            object objPIValue = pi.GetValue(obj, null);//用pi.GetValue获得值

                            //获得属性的类型,进行判断然后进行以后的操作
                            if (objPIValue == null)
                            {
                                objPIValue = "NULL";
                            }

                            else if (objPIValue.GetType() == typeof(string))
                            {
                                //进行你想要的操作
                                objPIValue = FormateValue((string)objPIValue);
                                objPIValue = '\'' + (string)objPIValue + '\'';
                            }
                            else if (objPIValue.GetType() == typeof(DateTime))
                            {
                                objPIValue = '\'' + ((DateTime)objPIValue).ToString() + '\'';
                            }

                            if (Keywords.Contains(pi.Name.ToUpper()))
                                sbMember.Append("`" + pi.Name + "`,");
                            else
                                sbMember.Append(pi.Name + ',');
                            sbValue.Append(objPIValue);
                            sbValue.Append(',');
                        }

                        break;
                    }
                }
            }
            sbMember.Remove(sbMember.Length - 1, 1);
            sbValue.Remove(sbValue.Length - 1, 1);

            sql = string.Format(sql, strTableName, sbMember.ToString(), sbValue.ToString());

            return sql;
        }
        /// <summary>
        /// 生成插入sql语句，（参数化）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public string LoadInsertSql(object obj, ref Dictionary<string, object> parms)
        {
            var t = obj.GetType();
            var dic = new Dictionary<string, object>();
            string sql = @"INSERT INTO {0}({1}) VALUES({2})";
            var objs = t.GetCustomAttributes(typeof(DBAttribute), true);
            string strTableName = string.Empty;
            foreach (DBAttribute attr in objs.OfType<DBAttribute>())
            {
                strTableName = attr.TableName;
                break;
            }
            var sbMember = new StringBuilder();
            var sbValue = new StringBuilder();
            foreach (var pi in t.GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (var attr in objAttrs.OfType<DBAttribute>())
                {
                    if (attr.IsMapping)
                    {
                        object objPiValue = pi.GetValue(obj, null);
                        if (Keywords.Contains(pi.Name.ToUpper()))
                            sbMember.Append("`" + pi.Name + "`,");
                        else
                            sbMember.Append(pi.Name + ',');

                        sbValue.Append("@" + pi.Name);
                        sbValue.Append(',');
                        dic.Add("@" + pi.Name, objPiValue);
                    }

                    break;
                }
            }
            sbMember.Remove(sbMember.Length - 1, 1);
            sbValue.Remove(sbValue.Length - 1, 1);
            sql = string.Format(sql, strTableName, sbMember, sbValue);
            parms = dic;
            return sql;
        }
        /// <summary>
        /// 生成修改sql语句，（参数化）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public string LoadUpdateSql(object obj, ref Dictionary<string, object> parms)
        {
            Type t = null;
            t = obj.GetType();
            string sql = @"Update {0} Set {1} Where {2}";
            var dic = new Dictionary<string, object>();
            var objs = t.GetCustomAttributes(typeof(DBAttribute), true);
            string strTableName = string.Empty;
            foreach (var attr in objs.OfType<DBAttribute>())
            {
                strTableName = attr.TableName;//表名只有获取一次
                break;
            }
            var sbUpdate = new StringBuilder();
            var sbKey = new StringBuilder();
            foreach (var pi in t.GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (var attr in objAttrs.OfType<DBAttribute>())
                {
                    if (attr.IsMapping)
                    {
                        string strPiName = pi.Name;
                        object objPiValue = pi.GetValue(obj, null); //用pi.GetValue获得值
                        if (attr.IsKey)
                        {
                            if (Keywords.Contains(pi.Name.ToUpper()))
                                sbKey.Append(" `" + pi.Name + "`=@" + pi.Name + " AND");
                            else
                                sbKey.Append(" " + pi.Name + "=@" + pi.Name + " AND");
                            dic.Add("@" + pi.Name, objPiValue);
                        }
                        //获得属性的类型,进行判断然后进行以后的操作
                        else if (objPiValue is string && objPiValue.ToString() != "")
                        {
                            if (Keywords.Contains(pi.Name.ToUpper()))
                                sbUpdate.Append(" `" + pi.Name + "`=@" + pi.Name + ',');
                            else
                                sbUpdate.Append(pi.Name + "=@" + pi.Name + ',');

                            dic.Add("@" + pi.Name, objPiValue);
                        }
                        else if (objPiValue is int && int.Parse(objPiValue.ToString()) > 0)
                        {
                            if (Keywords.Contains(pi.Name.ToUpper()))
                                sbUpdate.Append(" `" + pi.Name + "`=@" + pi.Name + ',');
                            else
                                sbUpdate.Append(pi.Name + "=@" + pi.Name + ',');

                            dic.Add("@" + pi.Name, objPiValue);
                        }
                        else if (objPiValue is long && long.Parse(objPiValue.ToString()) > 0)
                        {
                            if (Keywords.Contains(pi.Name.ToUpper()))
                                sbUpdate.Append(" `" + pi.Name + "`=@" + pi.Name + ',');
                            else
                                sbUpdate.Append(pi.Name + "=@" + pi.Name + ',');

                            dic.Add("@" + pi.Name, objPiValue);
                        }
                    }
                    break;
                }
            }

            if (sbUpdate.Length > 0)
                sbUpdate.Remove(sbUpdate.Length - 1, 1);
            if (sbKey.Length > 2)
                sbKey.Remove(sbKey.Length - 3, 3);
            sql = string.Format(sql, strTableName, sbUpdate.ToString(), sbKey.ToString());
            parms = dic;
            return sql;
        }
        /// <summary>
        /// 生成查询sql语句,(注意，值拼接string类型不为空的条件)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public string LoadSelectSql(object obj, ref Dictionary<string, object> parms)
        {
            Type t = null;
            t = obj.GetType();
            string sql = @"select * from  {0}  Where 1=1 {1}";
            var dic = new Dictionary<string, object>();
            var objs = t.GetCustomAttributes(typeof(DBAttribute), true);
            string strTableName = string.Empty;
            foreach (var attr in objs.OfType<DBAttribute>())
            {
                strTableName = attr.TableName;//表名只有获取一次
                break;
            }
            var sbWhere = new StringBuilder();
            foreach (var pi in t.GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (var attr in objAttrs.OfType<DBAttribute>())
                {
                    if (attr.IsMapping)
                    {
                        object objPiValue = pi.GetValue(obj, null);//用pi.GetValue获得值

                        //获得属性的类型,进行判断然后进行以后的操作
                        if (objPiValue is string && objPiValue.ToString() != "")
                        {
                            sbWhere.Append(" and " + pi.Name + "=@" + pi.Name);
                            dic.Add("@" + pi.Name, objPiValue);
                        }
                        else if (objPiValue is int && int.Parse(objPiValue.ToString()) > 0)
                        {
                            sbWhere.Append(" and " + pi.Name + "=@" + pi.Name);
                            dic.Add("@" + pi.Name, objPiValue);
                        }
                        else if (objPiValue is long && long.Parse(objPiValue.ToString()) > 0)
                        {
                            sbWhere.Append(" and " + pi.Name + "=@" + pi.Name);
                            dic.Add("@" + pi.Name, objPiValue);
                        }

                    }
                    parms = dic;
                    break;
                }
            }

            sql = string.Format(sql, strTableName, sbWhere.ToString());

            return sql;
        }

        /// <summary>
        /// 排序的方法
        /// </summary>
        /// <param name="orderby">排序</param>
        /// <param name="from">当前页码</param>
        /// <param name="limit">每页显示的数量</param>
        /// <returns></returns>
        public string Orderby(Dictionary<string, int> orderby, long from, long limit)
        {
            string sql;
            string orderStr = string.Empty;
            if (orderby != null && orderby.Count > 0)
            {
                foreach (var item in orderby)
                {
                    if (item.Value < 0)
                        orderStr += string.IsNullOrEmpty(orderStr) ? item.Key + " desc " : "," + item.Key + " desc ";
                    else
                        orderStr += string.IsNullOrEmpty(orderStr) ? item.Key + " asc " : "," + item.Key + " asc ";
                }
            }
            else
            {
                orderStr = " Id asc ";
            }
            sql = " order by " + orderStr;
            if (from >= 0 && limit > 0)
            {
                sql += string.Format(" limit {0} ,{1}  ", from * limit, limit);
            }
            return sql;
        }

        /// <summary>
        /// (+1重载)插入泛型对象
        /// </summary>
        /// <param name="DB"></param>
        /// <param name="obj"></param>
        public void Insert(List<object> lObj)
        {
            List<string> lSql = new List<string>();
            Type t = null;
            object[] objs = null;

            for (int i = 0; i < lObj.Count; i++)
            {
                object obj = lObj[i];
                if (t == null)
                    t = obj.GetType();
                string sql = @"INSERT INTO {0}({1}) VALUES({2})";
                if (objs == null)
                    objs = t.GetCustomAttributes(typeof(DBAttribute), true);
                string strTableName = string.Empty;
                foreach (object o in objs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        strTableName = attr.TableName;//表名只有获取一次
                        break;
                    }
                }
                StringBuilder sbMember = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                    foreach (object o in objAttrs)
                    {
                        DBAttribute attr = o as DBAttribute;
                        if (attr != null)
                        {
                            if (attr.IsMapping)
                            {
                                string strPIName = pi.Name;
                                object objPIValue = pi.GetValue(obj, null);//用pi.GetValue获得值

                                //获得属性的类型,进行判断然后进行以后的操作
                                if (objPIValue != null && objPIValue.GetType() == typeof(string))
                                {
                                    //进行你想要的操作
                                    objPIValue = FormateValue((string)objPIValue);
                                    objPIValue = '\'' + (string)objPIValue + '\'';
                                }
                                else if (objPIValue != null && objPIValue.GetType() == typeof(DateTime))
                                {
                                    //进行你想要的操作
                                    objPIValue = '\'' + objPIValue.ToString() + '\'';
                                }


                                if (objPIValue == null)
                                {
                                    objPIValue = "NULL";
                                }

                                if (Keywords.Contains(pi.Name.ToUpper()))
                                    sbMember.Append("`" + pi.Name + "`,");
                                else
                                    sbMember.Append(pi.Name + ',');
                                sbValue.Append(objPIValue);
                                sbValue.Append(',');
                            }

                            break;
                        }
                    }
                }
                sbMember.Remove(sbMember.Length - 1, 1);
                sbValue.Remove(sbValue.Length - 1, 1);
                sql = string.Format(sql, strTableName, sbMember.ToString(), sbValue.ToString());

                lSql.Add(sql);
            }

            this.BatchExecuteNonQuery(lSql);
        }

        /// <summary>
        /// 更新泛型对象
        /// </summary>
        /// <param name="DB"></param>
        /// <param name="lObj"></param>
        public void Update(object obj)
        {
            Type t = null;
            if (t == null)
                t = obj.GetType();
            string sql = @"Update {0} Set {1} Where {2}";
            object[] objs = t.GetCustomAttributes(typeof(DBAttribute), true);
            if (objs == null)
                objs = t.GetCustomAttributes(typeof(DBAttribute), true);
            string strTableName = string.Empty;
            foreach (object o in objs)
            {
                DBAttribute attr = o as DBAttribute;
                if (attr != null)
                {
                    strTableName = attr.TableName;//表名只有获取一次
                    break;
                }
            }
            StringBuilder sbUpdate = new StringBuilder();
            StringBuilder sbKey = new StringBuilder();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (object o in objAttrs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        if (attr.IsMapping)
                        {
                            string strPIName = pi.Name;
                            object objPIValue = pi.GetValue(obj, null);//用pi.GetValue获得值

                            //获得属性的类型,进行判断然后进行以后的操作
                            if (objPIValue != null && objPIValue.GetType() == typeof(string))
                            {
                                //进行你想要的操作
                                objPIValue = FormateValue((string)objPIValue);
                                objPIValue = '\'' + (string)objPIValue + '\'';
                            }
                            else if (objPIValue != null && objPIValue.GetType() == typeof(DateTime))
                            {
                                //进行你想要的操作
                                objPIValue = '\'' + objPIValue.ToString() + '\'';
                            }

                            if (objPIValue == null)
                            {
                                objPIValue = "NULL";
                            }

                            if (attr.IsKey)
                            {
                                if (Keywords.Contains(pi.Name.ToUpper()))
                                    sbKey.Append(" `" + pi.Name + "`=" + objPIValue + " AND");
                                else
                                    sbKey.Append(" " + pi.Name + '=' + objPIValue + " AND");
                            }
                            else
                            {
                                if (Keywords.Contains(pi.Name.ToUpper()))
                                    sbUpdate.Append(" `" + pi.Name + "`=" + objPIValue + ',');
                                else
                                    sbUpdate.Append(pi.Name + '=' + objPIValue + ',');
                            }
                        }

                        break;
                    }
                }
            }

            if (sbUpdate.Length > 0)
                sbUpdate.Remove(sbUpdate.Length - 1, 1);
            if (sbKey.Length > 2)
                sbKey.Remove(sbKey.Length - 3, 3);
            sql = string.Format(sql, strTableName, sbUpdate.ToString(), sbKey.ToString());
            int x = this.ExecuteNonQuery(sql);
        }

        public string LoadUpdateSql(object obj)
        {
            Type t = null;
            if (t == null)
                t = obj.GetType();
            string sql = @"Update {0} Set {1} Where {2}";
            object[] objs = t.GetCustomAttributes(typeof(DBAttribute), true);
            if (objs == null)
                objs = t.GetCustomAttributes(typeof(DBAttribute), true);
            string strTableName = string.Empty;
            foreach (object o in objs)
            {
                DBAttribute attr = o as DBAttribute;
                if (attr != null)
                {
                    strTableName = attr.TableName;//表名只有获取一次
                    break;
                }
            }
            StringBuilder sbUpdate = new StringBuilder();
            StringBuilder sbKey = new StringBuilder();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (object o in objAttrs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        if (attr.IsMapping)
                        {
                            string strPIName = pi.Name;
                            object objPIValue = pi.GetValue(obj, null);//用pi.GetValue获得值

                            //获得属性的类型,进行判断然后进行以后的操作
                            if (objPIValue != null && objPIValue.GetType() == typeof(string))
                            {
                                //进行你想要的操作
                                objPIValue = FormateValue((string)objPIValue);
                                objPIValue = '\'' + (string)objPIValue + '\'';
                            }
                            else if (objPIValue != null && objPIValue.GetType() == typeof(DateTime))
                            {
                                //进行你想要的操作
                                objPIValue = '\'' + objPIValue.ToString() + '\'';
                            }

                            if (objPIValue == null)
                            {
                                objPIValue = "NULL";
                            }

                            if (attr.IsKey)
                            {
                                if (Keywords.Contains(pi.Name.ToUpper()))
                                    sbKey.Append(" `" + pi.Name + "`=" + objPIValue + " AND");
                                else
                                    sbKey.Append(" " + pi.Name + '=' + objPIValue + " AND");
                            }
                            else
                            {
                                if (Keywords.Contains(pi.Name.ToUpper()))
                                    sbUpdate.Append(" `" + pi.Name + "`=" + objPIValue + ',');
                                else
                                    sbUpdate.Append(pi.Name + '=' + objPIValue + ',');
                            }
                        }

                        break;
                    }
                }
            }

            if (sbUpdate.Length > 0)
                sbUpdate.Remove(sbUpdate.Length - 1, 1);
            if (sbKey.Length > 2)
                sbKey.Remove(sbKey.Length - 3, 3);
            sql = string.Format(sql, strTableName, sbUpdate.ToString(), sbKey.ToString());

            return sql;
        }

        /// <summary>
        /// 更新泛型对象
        /// </summary>
        /// <param name="DB"></param>
        /// <param name="lObj"></param>
        public void Update(List<object> lObj)
        {
            List<string> lSql = new List<string>();
            Type t = null;
            object[] objs = null;

            for (int i = 0; i < lObj.Count; i++)
            {
                object obj = lObj[i];
                if (t == null)
                    t = obj.GetType();
                string sql = @"Update {0} Set {1} Where {2}";
                if (objs == null)
                    objs = t.GetCustomAttributes(typeof(DBAttribute), true);
                string strTableName = string.Empty;
                foreach (object o in objs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        strTableName = attr.TableName;//表名只有获取一次
                        break;
                    }
                }
                StringBuilder sbUpdate = new StringBuilder();
                StringBuilder sbKey = new StringBuilder();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                    foreach (object o in objAttrs)
                    {
                        DBAttribute attr = o as DBAttribute;
                        if (attr != null)
                        {
                            if (attr.IsMapping)
                            {
                                string strPIName = pi.Name;
                                object objPIValue = pi.GetValue(obj, null);//用pi.GetValue获得值

                                //获得属性的类型,进行判断然后进行以后的操作
                                if (objPIValue != null && objPIValue.GetType() == typeof(string))
                                {
                                    //进行你想要的操作
                                    objPIValue = FormateValue((string)objPIValue);
                                    objPIValue = '\'' + (string)objPIValue + '\'';
                                }
                                else if (objPIValue != null && objPIValue.GetType() == typeof(DateTime))
                                {
                                    //进行你想要的操作
                                    objPIValue = '\'' + objPIValue.ToString() + '\'';
                                }

                                if (objPIValue == null)
                                {
                                    objPIValue = "NULL";
                                }

                                if (attr.IsKey)
                                {
                                    sbKey.Append(" " + pi.Name + '=' + objPIValue + " AND");
                                }
                                else
                                {
                                    sbUpdate.Append(pi.Name + '=' + objPIValue + ',');
                                }
                            }

                            break;
                        }
                    }
                }
                sbUpdate.Remove(sbUpdate.Length - 1, 1);
                sbKey.Remove(sbKey.Length - 3, 3);
                sql = string.Format(sql, strTableName, sbUpdate.ToString(), sbKey.ToString());

                lSql.Add(sql);
            }

            this.BatchExecuteNonQuery(lSql);
        }


        public void Delete(List<object> lObj)
        {
            List<string> lSql = new List<string>();
            Type t = null;
            object[] objs = null;
            string sql = @"delete * from {0} where {1} in ({2})";
            for (int i = 0; i < lObj.Count; i++)
            {
                object obj = lObj[i];
                if (t == null)
                    t = obj.GetType();

                if (objs == null)
                    objs = t.GetCustomAttributes(typeof(DBAttribute), true);
                string strTableName = string.Empty;
                foreach (object o in objs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        strTableName = attr.TableName;//表名只有获取一次
                        break;
                    }
                }
                StringBuilder sbUpdate = new StringBuilder();


                //sql = string.Format(sql, strTableName, sbUpdate.ToString(), sbKey.ToString());

                //lSql.Add(sql);
            }

            this.BatchExecuteNonQuery(lSql);
        }


        /// <summary>
        /// 填充对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        public void Fill(object obj, DataRow dr)
        {
            Type t = obj.GetType();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (object o in objAttrs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        if (attr.IsMapping)
                        {
                            string strPIName = pi.Name;
                            if (dr.Table.Columns.Contains(strPIName))
                            {
                                if (dr[strPIName] != DBNull.Value)
                                    pi.SetValue(obj, dr[strPIName], null);
                                else
                                    pi.SetValue(obj, default(object), null);
                            }
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// SQL参数格式化
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string FormateValue(string s)
        {
            if (!string.IsNullOrEmpty(s) && s.Substring(s.Length - 1).Contains("\\"))
                s = s + "\\";
            if (s.Contains("\\\\b"))
                s = s.Replace("\\\\b", "\\\\\\\\b");
            else if (s.Contains("\\b"))
                s = s.Replace("\\b", "\\\\\\\\b");
            else if (s.Contains("\b"))
                s = s.Replace("\b", "\\\\\\\\b");

            s = s.Replace("􀁹 ", "");
            return string.IsNullOrEmpty(s) ? string.Empty : s.Replace("'", "''");
        }
        #endregion

        #region 创建表结构，初始化对象
        private void Init()
        {
            List<string> sqllist = new List<string>();

            sqllist.Add("PRAGMA auto_vacuum = 1");

            sqllist.Add("CREATE TABLE IF NOT EXISTS PROJECT("
                    + "ProjectID VARCHAR(200) primary key, "
                    + "FilterName  VARCHAR(2000),  "
                    + "FilterVersion  VARCHAR(2000),  "
                    + "SLCID INTEGER, "
                    + "TLCID INTEGER, "
                    + "ProjectDisplayName  VARCHAR(200),  "
                    + "SourceFile  VARCHAR(2000),  "
                    + "ProjectFile  VARCHAR(2000),  "
                    + "TempFileName  VARCHAR(2000),  "
                    + "ProjectPath  VARCHAR(2000))");

            sqllist.Add("CREATE TABLE IF NOT EXISTS ASSET("
                + "ASSETID VARCHAR(200) primary key, "
                + "FilterNameVersion  VARCHAR(200),  "
                + "FileName  VARCHAR(2000),  "
                + "Massage  VARCHAR(2000),  "
                + "Flag  VARCHAR(200),  "
                + "Words  INTEGER,  "
                + "Segments INTEGER)");

            sqllist.Add("CREATE TABLE IF NOT EXISTS TU("
                + "TID INTEGER, "
                + "ASSETID VARCHAR(200), "
                + "Source VARCHAR(2000), "
                + "Target VARCHAR(2000), "
                + "Comments VARCHAR(2000), "
                + "Words INTEGER, "
                + "Type INTEGER, "
                + "ParagraphID INTEGER, "
                + "RunID INTEGER, "
                + "Repetition INTEGER, "
                + "primary key (TID,ASSETID)) ");
            sqllist.Add("CREATE INDEX IF NOT EXISTS TU_TYPE ON TU(TYPE)");

            sqllist.Add("CREATE TABLE IF NOT EXISTS SPLACEHOLDER("
                + "PHID INTEGER primary key autoincrement, "
                + "TUID INTEGER, "
                + "NUMTEXT VARCHAR(50), "
                + "PHTEXT VARCHAR(2000), "
                + "TYPE VARCHAR(20), "
                + "LEFTEDGE VARCHAR(20), "
                + "RIGHTEDGE VARCHAR(20), "
                + "DISPLAYTEXT VARCHAR(2000))");
            sqllist.Add("CREATE INDEX IF NOT EXISTS SPLACEHOLDER_TUID ON SPLACEHOLDER(TUID)");

            sqllist.Add("CREATE TABLE IF NOT EXISTS TPLACEHOLDER("
                + "PHID INTEGER primary key autoincrement, "
                + "TUID INTEGER, "
                + "NUMTEXT VARCHAR(50), "
                + "PHTEXT VARCHAR(2000), "
                + "TYPE VARCHAR(20), "
                + "LEFTEDGE VARCHAR(20), "
                + "RIGHTEDGE VARCHAR(20), "
                + "DISPLAYTEXT VARCHAR(2000))");
            sqllist.Add("CREATE INDEX IF NOT EXISTS TPLACEHOLDER_TUID ON TPLACEHOLDER(TUID)");

            sqllist.Add("CREATE TABLE IF NOT EXISTS STYLE("
                + "STYLEID INTEGER primary key autoincrement, "
                + "CONTENT VARCHAR(2000)) ");

            sqllist.Add("CREATE TABLE IF NOT EXISTS STYLEMATCH("
                + "MATCHID INTEGER primary key autoincrement, "
                + "STYLEID1 INTEGER, "
                + "STYLEID2 INTEGER) ");
            sqllist.Add("CREATE INDEX IF NOT EXISTS STYLEMATCH_STYLEID1 ON STYLEMATCH(STYLEID1)");
            sqllist.Add("CREATE INDEX IF NOT EXISTS STYLEMATCH_STYLEID2 ON STYLEMATCH(STYLEID2)");
            BatchExecuteNonQuery(sqllist);
        }
        #endregion

        #region 数据转换
        public object ConvertDataRow<T>(DataRow dr, string head = "")
        {
            object obj = Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (object o in objAttrs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        if (attr.IsMapping)
                        {
                            string strPIName = head + pi.Name;
                            if (dr.Table.Columns.Contains(strPIName))
                            {
                                try
                                {
                                    if (dr[strPIName] != DBNull.Value)
                                        pi.SetValue(obj, dr[strPIName], null);
                                    else
                                        pi.SetValue(obj, default(object), null);

                                }
                                catch (Exception ex)
                                {

                                    //throw new Exception(string.Format("Exception:" + ex.Message), ex);
                                }
                            }
                        }

                        break;
                    }
                }
            }
            return obj;
        }

        public object ConvertDataRow<T>(DataRow dr, Dictionary<string, string> dic)
        {
            object obj = Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                object[] objAttrs = pi.GetCustomAttributes(typeof(DBAttribute), true);
                foreach (object o in objAttrs)
                {
                    DBAttribute attr = o as DBAttribute;
                    if (attr != null)
                    {
                        if (attr.IsMapping)
                        {
                            string strPIName = pi.Name;
                            if (dic.ContainsKey(pi.Name))
                                strPIName = dic[pi.Name];
                            if (dr.Table.Columns.Contains(strPIName))
                            {
                                try
                                {
                                    if (dr[strPIName] != DBNull.Value)
                                        pi.SetValue(obj, dr[strPIName], null);
                                    else
                                        pi.SetValue(obj, default(object), null);

                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(string.Format("Exception:" + ex.Message), ex);
                                }
                            }
                        }

                        break;
                    }
                }
            }
            return obj;
        }
        public List<T> ConvertDataTable<T>(DataTable dt, string head = "")
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add((T)ConvertDataRow<T>(dr, head));
            }
            return list;
        }

        public List<T> ConvertDataTable<T>(DataTable dt, Dictionary<string, string> dic)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add((T)ConvertDataRow<T>(dr, dic));
            }
            return list;
        }
        #endregion

    }
}