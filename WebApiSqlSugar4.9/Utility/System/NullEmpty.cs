using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebApi1.Utility;

namespace WebApi1.Utility
{
    /// <summary>
    /// 信息校验辅助类
    /// </summary>
    public static class NullEmpty
    {
        /// <summary>
        /// 判断对象是否为Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 判断对象是否为Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNuLL(this JObject obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 判断对象是否为Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNuLL(this JArray obj)
        {
            return obj == null;
        }

        /// <summary>
        /// CheckNull
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public static void CheckNull(this object obj, string name)
        {
            if (obj == null)
                throw new ArgumentNullException(name, name.CheckNullEmptyTip());
        }
        public static string CheckNullEmptyTip(this string str)
        {
            return "错误";
        }

        /// <summary>
        /// 判断对象是否Null或空(string null/"",list null/0,arr null/0)
        /// </summary>
        public static bool IsEmpty(this object obj)
        {
            if (obj == null)
                return true;

            string objType = obj.GetType().FullName;

            if (objType == "System.String" || objType == "Microsoft.Extensions.Primitives.StringValues")
            {
                return string.IsNullOrWhiteSpace(obj.ToString()) ? true : false;
            }
            else if (objType == "System.DateTime")
            {
                DateTime value = DateTime.MinValue;
                DateTime.TryParse(obj.ToString(), out value);
                return value == DateTime.MinValue;
            }
            else if (objType == "System.Guid")
            {
                Guid value = Guid.Empty;
                Guid.TryParse(obj.ToString(), out value);
                return value == Guid.Empty;
            }
            else if (objType == "System.Int32")
            {
                int value = 0;
                int.TryParse(obj.ToString(), out value);
                return value == 0;
            }
            else if (objType == "Newtonsoft.Json.Linq.JValue")
            {
                return (obj as JArray).Count == 0;
            }
            else if (objType == "Newtonsoft.Json.Linq.JObject")
            {
                return (obj as JObject).Properties().Count() == 0;
            }
            else if (obj is ICollection && (obj as ICollection).Count == 0)
            {
                return true;
            }
            //0001/1/1 0:00:00

            //System.Web.HttpValueCollection(NameValueCollection)
            //Newtonsoft.Json.Linq.JArray
            return string.IsNullOrWhiteSpace(obj.ToString());
        }

        /// <summary>
        /// 判断是否空字符串
        /// </summary>
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 判断是否空Int（NULL 0）
        /// </summary>
        public static bool IsEmpty(this int value)
        {
            return value == 0;
        }

        /// <summary>
        /// 判断是否空Decimal（NULL 0）
        /// </summary>
        public static bool IsEmpty(this decimal value)
        {
            return value == 0;
        }

        /// <summary>
        /// 判断是否空Long（NULL 0）
        /// </summary>
        public static bool IsEmpty(this long value)
        {
            return value == 0;
        }

        /// <summary>
        /// 判断是否空字符串
        /// </summary>
        public static bool IsEmpty(this StringBuilder build)
        {
            return build == null || (build != null && build.Length == 0);
        }

        /// <summary>
        /// 判断是否空Guid（NULL Empty）
        /// </summary>
        public static bool IsEmpty(this Guid guid)
        {
            return guid == null || guid == Guid.Empty;
        }

        /// <summary>
        /// 判断是否空Guid（NULL Empty）
        /// </summary>
        public static bool IsEmpty(this Guid? guid)
        {
            return guid == null || guid == Guid.Empty || !guid.HasValue;
        }

        /// <summary>
        /// 判断数组是否为空
        /// </summary>
        public static bool IsEmpty(this Array array)
        {
            //ICollection, IEnumerable, IList, IStructuralComparable, IStructuralEquatable, ICloneable
            return !(array != null && array.Length > 0);
        }

        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        public static bool IsEmpty(this ICollection collection)
        {
            return !(collection != null && collection.Count > 0);
        }

        /// <summary>
        /// 判断是否空DateTime（NULL Empty）
        /// </summary>
        public static bool IsEmpty(this DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return true;
            }
            if (date == DateTimeHelper.DefaultDateTime)
            {
                return true;
            }
            if (date == DateTime.Parse("1900-01-01 00:00"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// CheckEmpty
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tip"></param>
        public static void CheckEmpty(this string str, string name)
        {
            if (str.IsEmpty())
                throw new ArgumentNullException(name, name.CheckNullEmptyTip());
        }

        /// <summary>
        /// CheckEmpty
        /// </summary>
        /// <param name="build"></param>
        /// <param name="tip"></param>
        public static void CheckEmpty(this StringBuilder build, string name)
        {
            if (build.IsEmpty())
                throw new ArgumentNullException(name, name.CheckNullEmptyTip());
        }

        /// <summary>
        /// CheckEmpty
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="name"></param>
        public static void CheckEmpty(this Guid guid, string name)
        {
            if (guid.IsEmpty())
                throw new ArgumentNullException(name, name.CheckNullEmptyTip());
        }

        /// <summary>
        /// CheckEmpty
        /// </summary>
        /// <param name="array"></param>
        /// <param name="name"></param>
        public static void CheckEmpty(this Array array, string name)
        {
            if (array.IsEmpty())
                throw new ArgumentNullException(name, name.CheckNullEmptyTip());
        }

        /// <summary>
        /// CheckEmpty
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="name"></param>
        public static void CheckEmpty(this ICollection collection, string name)
        {
            if (collection.IsEmpty())
                throw new ArgumentNullException(name, name.CheckNullEmptyTip());
        }
    }
}