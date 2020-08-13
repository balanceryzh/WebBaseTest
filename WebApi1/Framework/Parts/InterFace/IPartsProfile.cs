using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebApi1.Framework
{
    public interface IPartsProfile
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// 配置标识
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 显示名称
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 类型
        /// </summary>
        EnumPartsType PartsType { get; }

        /// <summary>
        /// 是否动态化
        /// </summary>
        bool IsDynamic { get; }

        /// <summary>
        /// 程序集
        /// </summary>
        Assembly Assemblies { get; }

        /// <summary>
        ///  配置载入
        /// </summary>
        void Install();

        /// <summary>
        /// 配置卸载
        /// </summary>
        void Uninstall();

        /// <summary>
        /// 序号
        /// </summary>
        int Order { get; set; }

    }
}
