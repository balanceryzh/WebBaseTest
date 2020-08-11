using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Domains.Uow;
using WebApi1.InterFace;

namespace WebApi1.InterFace
{
    public interface IUnitOfWork : IUnitOfWorkActive, IUnitOfWorkCompleteHandle, IDependency, IDisposable
    {
        /// <summary>
        /// 开始事务
        /// </summary>
        void Begin(UnitOfWorkOptions options);

        /// <summary>
        /// 获取外部引用
        /// </summary>
        /// <returns></returns>
        IUnitOfWork GetOuter();

        /// <summary>
        /// 设置外部引用
        /// </summary>
        /// <param name="outer"></param>
        void SetOuter(IUnitOfWork outer);
    }
}