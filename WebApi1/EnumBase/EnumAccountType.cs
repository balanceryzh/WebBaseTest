using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.EnumBase
{
    public enum EnumAccountType
    {
        /// <summary>
        /// default
        /// </summary>
        Default = 0,
        /// <summary>
        /// 个人用户
        /// </summary>
        个人用户 = 1,
        /// <summary>
        /// 组织用户
        /// </summary>
        组织用户 = 2,
        /// <summary>
        /// 成员用户(组织子用户)
        /// </summary>
        成员用户 = 3,
        /// <summary>
        /// 管理员用户
        /// </summary>
        管理员用户 = 8,
        /// <summary>
        /// 超级管理员
        /// </summary>
        超级管理员 = 9
    }
}