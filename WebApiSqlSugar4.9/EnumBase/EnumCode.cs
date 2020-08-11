using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.EnumBase
{
    public enum EnumCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        成功 = 0,
        /// <summary>
        /// 错误
        /// </summary>
        错误 = 1,
        /// <summary>
        /// 提示
        /// </summary>
        提示 = 2,
        /// <summary>
        /// 跳转
        /// </summary>
        跳转 = 3,

        #region 11 系统程序

        /// <summary>
        /// 系统框架
        /// </summary>
        系统框架 = 1100,
        /// <summary>
        /// 初始错误
        /// </summary>
        初始错误 = 1102,
        /// <summary>
        /// 设备错误
        /// </summary>
        设备错误 = 1103,
        /// <summary>
        /// 服务异常
        /// </summary>
        服务错误 = 1104,
        /// <summary>
        /// 线程错误
        /// </summary>
        线程错误 = 1105,

        #endregion

        #region 12 文件配置

        /// <summary>
        /// 文件配置
        /// </summary>
        文件配置 = 1200,
        /// <summary>
        /// 路径错误
        /// </summary>
        路径错误 = 1201,
        /// <summary>
        /// 读取错误
        /// </summary>
        读取错误 = 1202,

        #endregion

        #region 13 网络请求

        /// <summary>
        /// 网络请求
        /// </summary>
        网络请求 = 1300,
        /// <summary>
        /// 请求超时
        /// </summary>
        请求失败 = 1301,
        /// <summary>
        /// 请求超时
        /// </summary>
        请求超时 = 1302,
        /// <summary>
        /// 请求跳转
        /// </summary>
        请求跳转 = 1303,

        /// <summary>
        /// 调用外部平台出错
        /// </summary>
        网络调用 = 1310,

        #endregion

        #region 14 认证授权

        /// <summary>
        /// 认证授权
        /// </summary>
        认证授权 = 1400,
        /// <summary>
        /// 认证失败
        /// </summary>
        认证失败 = 1401,
        /// <summary>
        /// 认证过期
        /// </summary>
        认证过期 = 1402,
        /// <summary>
        /// 认证刷新
        /// </summary>
        认证刷新 = 1403,
        /// <summary>
        /// 授权失败
        /// </summary>
        授权失败 = 1411,
        /// <summary>
        /// 暂无权限
        /// </summary>
        暂无权限 = 1412,
        /// <summary>
        /// 授权文件
        /// </summary>
        授权文件 = 1413,

        #endregion

        #region 21 数据缓存

        /// <summary>
        /// 数据缓存
        /// </summary>
        数据缓存 = 2100,
        /// <summary>
        /// 连接错误
        /// </summary>
        连接错误 = 2101,
        /// <summary>
        /// 执行错误
        /// </summary>
        执行错误 = 2102,
        /// <summary>
        /// 查询错误
        /// </summary>
        查询错误 = 2103,
        /// <summary>
        /// 事务错误
        /// </summary>
        事务错误 = 2104,

        #endregion


        #region 31 支付交易

        /// <summary>
        /// 支付交易
        /// </summary>
        支付交易 = 3100,

        /// <summary>
        /// 在线支付
        /// </summary>
        在线支付 = 3101,

        /// <summary>
        /// 支付通知
        /// </summary>
        支付通知 = 3102,

        /// <summary>
        /// 支付重复
        /// </summary>
        支付重复 = 3103,

        #endregion


        #region 51 核心模块

        #endregion

        #region 52 基础模块

        #endregion

        #region 53 用户模块

        #endregion

        #region 54 AI 模块

        #endregion

        #region 55 业务模块

        订单异常 = 5501,
        进度异常 = 5502

        #endregion

    }
}