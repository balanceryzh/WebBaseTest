using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Options;
using WebApi1.Resource;
using WebApi1.Utility;

namespace WebApi1.Framework
{
    public static class DependencyExtension
    {
        /// <summary>
        /// 附加参数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IocRegisterOptions AppendParams(this IocRegisterOptions options, string key, object value)
        {
            return AppendParams(options, new KeyValues<string, object>(key, value));
        }

        /// <summary>
        /// 附加参数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IocRegisterOptions AppendParams(this IocRegisterOptions options, params KeyValues<string, object>[] param)
        {
            if (options.Parameters.IsEmpty())
            {
                options.Parameters = new List<KeyValues<string, object>>();
            }

            if (param.IsEmpty())
            {
                return options;
            }

            for (var i = 0; i < param.Length; i++)
            {
                if (!options.Parameters.Any(x => x.Key == param[i].Key))
                {
                    options.Parameters.Add(param[i]);
                }
            }

            return options;
        }
    }
}