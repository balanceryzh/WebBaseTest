using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi1.Domains.Uow;

namespace WebApi1.InterFace
{
    public interface IUnitOfWorkActive
    {
        /// <summary>
        /// 单元标识
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 是否释放
        /// </summary>
        bool IsDisposed { get; }


        /// <summary>
        /// 完成事件
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// 失败事件
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 释放事件
        /// </summary>
        event EventHandler Disposed;


        /// <summary>
        /// 保存变更
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// 保存变更(异步)
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}