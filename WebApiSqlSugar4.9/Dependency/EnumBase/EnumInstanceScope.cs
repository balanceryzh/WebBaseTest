using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Dependency.EnumBase
{

    /// <summary>
    /// 实例范围
    /// </summary>
    public enum EnumInstanceScope
    {
        //doc:http://docs.autofac.org/en/latest/lifetime/instance-scope.html
        //doc: http://docs.autofac.org/en/latest/integration/aspnetcore.html

        /// <summary>
        /// 默认(使用InstancePerDependency)
        /// </summary>
        Default,
        /// <summary>
        /// 实例/依赖
        /// </summary>
        InstancePerDependency,
        /// <summary>
        /// 单一实例
        /// </summary>
        SingleInstance,
        /// <summary>
        /// 每个生命周期范围的实例
        /// </summary>
        InstancePerLifetimeScope,
        /// <summary>
        /// 每个匹配生命周期范围的实例
        /// </summary>
        InstancePerMatchingLifetimeScope,
        /// <summary>
        /// 每个请求实例
        /// </summary>
        InstancePerRequest,
        /// <summary>
        /// 每个拥有实例
        /// </summary>
        InstancePerOwned,
        /// <summary>
        /// 线程范围
        /// </summary>
        ThreadScope,
    }
}