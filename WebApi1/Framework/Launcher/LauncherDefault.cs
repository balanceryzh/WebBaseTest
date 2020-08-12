using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Utility;

namespace WebApi1.Framework
{
    public class LauncherDefault : ILauncher
    {

        IHosting appHosting { get; set; }

        /// <summary>
        /// 服务列表
        /// </summary>
        public List<IHosting> Hostings { get { return new List<IHosting>() { appHosting }; } }

        /// <summary>
        /// 附加宿主
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hosting"></param>
        public ILauncher Append(IHosting hosting)
        {
            hosting.CheckNull(nameof(hosting));
            appHosting = hosting;
            return this;
        }

        /// <summary>
        /// 启动器开始
        /// </summary>
        public ILauncher Startup()
        {
            appHosting?.OnStartup();
            return this;
        }

        /// <summary>
        /// 启动器停止
        /// </summary>
        public ILauncher Stop()
        {
            appHosting?.OnStop();
            return this;
        }
    }
}