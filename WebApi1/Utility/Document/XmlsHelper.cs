using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace WebApi1.Utility
{
    /// <summary>
    /// XML辅助类
    /// </summary>
    public static class XmlsHelper
    {
        #region Document

        /// <summary>
        /// 对象类型/对应的序列化对象
        /// </summary>
        static ConcurrentDictionary<int, XmlSerializer> _serializers = new ConcurrentDictionary<int, XmlSerializer>();

        /// <summary>
        /// xml序列化成字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>xml字符串</returns>
        public static string Serialize(object obj)
        {
            var content = "";
            var serializer = GetSerializer(obj.GetType());

            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = null;
            StreamReader sr = null;

            try
            {
                xtw = new System.Xml.XmlTextWriter(ms, Encoding.UTF8);
                xtw.Formatting = System.Xml.Formatting.Indented;
                serializer.Serialize(xtw, obj);
                ms.Seek(0, SeekOrigin.Begin);
                sr = new StreamReader(ms);
                content = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
                if (sr != null)
                    sr.Close();
                ms.Close();
            }

            return content;

        }

        /// <summary>
        /// xml字符串序列化成对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="content">xml字符串</param>
        /// <returns></returns>
        public static object DeSerialize(Type type, string content)
        {
            byte[] b = Encoding.UTF8.GetBytes(content);
            XmlSerializer serializer = GetSerializer(type);
            return serializer.Deserialize(new MemoryStream(b));
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static object Load(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                // open the stream...
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static bool Save(object obj, string filename)
        {
            bool success = false;

            FileStream fs = null;

            try
            {
                string dirPath = Path.GetDirectoryName(filename);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                //serializer.Serialize(Console.Out, obj);
                serializer.Serialize(fs, obj);
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return success;
        }

        /// <summary>
        /// 获取序列化对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static XmlSerializer GetSerializer(Type t)
        {
            int type_hash = t.GetHashCode();

            if (!_serializers.ContainsKey(type_hash))
                _serializers.TryAdd(type_hash, new XmlSerializer(t));

            return _serializers[type_hash];
        }

        #endregion
    }
}