using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApi1.Options;

namespace WebApi1.InterFace
{
    /// <summary>
    /// IOC注入 注册方法
    /// </summary>
    public interface IIocRegister
    {
        /// <summary>
        /// 外部注册(传入核心对象)
        /// </summary>
        /// <param name="action"></param>
        void Register(Action<object> action);

        /// <summary>
        /// 实例注入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void Register<T>(T obj) where T : new();

        /// <summary>
        /// 类型注入
        /// </summary>
        void RegisterType<T>();

        /// <summary>
        /// 类型注入
        /// </summary>
        void RegisterType<T>(IocRegisterOptions option);

        /// <summary>
        /// 类型注入
        /// </summary>
        void RegisterType<T, TService>() where TService : class, T;

        /// <summary>
        /// 类型注入
        /// </summary>
        void RegisterType<T, TService>(IocRegisterOptions option) where TService : class, T;

        /// <summary>
        /// 类型注入
        /// </summary>
        void RegisterType(Type type, Type service);

        /// <summary>
        /// 类型注入
        /// </summary>
        void RegisterType(Type type, Type service, IocRegisterOptions option);

        /// <summary>
        /// 程序集注入
        /// </summary>
        void RegisterAssembly(params Assembly[] assemblys);

        /// <summary>
        /// 程序集注入
        /// </summary>
        void RegisterAssembly(IocRegisterOptions option, params Assembly[] assemblys);


        /// <summary>
        /// 注册一个非参数的泛型类型，例如Repository[]。具体的类型
        /// </summary>
        void RegisterGeneric(Type type, Type service);

        /// <summary>
        /// 注册一个非参数的泛型类型，例如Repository[]。具体的类型
        /// </summary>
        void RegisterGeneric(Type type, Type service, IocRegisterOptions option);

    }
}