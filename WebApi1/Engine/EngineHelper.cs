using System;
using WebApi1.Dependency;
using WebApi1.InterFace;

namespace WebApi1.Engine
{
    public static class EngineHelper
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
}