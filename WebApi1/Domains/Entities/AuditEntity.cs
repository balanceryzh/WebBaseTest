using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.InterFace;

namespace WebApi1.Domains.Entities
{
    public class AuditEntity : PKeyEntity, IPKey, IAudit
    {
        /// <summary>
        /// 添加人Id
        /// </summary>
        public Guid CreateId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDt { get; set; }

        /// <summary>
        /// 修改人Id
        /// </summary>
        public Guid LastId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastDt { get; set; }
    }
}