using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Connection;
using WebApi1.Domains;
using WebApi1.Resource;

namespace WebApi1.Engine
{
    public class EngineSettingStorage
    {
        /// <summary>
        /// 数据存储
        /// </summary>
        public RepositoryConnection Database { get; set; } = new RepositoryConnection();

        /// <summary>
        /// 默认缓存
        /// </summary>
        public ConnectionOptions Caching { get; set; } = new ConnectionOptions();

        /// <summary>
        /// 连接列表
        /// </summary>
        public List<KeyValues<string, ConnectionOptions>> Connections { get; set; } = new List<KeyValues<string, ConnectionOptions>>();
    }
}