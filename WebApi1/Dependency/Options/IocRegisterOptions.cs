using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Dependency.EnumBase;
using WebApi1.Resource;
using WebApi1.Utility;

namespace WebApi1.Options
{
    /// <summary>
    /// Ioc注入 注册配置
    /// </summary>
    public class IocRegisterOptions
    {
        /// <summary>
        /// 按名称注册
        /// 仅支持方法：
        ///     RegisterType[T, TService](IocRegisterOptions option)
        ///     RegisterType(Type type, Type service, IocRegisterOptions option)
        /// </summary>
        public string Named { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<KeyValues<string, object>> Parameters { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public List<KeyValues<string, object>> Properties { get; set; }

        /// <summary>
        /// Aop代理配置(ProxyGenerationOptions)
        /// </summary>
        public ProxyGenerationOptions InterceptorProxy { get; set; }

        /// <summary>
        /// Aop拦截器
        /// </summary>
        public Type[] InterceptorTypes { get; set; }

        /// <summary>
        /// 实例生命周期
        /// </summary>
        public EnumInstanceScope InstanceScope { get; set; } = EnumInstanceScope.Default;
    }

    public static class IocRegisterOptionsExtension
    {
        /// <summary>
        /// IOC注入 添加拦截器
        /// </summary>
        /// <param name="register"></param>
        /// <param name="types"></param>
        public static IocRegisterOptions AddInterceptors(this IocRegisterOptions register, params Type[] types)
        {
            register.CheckNull(nameof(register));
            register.InterceptorTypes.ToList().AddRange(types);
            return register;
        }
    }
}