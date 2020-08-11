using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace WebApi1.Librarys
{
    public enum LogType
    {
        Mongodb = 1,
        Redis = 2,
        Track = 3,
        IPCheck = 4,
        JS = 5,
        Info = 6,
        Other = 0
    }

    public class Log
    {
        #region 写日志

        public static void WriteLogLocal(string LogStr)
        {
            WriteLogLocal(LogStr, "");
        }

        public static void WriteDebugLocal(string LogStr)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["DebugLog"] != null)
            {
                bool cn = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["DebugLog"]);
                if (cn)
                    WriteLogLocal(LogStr, "Debug");
            }
            else
            {
                return;
            }

        }
        public static void WriteLogLocal(string LogStr, string ApplicationHostName)
        {
            StreamWriter sw = null;
            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory + "\\log\\" + DateTime.Now.ToString("yyyy-MM");
                if (!string.IsNullOrEmpty(ApplicationHostName))
                    strPath = AppDomain.CurrentDomain.BaseDirectory + "\\log\\" + ApplicationHostName + "\\" + DateTime.Now.ToString("yyyy-MM");

                if (!System.IO.Directory.Exists(strPath))
                    System.IO.Directory.CreateDirectory(strPath);

                sw = new StreamWriter(strPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", true, System.Text.Encoding.UTF8);
                sw.WriteLine("\r\n----------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "----------\r\n" + LogStr);
                sw.Flush();
                sw.Close();
            }
            catch { }
            finally
            {
                if (sw != null)
                    sw.Dispose();
            }
        }

        #endregion

    }

    public class PathDictCache
    {
        static Dictionary<string, PathInfo> logPathDict = new Dictionary<string, PathInfo>();
        static int MaxQueueLength = 200;

        public static bool ContainsKey(string key)
        {
            return logPathDict.ContainsKey(key);
        }

        private static PathInfo GetValue(string key)
        {
            if (logPathDict.ContainsKey(key))
            {
                logPathDict[key].LastUsedDate = DateTime.Now;
                return logPathDict[key];
            }
            return null;
        }

        public static string GetPath(string key)
        {
            var obj = GetValue(key);
            if (obj != null)
                return obj.path;
            return null;
        }

        public static void AddTo(string key, PathInfo pInfo)
        {
            if (logPathDict.Count >= MaxQueueLength)
            {
                var logDictOrderByList = logPathDict.OrderBy(p => p.Value.LastUsedDate).ToList();
                int loop = logPathDict.Count - MaxQueueLength + 1;
                if (loop > 0)
                {
                    for (var i = 0; i < loop; i++)
                        logPathDict.Remove(logDictOrderByList[i].Key);
                }
            }

            logPathDict.Add(key, pInfo);
        }
    }
    public class PathInfo
    {
        public string path { get; set; }
        public DateTime LastUsedDate { get; set; }
    }

    public class LoggerStreamWriter
    {
        public string FullPath { get; set; }
        public StreamWriter SW { get; set; }
        public DateTime CreateDate { get; set; }
    }
}