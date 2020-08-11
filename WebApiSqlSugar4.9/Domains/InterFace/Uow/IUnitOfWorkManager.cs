using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Domains.Uow;

namespace WebApi1.InterFace
{
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// 获取当前活动的工作单元(如果不存在，则为空)。
        /// </summary>
        IUnitOfWorkActive Current { get; }

        /// <summary>
        /// 开始单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options = null);
    }
}