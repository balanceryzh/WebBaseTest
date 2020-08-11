using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Utility
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public static class EnumsHelper
    {
        #region 属性变量

        #endregion

        #region 枚举获取

        /// <summary>
        /// 获取枚举项
        /// </summary>
        public static TEntity Get<TEntity>(int value)
        {
            TEntity item = (TEntity)Enum.Parse(typeof(TEntity), value.ToString());
            return item == null ? default(TEntity) : item;
        }

        /// <summary>
        /// 获取枚举项
        /// </summary>
        public static TEntity Get<TEntity>(string value)
        {
            TEntity item = default(TEntity);
            if (!string.IsNullOrWhiteSpace(value))
            {
                item = (TEntity)Enum.Parse(typeof(TEntity), value.Trim());
            }
            return item == null ? default(TEntity) : item;
        }

        /// <summary>
        /// 获取枚举项
        /// </summary>
        public static TEntity Get<TEntity>(object obj)
        {
            TEntity item = default(TEntity);
            if (obj != null)
            {
                item = (TEntity)Enum.Parse(typeof(TEntity), obj.ToString());
            }
            return item == null ? default(TEntity) : item;
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// 获取枚举值
        /// </summary>
        public static int GetValue(this Enum value, int? defaultValue = null)
        {
            return (int)Enum.Parse(value.GetType(), value.ToString());
        }

        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetName(this Enum value)
        {
            return value.ToString();
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescript(this Enum value)
        {
            string str = value.ToString();
            System.Reflection.FieldInfo field = value.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 获取枚举字典
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="isDescript">文本是否为描述</param>
        /// <returns>Dictionary 集合</returns>
        public static Dictionary<int, string> GetToDictionary(Type enumType, bool isDescript = false)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("传入的参数必须是枚举类型！", "enumType");
            }
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (Enum item in Enum.GetValues(enumType))
            {
                dic.Add(Convert.ToInt32(item), isDescript ? item.GetDescript() : item.GetName());
            }
            return dic;
        }

        #endregion
    }
}