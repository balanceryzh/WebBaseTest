using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Utility
{
    public class MemoryCacheHelper
    {
        static MemoryCache objectCache = new MemoryCache(new MemoryCacheOptions());     //缓存对象
        static readonly object locker = new object();                                   //锁对象

        /// <summary>
        /// 获取缓存(根据缓存Key)
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns>返回object</returns>
        public static object Get(string key)
        {
            object val = null;
            if (key != null && objectCache.TryGetValue(key, out val))
            {
                return val;
            }
            return val;
        }

        /// <summary>
        /// 获取缓存(根据缓存Key)
        /// </summary>
        /// <typeparam name="TEntity">返回的类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns>返回的类型对象</returns>
        public static TEntity Get<TEntity>(string key)
        {
            TEntity val = default(TEntity);
            if (!string.IsNullOrWhiteSpace(key))
            {
                objectCache.TryGetValue<TEntity>(key, out val);
            }
            return val;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="obj">内容对象</param>
        public static void Set(string key, object obj)
        {
            SetCache<object>(key, obj);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="entity">内容对象</param>
        public static void Set<TEntity>(string key, TEntity entity)
        {
            SetCache<TEntity>(key, entity);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="obj">内容对象</param>
        /// <param name="expirationDateTime">过期时间(绝对即：指定在XXX时候过期)</param>
        public static void Set(string key, object obj, DateTime expirationDateTime)
        {
            SetCache<object>(key, obj, absoluteExpiration: expirationDateTime);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="entity">内容对象</param>
        /// <param name="expirationDateTime">过期时间(绝对即：指定在XXX时候过期)</param>
        public static void Set<TEntity>(string key, TEntity entity, DateTime expirationDateTime)
        {
            SetCache<TEntity>(key, entity, absoluteExpiration: expirationDateTime);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="obj">内容对象</param>
        /// <param name="slidingExpirationTimeSpan">过期时间(相对即：多少时间内未使用过期)</param>
        public static void Set(string key, object obj, TimeSpan slidingExpirationTimeSpan)
        {
            SetCache<object>(key, obj, slidingExpiration: slidingExpirationTimeSpan);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="entity">内容对象</param>
        /// <param name="slidingExpirationTimeSpan">过期时间(相对即：多少时间内未使用过期)</param>
        public static void Set<TEntity>(string key, TEntity entity, TimeSpan slidingExpirationTimeSpan)
        {
            SetCache<TEntity>(key, entity, slidingExpiration: slidingExpirationTimeSpan);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="obj">内容对象</param>
        /// <param name="filePaths">文件依赖</param>
        public static void Set(string key, object obj, List<string> filePaths)
        {
            SetCache<object>(key, obj, filePaths: filePaths);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键Key</param>
        /// <param name="entity">内容对象</param>
        /// <param name="filePaths">文件依赖</param>
        public static void Set<TEntity>(string key, TEntity entity, List<string> filePaths)
        {
            SetCache<TEntity>(key, entity, filePaths: filePaths);
        }


        /// <summary>
        /// 移除缓存(根据缓存Key)
        /// </summary>
        /// <param name="key">键Key</param>
        public static void Remove(string key)
        {
            objectCache.Remove(key);
        }

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public static void Clear()
        {
            ////foreach (var item in objectCache)
            ////{
            ////    objectCache.Remove(item.Key);
            ////}
        }

        /// <summary>
        /// 设置缓存(absoluteExpiration:过期时间; slidingExpiration:给定活动时间，该时间内未活动，则过期; filesPath:文件依赖)
        /// </summary>
        /// <typeparam name="TEntity">缓存对象类型</typeparam>
        /// <param name="key">根据缓存Key</param>
        /// <param name="obj">缓存内容</param>
        /// <param name="absoluteExpiration">绝对(指定时间)过期</param>
        /// <param name="slidingExpiration">相对(使用间隔)过期</param>
        /// <param name="filePaths">文件依赖</param>
        private static void SetCache<TEntity>(string key, TEntity obj, DateTime? absoluteExpiration = null, TimeSpan? slidingExpiration = null, List<string> filePaths = null)
        {
            var option = new MemoryCacheEntryOptions();
            if (absoluteExpiration != null && absoluteExpiration.HasValue)
            {
                option.AbsoluteExpiration = absoluteExpiration;
            }
            if (slidingExpiration != null && slidingExpiration.HasValue)
            {
                option.SlidingExpiration = slidingExpiration;
            }
            var result = objectCache.Set<TEntity>(key, obj);
        }
    }
}