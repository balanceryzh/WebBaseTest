using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WebApi1.Utility;

namespace WebApi1.Engine
{
    /// <summary>
    ///  KeyValue 集合
    /// </summary>
    public class EngineSettingKeyValues : List<EngineSettingKeyValue>
    {
    }

    /// <summary>
    /// Key-Value
    /// </summary>
    public class EngineSettingKeyValue
    {
        /// <summary>
        /// key
        /// </summary>
        [XmlAttribute("Key")]
        public string Key { get; set; }

        /// <summary>
        /// value
        /// </summary>
        [XmlAttribute("Value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// EngineSettingKeyValues操作扩展
    /// </summary>
    public static class EngineSettingKeyValuesExtension
    {
        /// <summary>
        /// 获取配置扩展
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(this EngineSettingKeyValues config, string key)
        {
            if (config.IsNull())
                return string.Empty;
            var item = config.Where(x => x.Key == key).FirstOrDefault();
            return item?.Value;
        }
    }
}