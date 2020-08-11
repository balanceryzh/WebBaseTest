using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.EnumBase;
using WebApi1.Utility;

namespace WebApi1.Domains.Entities
{
    /// <summary>
    /// 账户
    /// </summary>
    [SugarTable("UC_Account")]
    public class AccountEntity : FullEntity
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AccountEntity()
        {
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 认证手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 认证邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string CertificateType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string CertificateNo { get; set; }

        /// <summary>
        /// 规范登录账号
        /// </summary>
        public string NormalizedAccount { get; set; }

        /// <summary>
        /// 规范邮箱地址
        /// </summary>
        public string NormalizedEmail { get; set; }

        /// <summary>
        /// 启用双因素认证(ref:http://blog.sina.com.cn/s/blog_3c9e07d40101agix.html)
        /// </summary>
        public bool IsTwoFactor { get; set; }

        /// <summary>
        /// 启用锁定
        /// </summary>
        public bool IsLockout { get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime LockoutEndDateUtc { get; set; } = DateTimeHelper.DefaultDateTime;

        /// <summary>
        /// 验证失败次数
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 安全相关信息快照。
        /// 应用场景，假如说用户修改了密码或者是修改了角色，退出等涉及到用户安全相关的时候，这个时候数据库这个值就会改变
        /// ref:https://www.cnblogs.com/savorboard/p/5422084.html
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 当用户被持久化存储到存储区时，必须更改的随机值
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 登录 Ip
        /// </summary>
        public string LoginIp { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginDt { get; set; } = DateTimeHelper.DefaultDateTime;

        /// <summary>
        /// 积分
        /// </summary>
        public decimal Integrals { get; set; }

        /// <summary>
        /// 资金
        /// </summary>
        public decimal Moneys { get; set; }

        /// <summary>
        /// 邀请人账号Id
        /// </summary>
        public Guid InviterId { get; set; }

        /// <summary>
        /// 上级账号Id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expiration { get; set; } = DateTimeHelper.DefaultDateTime;

        /// <summary>
        /// 账户类型(EnumAccountType)
        /// </summary>
        public EnumAccountType Type { get; set; }

        /// <summary>
        /// 账户状态(EnumAccountStatus)
        /// </summary>
        public EnumAccountStatus Status { get; set; }
    }
}