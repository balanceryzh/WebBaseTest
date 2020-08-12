using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Framework
{
    public interface IHosting
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        Guid Key { get; }

        /// <summary>
        /// 启动事件
        /// </summary>
        void OnStartup();

        /// <summary>
        /// 开始事件
        /// </summary>
        void OnStart();

        /// <summary>
        /// 停止事件
        /// </summary>
        void OnStop();

        /// <summary>
        /// 开始事件(请求/处理?)
        /// </summary>
        void OnBegin();

        /// <summary>
        /// 结束事件(请求/处理?)
        /// </summary>
        void OnEnd();

    }
}