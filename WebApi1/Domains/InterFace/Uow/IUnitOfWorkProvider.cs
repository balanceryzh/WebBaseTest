using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.InterFace
{
    public interface IUnitOfWorkProvider
    {
        /// <summary>
        /// 当前工作单元
        /// </summary>
        IUnitOfWork Current { get; set; }
    }
}