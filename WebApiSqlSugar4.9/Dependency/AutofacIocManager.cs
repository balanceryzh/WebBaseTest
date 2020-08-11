using Autofac;
using Autofac.Core;
using Autofac.Builder;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using WebApi1.Dependency.EnumBase;
using WebApi1.InterFace;
using WebApi1.Options;
using WebApi1.Resource;
using WebApi1.Utility;

namespace WebApi1.Dependency
{
    /// <summary>
    /// ICO容器管理器/(基于Autofac)
    /// </summary>
    public class AutofacIocManager : IIocManager
    {
        /// <summary>
        /// ctor static
        /// </summary>
        static AutofacIocManager()
        {
            Instance = new AutofacIocManager();
        }

        /// <summary>
        /// ctor
        /// </summary>
        public AutofacIocManager()
        {
            builder = new ContainerBuilder();
        }

        #region ICO容器管理器/属性

        /// <summary>
        /// IIocManager 管理器单例
        /// </summary>
        public static AutofacIocManager Instance { get; private set; }

        #endregion

        #region ICO容器管理器/变量

        /// <summary>
        /// autofac 容器构建者(规则)
        /// </summary>
        protected ContainerBuilder builder;

        /// <summary>
        /// autofac/ioc 容器
        /// </summary>
        protected IContainer container;

        /// <summary>
        /// autofac 构建计数器
        /// </summary>
        protected int containerBuildCount = 0;

        #endregion

        #region ICO容器管理器/注入

        /// <summary>
        /// 外部注册(传入核心对象)
        /// </summary>
        /// <param name="action"></param>
        public void Register(Action<object> action)
        {
            action(builder);
        }

