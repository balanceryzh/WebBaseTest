using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApi1.Utility
{
    /// <summary>
    /// 时间辅助类
    /// </summary>
    public static class DateTimeHelper
    {
        #region 属性变量

        /// <summary>
        /// 默认时间
        ///</summary>
        public static DateTime DefaultDateTime
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 默认时间
        ///</summary>
        public static DateTime DefaultDateTime2
        {
            get
            {
                return DateTime.Parse("1900-01-01 00:00");
            }
        }

        /// <summary>
        /// 最小时间
        /// </summary>
        public static DateTime MinDateTime
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        #endregion

        #region 获取内容

        /// <summary>
        /// 获取对象时间值(出错：返回默认DefaultValue)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue"></param>
        /// <returns>返回DateTime对象</returns>
        public static DateTime Get(object obj, DateTime? defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = DefaultDateTime;

            DateTime returnValue = defaultValue.Value;
            if (obj != null)
            {
                DateTime.TryParse(obj.ToString(), out returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// 获取字符串时间值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="defaultValue"></param>
        /// <returns>返回DateTime对象</returns>
        public static DateTime Get(string str, DateTime? defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = DefaultDateTime;

            DateTime returnValue = defaultValue.Value;

            if (!string.IsNullOrWhiteSpace(str))
            {
                str = str.Trim();
                DateTime.TryParse(str, out returnValue);
            }
            return returnValue;
        }

        /// <summary>
        /// 获取起始时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetStart(object obj, DateTime? defaultValue = null)
        {
            DateTime date = Get(obj, defaultValue);
            string dataStr = date.ToString("yyyy-MM-dd 00:00:00");
            if (dataStr == DateTime.MinValue.ToString("yyyy-MM-dd 00:00:00"))
            {
                return DateTime.MinValue;
            }
            else
            {
                return DateTime.Parse(dataStr);
            }
        }

        /// <summary>
        /// 获取截止时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetEnd(object obj, DateTime? defaultValue = null)
        {
            DateTime date = Get(obj, defaultValue);
            string dataStr = date.ToString("yyyy-MM-dd 23:59:59");
            if (dataStr == DateTime.MaxValue.ToString("yyyy-MM-dd 23:59:59"))
            {
                return DateTime.MaxValue;
            }
            else
            {
                return DateTime.Parse(dataStr);
            }
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="format">格式</param>
        /// <returns>返回DateTime对象的字符串string</returns>
        public static DateTime FormatDateTime(this DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTime.Parse(dateTime.ToString(format));
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="format">格式</param>
        /// <returns>返回DateTime对象的字符串string</returns>
        public static DateTime FormatDate(this DateTime dateTime, string format = "yyyy-MM-dd")
        {
            return DateTime.Parse(dateTime.ToString(format));
        }

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime FormatTime(this DateTime dateTime, string format = "HH:mmss")
        {
            return DateTime.Parse(dateTime.ToString(format));
        }

        /// <summary>
        /// 格式化开始日期时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime FormatStart(this DateTime dateTime, string format = "yyyy-MM-dd 00:00:00")
        {
            return DateTime.Parse(dateTime.ToString(format));
        }

        /// <summary>
        /// 格式化结束日期时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime FormatEnd(this DateTime dateTime, string format = "yyyy-MM-dd 23:59:59")
        {
            return DateTime.Parse(dateTime.ToString(format));
        }

        #endregion

        #region 验证判断

        /// <summary>
        /// 判断是否空DateTime（NULL Empty）
        /// </summary>
        public static bool IsEmptyOrDefault(this DateTime dateTime, DateTime? defaultValue = null)
        {
            if (defaultValue == null)
                defaultValue = DefaultDateTime;

            return dateTime == null || dateTime == DateTime.MinValue || dateTime == defaultValue;
        }

        /// <summary>
        /// 判断两个时间日期部份是否相同(date2=null 时默认为当天)
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool IsEqualDate(this DateTime date1, DateTime? date2 = null)
        {

            if (date2 == null)
                date2 = DateTime.Now;
            return date1.Date.Equals(date2.Value.Date);
            //EF
            //System.Data.Entity.DbFunctions.DiffDays(date1.StartTime.Value,DateTime.Now) == 0//只获取当天
        }

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// DateTime转Timespan
        /// 对比：1970, 1, 1, 0, 0, 0, 0
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static TimeSpan GetToTimeSpan(DateTime? dateTime = null)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            TimeSpan span = (dateTime.Value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return span;
        }

        /// <summary>
        /// 获取Unix时间戳 秒级
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetToUnixTimestamp(DateTime? dateTime = null)
        {
            if (dateTime == null)
            {
                dateTime = DateTime.Now;
            }
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, 0); // 当地时区
            return (long)(dateTime.Value.ToUniversalTime() - UnixEpoch).TotalSeconds;
        }

        /// <summary>
        /// 获取时间（根据Unix时间戳）
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime GetByUnixTimestamp(long timestamp)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, 0); // 当地时区
            return UnixEpoch.AddSeconds(timestamp);
        }

        /// <summary>
        /// 获取两个时间差值信息
        /// dateTime1-dateTime2(Default:DateTime.Now)
        /// </summary>
        /// <param name="dateTime1">时间1</param>
        /// <param name="dateTime2">时间2</param>
        /// <returns></returns>
        public static TimeSpan GetToDifference(DateTime dateTime1, DateTime? dateTime2 = null)
        {
            if (dateTime2.IsNull()) { dateTime2 = DateTime.Now; };
            return dateTime1 - dateTime2.Value;
        }

        /// <summary>
        /// 获取两个时间差值信息
        /// dateTime2-dateTime1
        /// </summary>
        /// <param name="dateTime1">第一个时间</param>
        /// <param name="dateTime2">开始</param>
        /// <returns></returns>
        public static DateTimeDifferenceInfo GetToDifference2(DateTime dateTime1, DateTime? dateTime2 = null)
        {
            if (dateTime2.IsNull()) { dateTime2 = DateTime.Now; };

            //TimeSpan span = dateTime2.Value - dateTime1;
            //if (span.TotalDays > 60)
            //{
            //    return dt.ToShortDateString();
            //}
            //else
            //if (span.TotalDays > 30)
            //{
            //    return "one months ago";
            //}
            //else
            //if (span.TotalDays > 14)
            //{
            //    return "tow weeks ago";
            //}
            //else
            //if (span.TotalDays > 7)
            //{
            //    return "one weeks ago";
            //}
            //else
            //if (span.TotalDays > 1)
            //{
            //    return string.Format("{0} days ago", (int)Math.Floor(span.TotalDays));
            //}
            //else
            //if (span.TotalHours > 1)
            //{
            //    return string.Format("{0} hour ago", (int)Math.Floor(span.TotalHours));
            //}
            //else
            //if (span.TotalMinutes > 1)
            //{
            //    return string.Format("{0} minutes ago", (int)Math.Floor(span.TotalMinutes));
            //}
            //else
            //if (span.TotalSeconds >= 1)
            //{
            //    return string.Format("{0} seconds ago", (int)Math.Floor(span.TotalSeconds));
            //}
            //else
            //{
            //    return "1 seconds ago";
            //}

            return new DateTimeDifferenceInfo();
        }

        #endregion
    }

    /// <summary>
    /// 两个时间相差信息
    /// </summary>
    public class DateTimeDifferenceInfo
    {
        /// <summary>
        /// 差值年部份
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 差值月部份
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 差值周部份
        /// </summary>
        public int Week { get; set; }
        /// <summary>
        /// 差值天部份
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 差值时部份
        /// </summary>
        public int Hour { get; set; }
        /// <summary>
        /// 差值分部份
        /// </summary>
        public int Minute { get; set; }
        /// <summary>
        /// 差值秒部份
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// 差值年总值(不够为1，值为0)
        /// </summary>
        public int TotalYear { get; set; }
        /// <summary>
        /// 差值月总值(不够为1，值为0)
        /// </summary>
        public int TotalMonth { get; set; }
        /// <summary>
        /// 差值周总值(不够为1，值为0)
        /// </summary>
        public int TotalWeek { get; set; }
        /// <summary>
        /// 差值天总值(不够为1，值为0)
        /// </summary>
        public int TotalDay { get; set; }
        /// <summary>
        /// 差值时总值(不够为1，值为0)
        /// </summary>
        public int TotalHour { get; set; }
        /// <summary>
        /// 差值分总值(不够为1，值为0)
        /// </summary>
        public int TotalMinute { get; set; }
        /// <summary>
        /// 差值秒总值(不够为1，值为0)
        /// </summary>
        public int TotalSecond { get; set; }

    }
}