using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WebApi1.EnumBase;

namespace WebApi1.Engine
{
    [Serializable]
    public class EngineConfigModel
    {
        /// <summary>
        /// App命名空间
        /// </summary>
        public string AppNamespace { get; set; }

        /// <summary>
        /// App名称
        /// </summary>
        public string AppTitle { get; set; }

        /// <summary>
        /// AppCode
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// App程序类型: Console,Service,Winfrom,Wpf,Web,ApiService,Wap
        /// </summary>
        public EnumClientType ClientType { get; set; }

        /// <summary>
        /// 调试模式: true,false
        /// </summary>
        public bool IsDebugger { get; set; }

        /// <summary>
        /// 区域信息(多个，默认第一个,以','分割)
        /// </summary>
        public string Culture { get; set; }
        /// <summary>
        /// 英文缩写
        /// </summary>
        public string EnglishAbb { get; set; }
        /// <summary>
        /// 中文缩写
        /// </summary>
        public string ChineseAbb { get; set; }

        /// <summary>
        /// 翻译格式
        /// </summary>
        public string Supportedformats { get; set; }

        /// <summary>
        /// 托管/宿主 地址: http://+:8100 
        /// </summary>
        public string Hosting { get; set; }

        /// <summary>
        /// bind/module 等类库文件路径
        /// </summary>
        public string BinPath { get; set; }

        /// <summary>
        /// 配置文件路径: /config
        /// </summary>
        public string ConfigPath { get; set; }

        /// <summary>
        /// 外部工具路径: /tools
        /// </summary>
        public string ToolsPath { get; set; }
        /// <summary>
        /// 文件内容解析工具
        /// </summary>
        public string ParserFlowPath { get; set; }

        /// <summary>
        /// 存储配置
        /// </summary>
        public EngineSettingStorage Storage { get; set; } = new EngineSettingStorage();

        /// <summary>
        /// 资源配置
        /// </summary>
        public EngineSettingAssets Assets { get; set; } = new EngineSettingAssets();

        /// <summary>
        /// 认证授权
        /// </summary>
        public EngineSettingAuthority Authority { get; set; } = new EngineSettingAuthority();

        /// <summary>
        /// Setting (Key-Value) 集合
        /// </summary>
        [XmlArray("Setting")]
        [XmlArrayItem("Item")]
        public EngineSettingKeyValues Setting { get; set; } = new EngineSettingKeyValues();
    }
}