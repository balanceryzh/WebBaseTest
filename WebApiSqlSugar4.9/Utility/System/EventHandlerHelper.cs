using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Utility
{
    public static class EventHandlerHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        /// <summary>
        /// 事件调用
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender)
        {
            eventHandler.InvokeSafely(sender, EventArgs.Empty);
        }

        /// <summary>
        /// 事件调用
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }

        /// <summary>
        /// 事件调用
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void InvokeSafely<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        #endregion


    }
}