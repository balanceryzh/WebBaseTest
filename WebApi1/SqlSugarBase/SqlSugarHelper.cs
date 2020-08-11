using SqlSugar;
using WebApi1.Connection;
using WebApi1.EnumBase;
using WebApi1.Utility;

namespace WebApi1.SqlSugarBase
{
    public class SqlSugarHelper
    {
        public static SqlSugarClient GetContext(ConnectionOptions connection)
        {
            var config = new ConnectionConfig()
            {
                ConnectionString = connection.Get(),
                DbType = ConvertSqlSugarDBType(connection.DatabaseType),
                IsAutoCloseConnection = connection.AutoCloseConnection,
                //IsShardSameThread = true //设为true相同线程是同一个SqlSugarClient http://www.codeisbug.com/Doc/8/1158
                //不能使用异步方法
            };

            var model = new SqlSugarClient(config);



            return model;
        }

        /// <summary>
        /// EnumDBTyp 转SqlSugar数据库类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static SqlSugar.DbType ConvertSqlSugarDBType(EnumDatabaseType dbType)
        {
            switch (dbType)
            {
                case EnumDatabaseType.SqlServer:
                    return SqlSugar.DbType.SqlServer;
                case EnumDatabaseType.MySql:
                    return SqlSugar.DbType.MySql;
                case EnumDatabaseType.Oracle:
                    return SqlSugar.DbType.Oracle;
                case EnumDatabaseType.Sqlite:
                    return SqlSugar.DbType.Sqlite;
                default:
                    return SqlSugar.DbType.SqlServer;
            }
        }

        /// <summary>
        /// SqlSugar数据库类型转 EnumDbTyp
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static EnumDatabaseType ConvertDatabaseType(SqlSugar.DbType dbType)
        {
            switch (dbType)
            {
                case SqlSugar.DbType.SqlServer:
                    return EnumDatabaseType.SqlServer;
                case SqlSugar.DbType.MySql:
                    return EnumDatabaseType.MySql;
                case SqlSugar.DbType.Oracle:
                    return EnumDatabaseType.Oracle;
                case SqlSugar.DbType.Sqlite:
                    return EnumDatabaseType.Sqlite;
                default:
                    return EnumDatabaseType.SqlServer;
            }
        }
       /// <summary>
        /// 转排序
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OrderByType ConvertOrderType(string value)
        {
            if (value.IsEmpty())
                return OrderByType.Asc;
            value = value.ToLower().Trim();
            if (value == "desc" || value == "1")
                return OrderByType.Desc;

            return OrderByType.Asc;
        }
    }
}