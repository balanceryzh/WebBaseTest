using AutoMapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using WebApi1.Dependency;
using WebApi1.EnumBase;
using WebApi1.Framework;
using WebApi1.InterFace;
using WebApi1.Options;
using WebApi1.Resource;

namespace WebApi1.Engine
{
    public static class EngineHelper2
    {

        static readonly Lazy<IIocManager> _iocManagerLazy = new Lazy<IIocManager>(() => new AutofacIocManager());
        /// <summary>
        /// IOC 容器
        /// </summary>
        public static IIocManager IocManager { get; } = _iocManagerLazy.Value;
        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return IocManager.Resolve<T>();
        }
    }

    public static class EngineHelper
    {
        /// <summary>
        /// static ctor
        /// </summary>
        static EngineHelper()
        {
            //对象验证
            FluentValidationOptions.Configuration();
            RegisterType(typeof(IValidatorFactory), typeof(FluentValidatorDefaultFactory));

            //缓存服务
            var cachingRedisIoc = new IocRegisterOptions();
            cachingRedisIoc.AppendParams("connection", Configuration.Storage.Caching);
            RegisterType<ICaching, RedisCaching>(cachingRedisIoc);
            RegisterType<IAppCaching, RedisCaching>(cachingRedisIoc);
            RegisterType<ISessionCaching, RedisCaching>(cachingRedisIoc);
            RegisterType<IDataCaching, RedisCaching>(cachingRedisIoc);

            //配置文件
            RegisterGeneric(typeof(IXmlConfiguration<>), typeof(XmlConfigurationDefault<>),
                new IocRegisterOptions() { InstanceScope = EnumInstanceScope.SingleInstance });

            //Aop(执行日志/输入校验)
            //RegisterType<IApplicationLoggingInterceptor, ApplicationLoggingInterceptor>();
            //RegisterType<IInputValidatorInterceptor, InputValidatorInterceptor>();

            //accessor、session、token、auth
            RegisterType<IAccessor, AccessorDefault>();
            RegisterType<ISession, SessionDefault>();
            RegisterType<IAuthorize, AuthorizeDefault>();
            RegisterType<ITokenProvider, TokenProviderDefault>();

            //domains
            RegisterType<IInputValidatorInterceptor, InputValidatorInterceptor>();
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        public static EngineConfigModel Configuration
        {
            get
            {
                UTHOptions.Configuration(EngineXmlConfiguration.Instance.ConfigPath);
                return EngineXmlConfiguration.Instance;
            }
        }

        #region 宿主启动

        static readonly Lazy<ILauncher> _launcherLazy = new Lazy<ILauncher>(() => new LauncherDefault());

        /// <summary>
        /// 启动器
        /// </summary>
        public static ILauncher Launcher { get; } = _launcherLazy.Value;

        #endregion

        #region 类型查找

        static readonly Lazy<ITypeFinder> _typeFinderLazy = new Lazy<ITypeFinder>(() =>
        {
            ITypeFinder _instance = null;

            switch (Configuration.ClientType)
            {
                case EnumClientType.Console:
                case EnumClientType.Server:
                case EnumClientType.Winfrom:
                case EnumClientType.Wpf:
                    _instance = new AppDomainTypeFinder();
                    break;
                case EnumClientType.Website:
                case EnumClientType.Api:
                case EnumClientType.Wap:
                    _instance = new WebAppTypeFinder();
                    break;
                default:
                    _instance = new AppDomainTypeFinder();
                    break;
            }

            return _instance;
        });

        /// <summary>
        /// 类型查找器
        /// </summary>
        public static ITypeFinder TypeFinder => _typeFinderLazy.Value;

        #endregion

        #region IOC 容器

        static readonly Lazy<IIocManager> _iocManagerLazy = new Lazy<IIocManager>(() => new AutofacIocManager());

        /// <summary>
        /// IOC 容器
        /// </summary>
        public static IIocManager IocManager { get; } = _iocManagerLazy.Value;

        #region 注册操作

        /// <summary>
        /// 外部注册(传入核心对象)
        /// </summary>
        /// <param name="action"></param>
        public static void Register(Action<object> action)
        {
            IocManager.Register(action);
        }

        /// <summary>
        /// 实例注入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void Register<T>(T obj) where T : new()
        {
            IocManager.Register<T>(obj);
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public static void RegisterType<T>()
        {
            IocManager.RegisterType<T>();
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public static void RegisterType<T>(IocRegisterOptions option)
        {
            IocManager.RegisterType<T>(option);
        }

        /// <summary>
        /// 类型服务注入
        /// </summary>
        public static void RegisterType<T, TService>() where TService : class, T
        {
            IocManager.RegisterType<T, TService>();
        }

        /// <summary>
        /// 类型服务注入
        /// </summary>
        public static void RegisterType<T, TService>(IocRegisterOptions option) where TService : class, T
        {
            IocManager.RegisterType<T, TService>(option);
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public static void RegisterType(Type type, Type service)
        {
            IocManager.RegisterType(type, service);
        }

        /// <summary>
        /// 类型注入
        /// </summary>
        public static void RegisterType(Type type, Type service, IocRegisterOptions option)
        {
            IocManager.RegisterType(type, service, option);
        }


        /// <summary>
        /// 程序集注入
        /// </summary>
        public static void RegisterAssembly(Assembly assembly)
        {
            IocManager.RegisterAssembly(assembly);
        }

        /// <summary>
        /// 程序集注入
        /// </summary>
        public static void RegisterAssembly(Assembly assembly, IocRegisterOptions option)
        {
            IocManager.RegisterAssembly(option, assembly);
        }


        /// <summary>
        /// 注册一个非参数的泛型类型，例如Repository[]。具体的类型
        /// </summary>
        public static void RegisterGeneric(Type type, Type service)
        {
            IocManager.RegisterGeneric(type, service);
        }

        /// <summary>
        /// 注册一个非参数的泛型类型，例如Repository[]。具体的类型
        /// </summary>
        public static void RegisterGeneric(Type type, Type service, IocRegisterOptions option)
        {
            IocManager.RegisterGeneric(type, service, option);
        }


        #endregion

        #region 解析操作

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return IocManager.Resolve<T>();
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T Resolve<T>(Type type)
        {
            return (T)IocManager.Resolve(type);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T Resolve<T>(params KeyValues<string, object>[] parameters)
        {
            return IocManager.Resolve<T>(parameters);
        }


        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T Resolve<T>(string name, params KeyValues<string, object>[] parameters)
        {
            return IocManager.Resolve<T>(name, parameters);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Resolve(Type type)
        {
            return IocManager.Resolve(type);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object Resolve(Type type, object parameters)
        {
            return IocManager.Resolve(type, parameters);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ResolveAll<T>()
        {
            return IocManager.ResolveAll<T>();
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<T> ResolveAll<T>(object parameters)
        {
            return IocManager.ResolveAll<T>(parameters);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<object> ResolveAll(Type type)
        {
            return IocManager.ResolveAll(type);
        }

        /// <summary>
        /// 类型解析
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable<object> ResolveAll(Type type, object parameters)
        {
            return IocManager.ResolveAll(type, parameters);
        }

        #endregion

        /// <summary>
        /// 获取容器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetContainer<T>() where T : class
        {
            return IocManager.GetContainer<T>();
        }

        /// <summary>
        /// 容器构建器
        /// </summary>
        public static T GetBuilder<T>() where T : class
        {
            return IocManager.GetBuilder<T>();
        }

        /// <summary>
        /// 容器构建
        /// </summary>
        public static T ContainerBuilder<T>() where T : class
        {
            return IocManager.ContainerBuilder<T>();
        }

        #endregion

        #region 日志记录

        /// <summary>
        /// 日志组件
        /// </summary>
        /// <param name="msessage"></param>
        public static ILog GetLogger(string logger)
        {
            return LoggingManager.GetLogger(logger);
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="msessage"></param>
        public static void Log(string msessage, string logger = "Info")
        {
            LoggingManager.GetLogger(logger).Info(msessage);
        }

        /// <summary>
        /// 业务日志
        /// </summary>
        public static void Service(string message)
        {
            LoggingManager.GetLogger("Service").Info(message);
        }

        /// <summary>
        /// 信息日志
        /// </summary>
        public static void Info(string message)
        {
            LoggingManager.GetLogger("Info").Info(message);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        public static void Error(string message)
        {
            LoggingManager.GetLogger("Error").Error(message);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        public static void Error<T>(T obj)
        {
            if (!obj.IsNull())
            {
                LoggingManager.GetLogger("Error").Error<T>(obj);
            }
        }

        #endregion

        #region 对像映射

        /// <summary>
        /// Mapper 映射初始
        /// </summary>
        /// <param name="action"></param>
        public static void MapInitialize(Action<IMapperConfigurationExpression> action = null)
        {
            Mapper.Initialize(cfg =>
            {
                action?.Invoke(cfg);
            });
            //Mapper.AssertConfigurationIsValid();
        }
        
        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TEntity Map<TEntity>(object source)
        {
            Mapper mapper = new Mapper();
            return mapper.Map<TEntity>(source);
        }

        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        #endregion

        #region 领域管理

        static readonly Lazy<DomainProfileManager> _domainProfileManagerLazy = new Lazy<DomainProfileManager>(() => new DomainProfileManager());

        /// <summary>
        /// 领域配置文件列表
        /// </summary>
        public static DomainProfileManager Domains { get { return _domainProfileManagerLazy.Value; } }

        #endregion

        #region 部件管理

        static readonly Lazy<PartsManager> _partsManagerLazy = new Lazy<PartsManager>(() => new PartsManager());

        /// <summary>
        /// 部件列表
        /// </summary>
        public static PartsManager Parts { get { return _partsManagerLazy.Value; } }

        #endregion
    }
}