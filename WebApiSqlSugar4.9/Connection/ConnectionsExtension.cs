using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Web;

using WebApi1.EnumBase;

namespace WebApi1.Connection
{
    public static class ConnectionsExtension
    {

        public static string Get(this ConnectionOptions option)
        {
            if (option == null)
                return string.Empty;

            if (!string.IsNullOrWhiteSpace(option.ConnectionString))
                return option.ConnectionString;

            StringBuilder builder = new StringBuilder();
            switch (option.DatabaseType)
            {
                case EnumDatabaseType.SqlServer:
                    builder.AppendFormat("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", option.Address, option.DatabaseName, option.User, option.Password);
                    break;
                case EnumDatabaseType.MySql:
                    if (option.Port == 0) { option.Port = 3306; }
                    builder.AppendFormat("Data Source={0};Initial Catalog={1};user id={2};password={3};port={4};Charset={5}", option.Address, option.DatabaseName, option.User, option.Password, option.Port, option.Coding);
                    break;
                case EnumDatabaseType.Oracle:
                    builder.AppendFormat("Data Source={0};Initial Catalog={1};user id={2};password={3};persist security info=false;", option.Address, option.DatabaseName, option.User, option.Password);
                    break;
                case EnumDatabaseType.Sqlite:
                    builder.AppendFormat("Data Source={0};", option.Address);
                    break;
                case EnumDatabaseType.Redis:

                    if (option.Port == 0)
                    {
                        option.Port = 6379;
                    }
                    if (option.PoolSize == 0)
                    {
                        option.PoolSize = 60;
                    }
                    if (string.IsNullOrWhiteSpace(option.DatabaseName))
                    {
                        option.DatabaseName = "0";
                    }

                    string readStr = "", writeStr = "";
                    if (string.IsNullOrWhiteSpace(option.Password))
                    {
                        readStr = "";
                        writeStr = "";
                    }
                    else
                    {
                        readStr = string.Format("{0}@", option.Password);
                        writeStr = string.Format("{0}@", option.Password);
                    }
                    if (string.IsNullOrWhiteSpace(option.ReadAddress))
                    {
                        readStr = readStr + option.Address;
                    }
                    else
                    {
                        readStr = readStr + option.ReadAddress;
                    }
                    if (string.IsNullOrWhiteSpace(option.WriteAddress))
                    {
                        writeStr = writeStr + option.Address;
                    }
                    else
                    {
                        writeStr = writeStr + option.WriteAddress;
                    }

                    readStr = readStr + ":" + option.Port.ToString();
                    writeStr = writeStr + ":" + option.Port.ToString();

                    builder.AppendFormat("{0};{1};MaxWritePoolSize={2};MaxReadPoolSize={2};AutoStart={3};DefaultDb={4};",
                      readStr, writeStr, option.PoolSize, option.DatabaseName);
                    break;
                case EnumDatabaseType.Mongodb:
                    //builder.AppendFormat("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", Address, DBName, User, Password);
                    break;
            }
            return builder.ToString();
        }
    }
}