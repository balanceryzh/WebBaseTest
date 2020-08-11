using System;
using WebApi1.EnumBase;

namespace WebApi1.Resource
{

    /// <summary>
    /// EnumCode异常接口(自定义异常)
    /// </summary>
    public interface ICodeException
    {
        /// <summary>
        /// 异常Code
        /// </summary>
        EnumCode Code { get; set; }
    }

    /// <summary>
    /// EnumCode异常(自定义异常)
    /// </summary>
    public class CodeException : Exception, ICodeException
    {
        /// <summary>
        /// 异常Code
        /// </summary>
        public virtual EnumCode Code { get; set; } = EnumCode.错误;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        public CodeException(EnumCode code) : base(code.ToString())
        {
            Code = code;
        }

        /// <summary>
        /// ctore
        /// </summary>
        /// <param name="message"></param>
        public CodeException(string message) : base(message)
        {
            Code = EnumCode.提示;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public CodeException(EnumCode code, string message) : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code"></param>
        /// <param name="exception"></param>
        public CodeException(EnumCode code, Exception exception) : base(exception.Message, exception)
        {
            Code = code;
        }


    }
}