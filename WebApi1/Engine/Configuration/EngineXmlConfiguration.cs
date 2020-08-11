using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Framework;

namespace WebApi1.Engine
{
    /// <summary>
    /// 引擎配置服务
    /// </summary>
    public class EngineXmlConfiguration : XmlConfigurationDefault<EngineConfigModel>, IEngineXmlConfiguration
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public override string FileName
        {
            get
            {
                return "Engine.config";
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public override string FilePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
    }
}