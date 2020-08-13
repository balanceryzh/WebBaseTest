using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.EnumBase;

namespace WebApi1.Framework
{
    public class LoggerCreate : ILog
    {
        private readonly ICollection<ILogger> _logs;

        public LoggerCreate(string name)
        {
            _logs = LoggingManager.Adapters.Select(adapter => adapter.GetLogger(name)).ToList();
        }

        static LoggerCreate()
        {
            EntryEnabled = true;
            EntryEnumLoglevel = EnumLoglevel.All;
        }

        #region 静态设置全局，日志级别的入口控制，级别决定是否执行相应级别的日志记录功能

        /// <summary>
        /// 获取或设置 日志级别的入口控制，级别决定是否执行相应级别的日志记录功能
        /// </summary>
        public static EnumLoglevel EntryEnumLoglevel { get; set; }

        /// <summary>
        /// 获取或设置 是否允许记录日志，如为 false，将完全禁止日志记录
        /// </summary>
        public static bool EntryEnabled { get; set; }

        #endregion

        #region Implementation of ILog

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Trace"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Trace<T>(T message)
        {
            if (!IsEnabledFor(EnumLoglevel.Trace))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Trace(message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Trace"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Trace(string format, params object[] args)
        {
            if (!IsEnabledFor(EnumLoglevel.Trace))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Trace(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Debug"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Debug<T>(T message)
        {
            if (!IsEnabledFor(EnumLoglevel.Debug))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Debug(message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Debug"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Debug(string format, params object[] args)
        {
            if (!IsEnabledFor(EnumLoglevel.Debug))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Debug(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Info"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Info<T>(T message)
        {
            if (!IsEnabledFor(EnumLoglevel.Info))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Info(message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Info"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Info(string format, params object[] args)
        {
            if (!IsEnabledFor(EnumLoglevel.Info))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Info(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Warn"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Warn<T>(T message)
        {
            if (!IsEnabledFor(EnumLoglevel.Warn))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Warn(message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Warn"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Warn(string format, params object[] args)
        {
            if (!IsEnabledFor(EnumLoglevel.Warn))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Warn(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Error"/>日志消息
        /// </summary>
        /// <param name="obj">日志消息</param>
        public void Error<T>(T obj)
        {
            if (!IsEnabledFor(EnumLoglevel.Error))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Error(obj);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Error"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Error(string format, params object[] args)
        {
            if (!IsEnabledFor(EnumLoglevel.Error))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Error(format, args);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Fatal"/>日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Fatal<T>(T message)
        {
            if (!IsEnabledFor(EnumLoglevel.Fatal))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Fatal(message);
            }
        }

        /// <summary>
        /// 写入<see cref="EnumLoglevel.Fatal"/>格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        public void Fatal(string format, params object[] args)
        {
            if (!IsEnabledFor(EnumLoglevel.Fatal))
            {
                return;
            }
            foreach (ILogger log in _logs)
            {
                log.Fatal(format, args);
            }
        }

        #endregion

        #region 私有方法

        private static bool IsEnabledFor(EnumLoglevel level)
        {
            return EntryEnabled && level >= EntryEnumLoglevel;
        }

        #endregion
    }
}