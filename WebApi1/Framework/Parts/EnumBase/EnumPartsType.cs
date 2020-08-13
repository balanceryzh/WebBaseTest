using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Framework
{
    public enum EnumPartsType
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,
        运行 = 1,
        模块 = 2,
        组件 = 4,
        插件 = 8
    }
}