using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Resource
{
    public class FileTypeList
    {
        public List<DirectoryFiles> NoLinkFiles { get; set; }

        public List<LinkFile> LinkFiles { get; set; }

    }
    public class DirectoryFiles
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 是否目录
        /// </summary>
        public bool IsDirectory { get; set; }
        /// <summary>
        /// 是否已匹配
        /// </summary>
        public bool IsMarry { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        public string Md5 { get; set; }

        public string Extension { get; set; }


    }

    public class LinkFile
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string ZSName { get; set; }
        public string ZSMD5 { get; set; }
        /// <summary>
        /// 完整路径
        /// </summary>
        public string ZSFullName { get; set; }
        /// <summary>
        /// 中文文件扩展名
        /// </summary>
        public string ZSExtension { get; set; }

        public int ZSWordCount { get; set; }

        public int ZSRowCount { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ENName { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        public string ENFullName { get; set; }
        /// <summary>
        /// 英文文件扩展名
        /// </summary>
        public string ENExtension { get; set; }


        public int ENWordCount { get; set; }
        public int ENRowCount { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long ZSSize { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long ENSize { get; set; }
        public string ENMD5 { get; set; }

    }
}