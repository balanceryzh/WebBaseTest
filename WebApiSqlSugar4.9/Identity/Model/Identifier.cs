using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.EnumBase;

namespace WebApi1.Identity.Model
{
    [Serializable]
    public class Identifier
    {
        /// <summary>
        /// 账户Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// 上级账号
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EnumAccountType Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EnumAccountStatus Status { get; set; }
    }
}