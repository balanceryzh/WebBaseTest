using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Entity.Data.User
{
    [DBAttribute(TableName = "TB_User")]
    public class TB_User
    {


        /// <summary>
        /// Id
        /// </summary>
        [DBAttribute(IsKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 账户id
        /// </summary>
        [DBAttribute]
        public long AccountId { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [DBAttribute]
        public string Account { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [DBAttribute]
        public string NickName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [DBAttribute]
        public string RealName { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [DBAttribute]
        public string Country { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [DBAttribute]
        public string Province { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [DBAttribute]
        public string City { get; set; }

        /// <summary>
        /// Area
        /// </summary>
        [DBAttribute]
        public string Area { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [DBAttribute]
        public string Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [DBAttribute]
        public string PostCode { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        [DBAttribute]
        public int Age { get; set; }

        /// <summary>
        /// Birthday
        /// </summary>
        [DBAttribute]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [DBAttribute]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [DBAttribute]
        public string Head { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        [DBAttribute]
        public string Signature { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DBAttribute]
        public string Email { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        [DBAttribute]
        public string Mobile { get; set; }

        /// <summary>
        /// 手机号国家编码
        /// </summary>
        [DBAttribute]
        public string CountryNumber { get; set; }

        /// <summary>
        /// 销售编号
        /// </summary>
        [DBAttribute]
        public string SalesNumber { get; set; }

        /// <summary>
        /// CompanyKey
        /// </summary>
        [DBAttribute]
        public string CompanyKey { get; set; }

        /// <summary>
        /// Sex
        /// </summary>
        [DBAttribute]
        public int Sex { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [DBAttribute]
        public int Type { get; set; }

        /// <summary>
        /// State
        /// </summary>
        [DBAttribute]
        public int State { get; set; }

    }
}