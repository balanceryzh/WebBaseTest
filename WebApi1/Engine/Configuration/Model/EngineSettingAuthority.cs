using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.EnumBase;

namespace WebApi1.Engine
{
    public class EngineSettingAuthority
    {
        /// <summary>
        /// 授权类型
        /// </summary>
        public EnumAuthenticationType Authentication { get; set; }

        /// <summary>
        /// issuer 请求实体，可以是发起请求的用户的信息，也可是jwt的签发者。
        /// </summary>
        public string Issuer { get; set; } = "UTH Auth Server";

        /// <summary>
        /// 订阅者
        /// 对订阅进行验证，减少转发攻击。例如，一个接收令牌的站点不能将其重放到另一个。一个转发的令牌将包含原始站点的订阅。
        /// </summary>
        public List<string> Audiences { get; set; } = new List<string>();

        /// <summary>
        /// 登录地址
        /// </summary>
        public string LoginPath { get; set; } = "/account/login";

        /// <summary>
        /// 退出地址
        /// </summary>
        public string LogoutPath { get; set; } = "/account/logout";

        /// <summary>
        /// 无权限地址
        /// </summary>
        public string AccessDeniedPath { get; set; } = "/error/access";

        /// <summary>
        /// 密钥Key
        /// JWT Key 必须是16个字符
        /// </summary>
        public string SecretKey { get; set; } = "abcdefghijkhim1234567890";

        /// <summary>
        /// 过期时间(S/秒)
        /// 默认60*30(30分)
        /// </summary>
        public int ExpireTime { get; set; } = 1800;
    }
}