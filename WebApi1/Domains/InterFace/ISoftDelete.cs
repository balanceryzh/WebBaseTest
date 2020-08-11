using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.InterFace
{
    /// <summary>
    /// 逻辑删除实体
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        bool IsDelete { get; set; }
    }
}