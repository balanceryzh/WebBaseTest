using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Engine
{
    public class EngineSettingAssets
    {
        /// <summary>
        /// 服务名称(默认本地)
        /// </summary>
        public string Name { get; set; } = "Local";

        /// <summary>
        /// 临时目录
        /// </summary>
        public string TempPath { get; set; } = "temp";

        /// <summary>
        /// 上传路径
        /// </summary>
        public string UploadPath { get; set; } = "upload";

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }
    }
}