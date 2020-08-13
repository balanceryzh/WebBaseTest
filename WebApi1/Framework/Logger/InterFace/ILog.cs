using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi1.Framework
{
    public interface ILog
    {
        /// <summary>
        /// 写入LogLevel.Trace日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Trace<T>(T message);

        /// <summary>
        /// 写入LogLevel.Trace格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Trace(string format, params object[] args);

        /// <summary>
        /// 写入LogLevel.Debug日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Debug<T>(T message);

        /// <summary>
        /// 写入LogLevel.Debug格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Debug(string format, params object[] args);

        /// <summary>
        /// 写入LogLevel.Info日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Info<T>(T message);

        /// <summary>
        /// 写入LogLevel.Info格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Info(string format, params object[] args);

        /// <summary>
        /// 写入LogLevel.Warn日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Warn<T>(T message);

        /// <summary>
        /// 写入LogLevel.Warn格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Warn(string format, params object[] args);

        /// <summary>
        /// 写入LogLevel.Error日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Error<T>(T message);

        /// <summary>
        /// 写入LogLevel.Error格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Error(string format, params object[] args);

        /// <summary>
        /// 写入LogLevel.Fatal日志消息
        /// </summary>
        /// <param name="message">日志消息</param>
        void Fatal<T>(T message);

        /// <summary>
        /// 写入LogLevel.Fatal格式化日志消息
        /// </summary>
        /// <param name="format">日志消息格式</param>
        /// <param name="args">格式化参数</param>
        void Fatal(string format, params object[] args);
    }
}
