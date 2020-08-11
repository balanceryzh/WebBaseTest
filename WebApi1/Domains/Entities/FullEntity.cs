using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.InterFace;

namespace WebApi1.Domains.Entities
{
    public class FullEntity : AuditEntity, IPKey, IAudit, ISoftDelete
    {
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}