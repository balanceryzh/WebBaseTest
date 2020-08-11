using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.EnumBase
{
    public enum EnumAuthenticationType
    {
        /// <summary>
        /// none
        /// </summary>
        None = 0,
        /// <summary>
        /// CookieCollection
        /// </summary>
        Cookie = 1,
        /// <summary>
        /// Jwt
        /// </summary>
        Jwt=2,
        /// <summary>
        /// UTH 自定义认证(使用Redis 存储 Session)
        /// </summary>
        UTH = 3
    }
}