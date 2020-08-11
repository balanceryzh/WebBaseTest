using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApi1.InterFace
{
    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        /// <summary>
        /// 完成操作
        /// </summary>
        void Complete();

        /// <summary>
        /// 完成操作
        /// </summary>
        Task CompleteAsync();
    }
}