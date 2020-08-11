using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.InterFace
{
    /// <summary>
    /// 主键实体接口
    /// </summary>
    public interface IPKey<TKey>
    {
        /// <summary>
        /// id
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// 主键实体接口
    /// </summary>
    public interface IPKey : IPKey<Guid>
    {
    }
}