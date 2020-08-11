using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.EnumBase
{
    public enum EnumClientType
    {
        /// <summary>
        /// default
        /// </summary>
        Default = 0,
        /// <summary>
        /// 控制台
        /// </summary>
        Console = 1,
        /// <summary>
        /// 服务
        /// </summary>
        Server = 2,
        /// <summary>
        /// Winfrom
        /// </summary>
        Winfrom = 3,
        /// <summary>
        /// WPF
        /// </summary>
        Wpf = 4,
        /// <summary>
        /// Website
        /// </summary>
        Website = 5,
        /// <summary>
        /// Api
        /// </summary>
        Api = 6,
        /// <summary>
        /// Wap
        /// </summary>
        Wap = 7,
    }
}