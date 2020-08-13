using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi1.Framework
{
    /// <summary>
    /// 领域信息配置接口
    /// （领域配置单独提出，不依赖模块配置，引入UTH.Module就可以使）
    /// eg:DTO的注入配置
    /// </summary>
    public interface IDomainProfile
    {
        /// <summary>
        /// 配置载入
        /// </summary>
        void Install();
    }
}
