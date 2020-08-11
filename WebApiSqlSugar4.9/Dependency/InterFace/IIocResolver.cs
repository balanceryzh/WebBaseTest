using System;
using System.Collections.Generic;
using WebApi1.Resource;

namespace WebApi1.InterFace
{
    public interface IIocResolver
    {
        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Resolve<T>(IEnumerable<KeyValues<string, object>> parameters);

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Resolve<T>(string name, IEnumerable<KeyValues<string, object>> parameters = null);

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object Resolve(Type type, object parameters);

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> ResolveAll<T>(object parameters);

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<object> ResolveAll(Type type);

        /// <summary>
        /// 解析对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<object> ResolveAll(Type type, object parameters);

    }
}