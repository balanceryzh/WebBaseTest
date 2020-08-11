using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Framework
{
    public interface IXmlConfiguration<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// 文件路径
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// 缓存Key
        /// </summary>
        string CacheKey { get; }

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        TEntity Load(bool checkTime = true);

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        bool Save(TEntity entity);
    }
}