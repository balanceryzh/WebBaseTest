using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.InterFace
{
    public interface IAudit
    {
        /// <summary>
        /// 添加人Id 
        /// </summary>
        Guid CreateId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        DateTime CreateDt { get; set; }

        /// <summary>
        /// 修改人Id 
        /// </summary>
        Guid LastId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime LastDt { get; set; }
    }
}