using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Resource
{
    public static class WEBOptions
    {
        #region Global

        /// <summary>
        /// Config目录
        /// </summary>
        public static string ConfigDirectory { get; private set; } = Environment.CurrentDirectory;

        /// <summary>
        /// Global 信息配置
        /// </summary>
        /// <param name="configDirectory"></param>
        public static void Configuration(string configDirectory = null)
        {
            if (!string.IsNullOrWhiteSpace(configDirectory))
            {
                ConfigDirectory = configDirectory;
            }
        }

        #endregion

        #region Key

        /// <summary>
        /// 应用
        /// </summary>
        public const string AppCode = "appcode";

        /// <summary>
        /// Ip
        /// </summary>
        public const string ClientIp = "clientip";

        /// <summary>
        /// 语言
        /// </summary>
        public const string Culture = "culture";


        #endregion

        #region Value

        /// <summary>
        /// 倒计时秒
        /// </summary>
        public const double CountdownSecond = 15;

        /// <summary>
        /// 密码最小长度
        /// </summary>
        public const int PasswordLengthMin = 6;

        /// <summary>
        /// 密码最大长度
        /// </summary>
        public const int PasswordLengthMax = 36;


        #endregion

        #region Cache Key

        /// <summary>
        /// 缓存Key
        /// </summary>
        public const string CacheConfigKeyFormat = "_CONFIG_OBJECT_{0}";

        #endregion

        #region Request Key

        /// <summary>
        /// 来源
        /// </summary>
        public const string RequestReturnUrl = "ReturnUrl";

        #endregion

        #region Cookies key

        #endregion
    }
}