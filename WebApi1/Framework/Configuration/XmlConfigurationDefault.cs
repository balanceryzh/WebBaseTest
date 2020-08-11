using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApi1.Culture;
using WebApi1.EnumBase;
using WebApi1.Resource;
using WebApi1.Utility;

namespace WebApi1.Framework
{
    public class XmlConfigurationDefault<TEntity> : IXmlConfiguration<TEntity> where TEntity : class, new()
    {
        static object _lockObj = new object();
        static readonly Lazy<TEntity> _lazy = new Lazy<TEntity>(() =>
        {
            return new XmlConfigurationDefault<TEntity>().Load();
        });

        DateTime _lastDt { get; set; }

        /// <summary>
        /// 配置对象
        /// </summary>
        public static TEntity Instance => _lazy.Value;

        /// <summary>
        /// 文件名称
        /// </summary>
        public virtual string FileName
        {
            get
            {
                if (_fileName.IsEmpty())
                {
                    var type = typeof(TEntity);
                    _fileName = string.Format("{0}.config", type.Name.Replace("ConfigModel", ""));
                }
                return _fileName;
            }
        }
        private string _fileName;

        /// <summary>
        /// 文件路径
        /// </summary>
        public virtual string FilePath
        {
            get
            {
                if (_filePath.IsEmpty())
                {
                    _filePath = FilesHelper.GetFilePath(Path.Combine(WEBOptions.ConfigDirectory, FileName));
                }
                return _filePath;
            }
        }
        private string _filePath;

        /// <summary>
        /// 缓存Key
        /// </summary>
        public virtual string CacheKey
        {
            get
            {
                return string.Format(WEBOptions.CacheConfigKeyFormat, FileName.ToLower());
            }
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        public TEntity Load(bool checkTime = true)
        {
            lock (_lockObj)
            {
                TEntity item = MemoryCacheHelper.Get<TEntity>(CacheKey);
                if (item == null)
                {
                    if (!File.Exists(FilePath))
                    {
                        throw new CodeException(EnumCode.路径错误, $"{LANG.PeiZhiWenJian}({FilePath})".NotFoundTip());
                    }
                    item = XmlsHelper.Load(typeof(TEntity), FilePath) as TEntity;
                    SetCache(item);
                }
                return item;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save(TEntity entity)
        {
            if (XmlsHelper.Save(entity, FilePath))
            {
                SetCache(entity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 设置文件缓存(文件依赖)
        /// </summary>
        /// <param name="entity"></param>
        private void SetCache(TEntity entity)
        {
            MemoryCacheHelper.Set<TEntity>(CacheKey, entity, new List<string>() { FilePath });
        }
    }
}