using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebApi1.Identity.Model;

namespace WebApi1.Identity
{

    public interface ISession
    {
        /// <summary>
        /// 身份信息
        /// </summary>
        IPrincipal Principal { get; }

        /// <summary>
        /// 访问信息
        /// </summary>
        IAccessor Accessor { get; }

        /// <summary>
        /// 访问用户
        /// </summary>
        Identifier User { get; }

        /// <summary>
        /// 是否认证
        /// </summary>
        bool IsAuthenticated { get; }
    }
}