using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Culture
{
    public struct Language
    {
        /// <summary>
        /// 语言编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string Culture { get; set; }

        /// <summary>
        /// 本地名称
        /// </summary>
        public string Native { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string English { get; set; }
    }
}