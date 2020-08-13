using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Framework { 
    public interface ILogger: ILog
    {

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoglevel.Trace"/>级别的日志
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoglevel.Debug"/>级别的日志
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoglevel.Info"/>级别的日志
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoglevel.Warn"/>级别的日志
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoglevel.Error"/>级别的日志
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="EnumLoglevel.Fatal"/>级别的日志
        /// </summary>
        bool IsFatalEnabled { get; }
    }
}