        /// <summary>
        /// 实例注入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Register<T>(T obj) where T : new()
        {
            var regBuilder = builder.Register(t => obj).As<T>();
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public void RegisterType<T>()
        {
            RegisterType<T>(new IocRegisterOptions());
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public void RegisterType<T>(IocRegisterOptions option)
        {
            var regBuilder = builder.RegisterType(typeof(T));
            if (!option.IsNull())
            {
                regBuilder.RegistrationOption(option);
            }
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public void RegisterType<T, TService>() where TService : class, T
        {
            RegisterType<T, TService>(new IocRegisterOptions());
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public void RegisterType<T, TService>(IocRegisterOptions option) where TService : class, T
        {
            if (!option.IsNull() && !option.Named.IsEmpty())
            {
                builder.RegisterType<TService>().Named<T>(option.Named)
                    .RegistrationOption(option);
            }
            else
            {
                var regBuilder = builder.RegisterType<TService>().As<T>();
                if (!option.IsNull())
                {
                    regBuilder.RegistrationOption(option);
                }
            }
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public void RegisterType(Type type, Type service)
        {
            RegisterType(type, service, new IocRegisterOptions());
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public void RegisterType(Type type, Type service, IocRegisterOptions option)
        {
            if (!option.IsNull() && !option.Named.IsEmpty())
            {
                builder.RegisterType(service).Named(option.Named, type)
                    .RegistrationOption(option);
            }
            else
            {
                var regBuilder = builder.RegisterType(service).As(type);
                if (!option.IsNull())
                {
                    regBuilder.RegistrationOption(option);
                }
            }
        }


        /// <summary>
        /// 程序集注入
        /// </summary>
        public void RegisterAssembly(params Assembly[] assemblys)
        {
            RegisterAssembly(new IocRegisterOptions(), assemblys);
        }

        /// <summary>
        /// 程序集注入
        /// </summary>
        public void RegisterAssembly(IocRegisterOptions option, params Assembly[] assemblys)
        {
            builder.RegisterAssemblyTypes(assemblys)
                .RegistrationOption(option)
                   .AsImplementedInterfaces();
        }


        /// <summary>
        /// 注册一个非参数的泛型类型，例如Repository[]。具体的类型
        /// 将按要求制作，例如，在解析储存库[int]() 中。
        /// </summary>
        public void RegisterGeneric(Type type, Type service)
        {
            RegisterGeneric(type, service, new IocRegisterOptions());
        }

        /// <summary>
        /// 注册一个非参数的泛型类型，例如Repository[]。具体的类型
        /// 将按要求制作，例如，在解析储存库[int]() 中。
        /// </summary>
        public void RegisterGeneric(Type type, Type service, IocRegisterOptions option)
        {
            var regBuilder = builder.RegisterGeneric(service).As(type);
            if (!option.IsNull())
            {
                regBuilder.RegistrationOption(option);
            }
        }

        #endregion

        #region ICO容器管理器/解析

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            var result = default(T);
            result = container.Resolve<T>();
            return result;
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public T Resolve<T>(Type type)
        {
            return (T)container.Resolve(type);
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T Resolve<T>(IEnumerable<KeyValues<string, object>> parameters)
        {
            List<Parameter> paramItems = new List<Parameter>();

            if (!parameters.IsEmpty())
            {
                foreach (var item in parameters)
                {
                    paramItems.Add(new NamedParameter(item.Key, item.Value));
                }
            }
            return container.Resolve<T>(paramItems);
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T Resolve<T>(string name, IEnumerable<KeyValues<string, object>> parameters = null)
        {
            List<Parameter> paramItems = new List<Parameter>();
            if (!parameters.IsEmpty())
            {
                foreach (var item in parameters)
                {
                    paramItems.Add(new NamedParameter(item.Key, item.Value));
                }
            }
            return paramItems.IsEmpty() ? container.ResolveNamed<T>(name) : container.ResolveNamed<T>(name, paramItems);
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Resolve(Type type, object parameters)
        {
            return container.Resolve(type, GetResolveParameter(parameters));
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return GetResolveAll<T>(null);
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ResolveAll<T>(object parameters)
        {
            return GetResolveAll<T>(parameters);
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<object> ResolveAll(Type type)
        {
            return GetResolveAll<object>(type);
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<object> ResolveAll(Type type, object parameters)
        {
            return GetResolveAll<object>(type, parameters);
        }

        #endregion

        #region ICO容器管理器/操作

        /// <summary>
        /// 获取容器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetContainer<T>() where T : class
        {
            return container as T;
        }

        /// <summary>
        /// 容器构建器
        /// </summary>
        public T GetBuilder<T>() where T : class
        {
            return builder as T;
        }

        /// <summary>
        /// 容器构建
        /// </summary>
        public T ContainerBuilder<T>() where T : class
        {
            if (container.IsNull() && !builder.IsNull())
            {
                container = builder.Build();
            }
            containerBuildCount++;
            return container as T;
        }

        /// <summary>
        /// 检测类型是否注册
        /// </summary>
        public bool IsRegistered(Type type)
        {
            return false;
        }

        /// <summary>
        /// 检测类型是否注册
        /// </summary>
        public bool IsRegistered<T>()
        {
            return false;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            container.Dispose();
        }

        #endregion

        #region ICO容器管理器/辅助

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IEnumerable<Parameter> GetResolveParameter(object parameters)
        {
            return null;
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IEnumerable<T> GetResolveAll<T>(object parameters)
        {
            return GetResolveAll<T>(typeof(T), parameters);
        }

        /// <summary>
        /// 解析类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private IEnumerable<T> GetResolveAll<T>(Type service, object parameters)
        {
            // We're going to find each service which was registered
            // with a key, and for those which match the type T we'll store the key
            // and later supplement the default output with individual resolve calls to those
            // keyed services
            var allKeys = new List<object>();
            foreach (var componentRegistration in container.ComponentRegistry.Registrations)
            {
                // Get the services which match the KeyedService type
                var typedServices = componentRegistration.Services.Where(x => x is KeyedService).Cast<KeyedService>();
                // Add the key to our list so long as the registration is for the correct type T
                allKeys.AddRange(typedServices.Where(y => y.ServiceType == service).Select(x => x.ServiceKey));
            }

            // Get the default resolution output which resolves all un-keyed services
            var allUnKeyedServices = new List<T>(container.Resolve<IEnumerable<T>>());
            // Add the ones which were registered with a key
            allUnKeyedServices.AddRange(allKeys.Select(key => container.ResolveKeyed<T>(key)));

            // Return the total resultset
            return allUnKeyedServices;
        }

        #endregion
    }

    /// <summary>
    /// ICO容器管理器/(基于Autofac)扩展
    /// </summary>
    public static class RegistrationExtensions
    {
        /// <summary>
        ///IOC 注入注册配置 扩展方法
        /// </summary>
        /// <typeparam name="TLimit"></typeparam>
        /// <typeparam name="TReflectionActivatorData"></typeparam>
        /// <typeparam name="TStyle"></typeparam>
        /// <param name="registration"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IRegistrationBuilder<TLimit, TReflectionActivatorData, TStyle> RegistrationOption<TLimit, TReflectionActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TReflectionActivatorData, TStyle> registration,
            IocRegisterOptions option) where TReflectionActivatorData : ReflectionActivatorData
        {
            if (registration.IsEmpty() && option.IsEmpty())
            {
                return registration;
            }

            //参数
            if (!option.Parameters.IsEmpty())
            {
                option.Parameters.ForEach(x =>
                {
                    registration = registration.WithParameter(new NamedParameter(x.Key, x.Value));
                });
            }

            //属性
            if (!option.Properties.IsEmpty())
            {
                option.Properties.ForEach(x =>
                {
                    registration = registration.WithProperty(new NamedParameter(x.Key, x.Value));
                });
            }

            //aop
            ProxyGenerationOptions proxyOption = null;
            if (option.InterceptorProxy != null)
            {
                proxyOption = option.InterceptorProxy as ProxyGenerationOptions;
            }
            if (option.InterceptorProxy != null && !option.InterceptorTypes.IsEmpty())
            {
                registration = registration
                    .InstancePerLifetimeScope()
                    .EnableInterfaceInterceptors(proxyOption)
                    .InterceptedBy(option.InterceptorTypes);
            }

            //instance scope
            switch (option.InstanceScope)
            {
                case EnumInstanceScope.SingleInstance:
                    registration = registration.SingleInstance();
                    break;

                case EnumInstanceScope.InstancePerLifetimeScope:
                    registration = registration.InstancePerLifetimeScope();
                    break;

                case EnumInstanceScope.InstancePerRequest:
                    registration = registration.InstancePerRequest();
                    //(.net core 应用使 InstancePerLifetimeScope 而不是 InstancePerRequest)
                    break;

                case EnumInstanceScope.Default:
                case EnumInstanceScope.InstancePerDependency:
                    break;
            }

            //启用PropertiesAutowired 跟据属性注入
            registration.PropertiesAutowired();

            return registration;
        }
    }
}