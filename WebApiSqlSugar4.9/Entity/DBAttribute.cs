using System;
using System.Runtime.Serialization;

namespace WebApi1.Entity
{
    [DataContract]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = true)]
    public class DBAttribute : Attribute
    {
        public DBAttribute()
        {
            IsMapping = true;
        }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 是否映射
        /// </summary>
        public bool IsMapping { get; set; }

        /// <summary>
        /// 是否为关键字
        /// </summary>
        public bool IsKey { get; set; }
    }
}