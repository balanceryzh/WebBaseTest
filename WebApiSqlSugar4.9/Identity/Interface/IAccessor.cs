using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebApi1.Identity
{
    /// <summary>
    /// 访问器接口
    /// </summary>
    public interface IAccessor
    {
        /// <summary>
        /// 应用编号
        /// </summary>
        string AppCode { get; }

        /// <summary>
        /// 区域文化
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// 客户端地址
        /// </summary>
        string ClientIp { get; }

    }
}