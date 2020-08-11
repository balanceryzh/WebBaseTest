using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.InterFace
{
    /// <summary>
    /// ICO管理器
    /// </summary>
    public interface IIocManager : IIocRegister, IIocResolver, IDisposable
    {
        /// <summary>
        /// 获取容器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetContainer<T>() where T : class;

        /// <summary>
        /// 获取容器构建器
        /// </summary>
        T GetBuilder<T>() where T : class;

        /// <summary>
        /// 容器构建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T ContainerBuilder<T>() where T : class;

        /// <summary>
        /// 检测类型是否注册
        /// </summary>
        bool IsRegistered(Type type);

        /// <summary>
        /// 检测类型是否注册
        /// </summary>
        bool IsRegistered<T>();
    }
}