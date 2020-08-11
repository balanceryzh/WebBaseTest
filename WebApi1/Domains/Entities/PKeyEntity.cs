using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.InterFace;

namespace WebApi1.Domains.Entities
{
    /// <summary>
    /// 主键实现（Key）
    /// </summary>
    public class PKeyEntity : IPKey, IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
    }
}