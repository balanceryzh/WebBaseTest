using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Utility;

namespace WebApi1.Framework
{
    public class LoggingManager
    {
        private static readonly ConcurrentDictionary<string, ILog> Loggers;
        internal static ICollection<ILoggerAdapter> Adapters { get; private set; }
        private static readonly object lockObj = new object();

        /// <summary>
        /// 初始化一个<see cref="LoggingManager"/>实例
        /// </summary>
        static LoggingManager()
        {
            Loggers = new ConcurrentDictionary<string, ILog>();
            Adapters = new List<ILoggerAdapter>();
        }

        #region 增加/移除 Adapter

        /// <summary>
        /// 添加日志适配器
        /// </summary>
        public static void AddLoggerAdapter(ILoggerAdapter adapter)
        {
            lock (lockObj)
            {
                if (Adapters.Any(m => m == adapter))
                {
                    return;
                }
                Adapters.Add(adapter);
                Loggers.Clear();
            }
        }

        /// <summary>
        /// 移除日志适配器
        /// </summary>
        public static void RemoveLoggerAdapter(ILoggerAdapter adapter)
        {
            lock (lockObj)
            {
                if (Adapters.All(m => m != adapter))
                {
                    return;
                }
                Adapters.Remove(adapter);
                Loggers.Clear();
            }
        }

        #endregion

        #region 获取 ILog(Logger中的操作)

        /// <summary>
        /// 获取指定类型的日志记录实例
        /// </summary>
        public static ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        /// <summary>
        /// 获取指定类型的日志记录实例
        /// </summary>
        public static ILog GetLogger(Type type)
        {
            type.CheckNull(nameof(type));
            return GetLogger(type.FullName);
        }

        /// <summary>
        /// 获取日志记录者实例
        /// </summary>
        public static ILog GetLogger(string name)
        {
            name.CheckEmpty(nameof(name));
            lock (lockObj)
            {
                ILog logger;
                if (Loggers.TryGetValue(name, out logger))
                {
                    return logger;
                }
                logger = new LoggerCreate(name);
                Loggers[name] = logger;
                return logger;
            }
        }

        #endregion

    }
}