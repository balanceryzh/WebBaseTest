using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Domains.Uow
{
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// 连接信息
        /// </summary>
        public RepositoryConnection Connection { get; set; }
    }
}