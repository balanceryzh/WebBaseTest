using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.EnumBase
{
    public enum EnumDatabaseType
    {
        /// <summary>
        /// default
        /// </summary>
        Default = 0,
        /// <summary>
        /// mysql
        /// </summary>
        MySql = 1,
        /// <summary>
        /// sqlserver
        /// </summary>
        SqlServer = 2,
        /// <summary>
        /// sqlite
        /// </summary>
        Sqlite = 3,
        /// <summary>
        /// oracle
        /// </summary>
        Oracle = 4,
        /// <summary>
        /// redis
        /// </summary>
        Redis = 5,
        /// <summary>
        /// mongodb
        /// </summary>
        Mongodb = 6,
        /// <summary>
        /// memory
        /// </summary>
        MemoryCache = 7,
    }
}