using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Framework
{
    public interface ILauncher
    {
        /// <summary>
        /// 服务列表
        /// </summary>
        List<IHosting> Hostings { get; }

        /// <summary>
        /// 附加服务
        /// </summary>
        /// <param name="hosting"></param>
        /// <returns></returns>
        ILauncher Append(IHosting hosting);

        /// <summary>
        /// 启动器开始
        /// </summary>
        ILauncher Startup();

        /// <summary>
        /// 启动器停止
        /// </summary>
        ILauncher Stop();

    }
}