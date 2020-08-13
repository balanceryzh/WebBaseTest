using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Framework
{
    /// <summary>
    /// 验证配置
    /// </summary>
    public static class FluentValidationOptions
    {
        /// <summary>
        /// 配置
        /// </summary>
        public static void Configuration()
        {
            ValidatorOptions.LanguageManager = new FluentValidatorLanguageManager();
        }
    }
}