using WebApi1.EnumBase;

namespace WebApi1.Connection
{
    public class ConnectionOptions
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ConnectionOptions() { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public ConnectionOptions(string connectionString)
        {

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                ConnectionString = connectionString;
            }

        }

        /// <summary>
        /// ctor
        /// </summary>
        public ConnectionOptions(ConnectionOptions connection)
        {
            if (connection != null)
            {
                Enabled = connection.Enabled;
                ConnectionString = connection.ConnectionString;
                DatabaseType = connection.DatabaseType;
                DatabaseName = connection.DatabaseName;
                Address = connection.Address;
                ReadAddress = connection.ReadAddress;
                WriteAddress = connection.WriteAddress;
                Port = connection.Port;
                User = connection.User;
                Password = connection.Password;
                AutoStart = connection.AutoStart;
                AutoCloseConnection = connection.AutoCloseConnection;
                PoolSize = connection.PoolSize;
                Coding = connection.Coding;
                Provider = connection.Provider;
                ExpiresType = connection.ExpiresType;
                ExpireTime = connection.ExpireTime;
            }
        }



        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 库类型
        /// </summary>
        public EnumDatabaseType DatabaseType { get; set; }

        /// <summary>
        /// 库名称
        /// </summary>
        public string DatabaseName { get; set; }



        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 读地址
        /// </summary>
        public string ReadAddress { get; set; }

        /// <summary>
        /// 写地址
        /// </summary>
        public string WriteAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 自动启动
        /// </summary>
        public bool AutoStart { get; set; } = true;

        /// <summary>
        /// 是否自动关闭连接
        /// </summary>
        public bool AutoCloseConnection { get; set; } = true;

        /// <summary>
        /// 连接队例长度
        /// </summary>
        public int PoolSize { get; set; }


        /// <summary>
        /// 编码
        /// </summary>
        public string Coding { get; set; } = "utf8";

        /// <summary>
        /// 驱动
        /// </summary>
        public string Provider { get; set; }


        /// <summary>
        /// 过期类型
        /// </summary>
        public EnumExpiresType ExpiresType { get; set; }

        /// <summary>
        /// 过期时间(S/秒)
        /// </summary>
        public int ExpireTime { get; set; }
    }
}