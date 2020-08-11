using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebApi1.Resource;

namespace WebApi1.Utility
{
    public class FilesHelper
    {
        #region Path

        ///.net core 路径：https://www.imooc.com/article/38470

        /// <summary>
        /// 获目路路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isAutoCreate"></param>
        /// <returns></returns>
        public static string GetDirectoryPath(string path, bool isAutoCreate = true)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists && isAutoCreate)
            {
                dir.Create();
                dir = new DirectoryInfo(path);
            }
            return dir.FullName;
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isAutoCreateDirectory"></param>
        /// <returns></returns>
        public static string GetFilePath(string path, bool isAutoCreateDirectory = true)
        {
            var file = new FileInfo(@path);
            if (file != null)
            {
                GetDirectoryPath(file.DirectoryName);
            }
            return file.FullName; //Path.Combine(file.DirectoryName, file.Name); Path.GetFullPath(path);
        }

        /// <summary>
        /// 获取Md5文件路径
        /// </summary>
        /// <param name="md5"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMd5Path(string md5, string name = "", string path = "", string root = null)
        {
            name = !name.IsEmpty() ? name : md5;
            return $"{root}{path}/{md5.Substring(0, 1)}/{md5.Substring(1, 1)}/{md5.Substring(2, 2)}/{md5}{Path.GetExtension(name)}";
        }

        /// <summary>
        /// 获取Md5文件路径
        /// </summary>
        /// <param name="md5"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetMd5PathByName(string md5, string name, string path = "", string root = null)
        {
            name = !name.IsEmpty() ? name : md5;
            return $"{root}{path}/{md5.Substring(0, 1)}/{md5.Substring(1, 1)}/{md5.Substring(2, 2)}/{name}";
        }

        public static string GetPathByTemp(string md5, string path = "", string root = null)
        {

            return $"{root}{path}/{md5}";
        }

        /// <summary>
        /// 获取临时文件路径
        /// </summary>
        /// <param name="root"></param>
        /// <param name="md5"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetTempPath(string root, string md5, string name)
        {
            var path = $"{root}/{DateTime.Now.ToString("yyMMddHH")}";
            GetDirectoryPath(path);
            return $"{path}/{md5}{Path.GetExtension(name)}";
        }

        #endregion

        #region Directory/File/Create/Delete/ReName/Extract....

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="overwrite"></param>
        public static void CopyDirectory(string source, string dest, bool overwrite = true)
        {
            DirectoryInfo dir = new DirectoryInfo(source);
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            foreach (FileSystemInfo i in files)
            {
                string dests = Path.Combine(GetDirectoryPath(dest), i.Name);
                if (i is DirectoryInfo)
                {
                    CopyDirectory(i.FullName, dests);                             //递归调用复制子文件夹
                }
                else
                {
                    File.Copy(i.FullName, dests, overwrite);                 //不是文件夹即复制文件，true表示可以覆盖同名文件
                }
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path, bool recursive = true)
        {
            if (System.IO.Directory.Exists(path))
            {
                System.IO.Directory.Delete(path, recursive);
            }
        }

        /// <summary>
        /// 目录列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryInfo[] Directorys(string path)
        {
            path = GetDirectoryPath(path);
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.GetDirectories();
        }

        /// <summary>
        /// 目录文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileInfo[] DirectoryFiles(string path)
        {
            path = GetDirectoryPath(path);
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.GetFiles();
        }

        /// <summary>
        /// 目录文件数量
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int DirectoryCount(string path)
        {
            path = GetDirectoryPath(path);
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.GetFiles().Count();
        }


        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void CreateFile(string path, string content = null)
        {
            FileStream fileStream = null;
            StreamWriter writeStream = null;
            try
            {
                fileStream = new FileStream(GetFilePath(path), FileMode.OpenOrCreate, FileAccess.ReadWrite);
                writeStream = new StreamWriter(fileStream);
                writeStream.WriteLine(content ?? "");
                writeStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fileStream?.Close();
                fileStream?.Dispose();

                writeStream?.Close();
                writeStream?.Dispose();
            }



        }

        /// <summary>
        /// 复制文件
        /// </summary>
        public static void CopyFile(string source, string dest, bool overwrite = true)
        {
            File.Copy(source, GetFilePath(dest), overwrite);
        }
        /// <summary>
        /// 临时文件复制到实际目录
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Md5"></param>
        /// <param name="overwrite"></param>
        public static void CopyFileGetMd5(string source, string Md5, bool overwrite = true)
        {
            File.Copy(source, GetMd5Path(Md5), overwrite);
        }


        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size"></param>
        /// <param name="begin"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static byte[] ReadFile(string path, out long size, long begin = 0, long count = 0)
        {
            FileStream fs = null;
            size = 0;

            try
            {
                using (fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    size = fs.Length;

                    count = count != 0 ? count : fs.Length - begin;

                    var buffer = new byte[count];

                    fs.Seek(begin, SeekOrigin.Begin);
                    fs.Read(buffer, 0, (int)count);

                    return buffer;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs?.Close();
                fs?.Dispose();
            }

            return null;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void Extract(string source, string dest)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ZipFile.ExtractToDirectory(source, dest, Encoding.GetEncoding("GB2312"));

        }

        public static void CompressFolder(string folder, string targetFilename, long size)
        {
            //bool zipping = true;


            //Task.Run(() =>
            //{
            //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //    ZipFile.ExtractToDirectory(folder, targetFilename, Encoding.GetEncoding("GB2312"));
            //    zipping = false;
            //});
            //long fiCount = 0;
            //while (zipping)
            //{

            //    if (File.Exists(targetFilename))
            //    {
            //        var fi = new FileInfo(targetFilename);
            //        fiCount = fiCount + fi.Length;
            //        long i= fiCount / size;
            //    }
            //}
        }


        /// <summary>
        /// 获取目录及文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<DirectoryFiles> GetDirectoryFiles(string path)
        {
            List<DirectoryFiles> list = new List<DirectoryFiles>();

            foreach (var item in Directorys(path))
            {
                list.Add(new DirectoryFiles()
                {
                    Name = item.Name,
                    FullName = item.FullName,
                    IsDirectory = true
                });
            }
            foreach (var item in DirectoryFiles(path))
            {
                list.Add(new DirectoryFiles()
                {
                    Name = item.Name,
                    FullName = item.FullName,
                    IsDirectory = false,
                    Size = item.Length
                });
            }

            return list;
        }

        #endregion

        #region Work/Excel/PDF

        /// <summary>
        /// 获取Excel对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IWorkbook GetExcel(string path)
        {
            IWorkbook workbook = null;
            using (FileStream fs = File.OpenRead(path))
            {
                if (path.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs); //2007
                }
                else if (path.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs); //2003
                }
            }
            return workbook;
        }

        #endregion

        #region Save / Merge

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">路径(含文件名)</param>
        /// <param name="content">内容 </param>
        /// <param name="append">追加</param>
        public static bool Save(string path, string content, bool append = false)
        {
            FileStream fileStream = null;
            StreamWriter writeStream = null;
            try
            {
                string filePath = GetFilePath(path);
                if (File.Exists(filePath) && !append)
                {
                    File.Delete(filePath);
                }
                fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                writeStream = new StreamWriter(fileStream);
                writeStream.WriteLine(content);
                writeStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
                if (writeStream != null)
                {
                    writeStream.Close();
                    writeStream.Dispose();
                }
            }

            return true;
        }

        /// <summary>
        /// 保存字节组
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="append"></param>
        public static bool Save(string path, byte[] data, int offset = 0, bool append = false)
        {
            FileStream stream = null;
            try
            {
                path = GetFilePath(path);
                if (File.Exists(path) && !append)
                {
                    File.Delete(path);
                }

                stream = new FileStream(path, append ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.ReadWrite);
                stream.Write(data, offset, data.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
            return true;
        }

        /// <summary>
        /// 文件合并(多个文件合并成一个)
        /// </summary>
        /// <param name="path">目标文件路径</param>
        /// <param name="files">源文件路径</param>
        /// <returns></returns>
        public static string Merge(string path, params string[] files)
        {
            if (path.IsEmpty() || files.IsEmpty())
            {
                return string.Empty;
            }

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            FileStream fs = null;

            try
            {
                md5.Initialize();

                path = GetFilePath(path);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

                for (var i = 0; i < files.Length; i++)
                {
                    var bytes = File.ReadAllBytes(files[i]);
                    fs.Write(bytes, 0, bytes.Length);

                    if (i == files.Length - 1)
                    {
                        //最后一块
                        md5.TransformFinalBlock(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        md5.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
                    }
                    bytes = null;
                }

                fs?.Close();
                byte[] result = md5.Hash;
                md5.Clear();

                StringBuilder build = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    build.Append(result[i].ToString("x2"));
                }

                return build.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

        }


        #endregion

        #region Content-Type

        public static string GetContentType(string name)
        {
            //http://tool.oschina.net/commons
            //.doc 	application/msword 	.dot 	application/msword
            //.pdf 	application/pdf 	.pdf 	application/pdf

            //doc,docx
            //xls,xlsx
            //ppt,pptx
            //pdf


            var ext = StringHelper.FormatTrimStart(Path.GetExtension(name), ".").ToLower();
            switch (ext)
            {
                case "doc": return "application/msword";
                case "dot": return "application/msword";
                case "docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case "dotx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
                case "docm": return "application/vnd.ms-word.document.macroEnabled.12";
                case "dotm": return "application/vnd.ms-word.template.macroEnabled.12";
                case "xls ": return "application/vnd.ms-excel";
                case "xlt ": return "application/vnd.ms-excel";
                case "xla ": return "application/vnd.ms-excel";
                case "xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "xltx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
                case "xlsm": return "application/vnd.ms-excel.sheet.macroEnabled.12";
                case "xltm": return "application/vnd.ms-excel.template.macroEnabled.12";
                case "xlam": return "application/vnd.ms-excel.addin.macroEnabled.12";
                case "xlsb": return "application/vnd.ms-excel.sheet.binary.macroEnabled.12";
                case "ppt ": return "application/vnd.ms-powerpoint";
                case "pot ": return "application/vnd.ms-powerpoint";
                case "pps ": return "application/vnd.ms-powerpoint";
                case "ppa ": return "application/vnd.ms-powerpoint";
                case "pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case "potx": return "application/vnd.openxmlformats-officedocument.presentationml.template";
                case "ppsx": return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
                case "ppam": return "application/vnd.ms-powerpoint.addin.macroEnabled.12";
                case "pptm": return "application/vnd.ms-powerpoint.presentation.macroEnabled.12";
                case "potm": return "application/vnd.ms-powerpoint.presentation.macroEnabled.12";
                case "ppsm": return "application/vnd.ms-powerpoint.slideshow.macroEnabled.12";
                case "pdf":
                    return "application/pdf";
                default:
                    return "application/octet-stream";
            }
        }

        #endregion

        #region 其它方式下载参考

        /// <summary>
        /// 获取文件的MD5码
        /// </summary>
        /// <returns></returns>
        public static string GetFileMd5(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }

            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        //namespace 文件下载_客户端_
        //    {
        //        public partial class Form1 : Form
        //        {
        //            public Form1()
        //            {
        //                InitializeComponent();
        //            }

        //            /// <summary>
        //            /// 直接下载
        //            /// </summary>
        //            /// <param name="sender"></param>
        //            /// <param name="e"></param>
        //            private async void button1_ClickAsync(object sender, EventArgs e)
        //            {
        //                using (HttpClient http = new HttpClient())
        //                {
        //                    var httpResponseMessage = await http.GetAsync("http://localhost:813/新建文件夹2.rar", HttpCompletionOption.ResponseHeadersRead);//发送请求
        //                    var contentLength = httpResponseMessage.Content.Headers.ContentLength;//读取文件大小
        //                    using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())//读取文件流
        //                    {
        //                        var readLength = 1024000;//1000K  每次读取大小
        //                        byte[] bytes = new byte[readLength];
        //                        int writeLength;
        //                        while ((writeLength = stream.Read(bytes, 0, readLength)) > 0)//分块读取文件流
        //                        {
        //                            using (FileStream fs = new FileStream(Application.StartupPath + "/temp.rar", FileMode.Append, FileAccess.Write))//使用追加方式打开一个文件流
        //                            {
        //                                fs.Write(bytes, 0, writeLength);//追加写入文件
        //                                contentLength -= writeLength;
        //                                if (contentLength == 0)//如果写入完成 给出提示
        //                                    MessageBox.Show("下载完成");
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            /// <summary>
        //            /// 异步下载
        //            /// </summary>
        //            /// <param name="sender"></param>
        //            /// <param name="e"></param>
        //            private async void button2_ClickAsync(object sender, EventArgs e)
        //            {
        //                //开启一个异步线程
        //                await Task.Run(async () =>
        //                {
        //                    //异步操作UI元素
        //                    label1.Invoke((Action)(() =>
        //                    {
        //                        label1.Text = "准备下载...";
        //                    }));

        //                    long downloadSize = 0;//已经下载大小
        //                    long downloadSpeed = 0;//下载速度
        //                    using (HttpClient http = new HttpClient())
        //                    {
        //                        var httpResponseMessage = await http.GetAsync("http://localhost:813/新建文件夹2.rar", HttpCompletionOption.ResponseHeadersRead);//发送请求
        //                        var contentLength = httpResponseMessage.Content.Headers.ContentLength;   //文件大小                
        //                        using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
        //                        {
        //                            var readLength = 1024000;//1000K
        //                            byte[] bytes = new byte[readLength];
        //                            int writeLength;
        //                            var beginSecond = DateTime.Now.Second;//当前时间秒
        //                            while ((writeLength = stream.Read(bytes, 0, readLength)) > 0)
        //                            {
        //                                //使用追加方式打开一个文件流
        //                                using (FileStream fs = new FileStream(Application.StartupPath + "/temp.rar", FileMode.Append, FileAccess.Write))
        //                                {
        //                                    fs.Write(bytes, 0, writeLength);
        //                                }
        //                                downloadSize += writeLength;
        //                                downloadSpeed += writeLength;
        //                                progressBar1.Invoke((Action)(() =>
        //                                {
        //                                    var endSecond = DateTime.Now.Second;
        //                                    if (beginSecond != endSecond)//计算速度
        //                                    {
        //                                        downloadSpeed = downloadSpeed / (endSecond - beginSecond);
        //                                        label1.Text = "下载速度" + downloadSpeed / 1024 + "KB/S";

        //                                        beginSecond = DateTime.Now.Second;
        //                                        downloadSpeed = 0;//清空
        //                                    }
        //                                    progressBar1.Value = Math.Max((int)(downloadSize * 100 / contentLength), 1);
        //                                }));
        //                            }

        //                            label1.Invoke((Action)(() =>
        //                            {
        //                                label1.Text = "下载完成";
        //                            }));
        //                        }
        //                    }
        //                });
        //            }


        //            /// <summary>
        //            /// 是否暂停
        //            /// </summary>
        //            static bool isPause = true;
        //            /// <summary>
        //            /// 下载开始位置（也就是已经下载了的位置）
        //            /// </summary>
        //            static long rangeBegin = 0;//(当然，这个值也可以存为持久化。如文本、数据库等)

        //            /// <summary>
        //            /// 断线续传
        //            /// </summary>
        //            /// <param name="sender"></param>
        //            /// <param name="e"></param>
        //            private async void button3_ClickAsync(object sender, EventArgs e)
        //            {
        //                isPause = !isPause;
        //                if (!isPause)//点击下载
        //                {
        //                    button3.Text = "暂停";

        //                    await Task.Run(async () =>
        //                    {
        //                        //异步操作UI元素
        //                        label1.Invoke((Action)(() =>
        //                        {
        //                            label1.Text = "准备下载...";
        //                        }));

        //                        long downloadSpeed = 0;//下载速度
        //                        using (HttpClient http = new HttpClient())
        //                        {
        //                            //var url = "http://localhost:813/新建文件夹2.rar";  //a标签下载链接
        //                            var url = "http://localhost:813/FileDownload/FileDownload5";    //我们自己实现的服务端下载链接
        //                            var request = new HttpRequestMessage { RequestUri = new Uri(url) };
        //                            request.Headers.Range = new RangeHeaderValue(rangeBegin, null);//【关键点】全局变量记录已经下载了多少，然后下次从这个位置开始下载。
        //                            var httpResponseMessage = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        //                            var contentLength = httpResponseMessage.Content.Headers.ContentLength;//本次请求的内容大小
        //                            if (httpResponseMessage.Content.Headers.ContentRange != null) //如果为空，则说明服务器不支持断点续传
        //                            {
        //                                contentLength = httpResponseMessage.Content.Headers.ContentRange.Length;//服务器上的文件大小
        //                            }

        //                            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
        //                            {
        //                                var readLength = 1024000;//1000K
        //                                byte[] bytes = new byte[readLength];
        //                                int writeLength;
        //                                var beginSecond = DateTime.Now.Second;//当前时间秒
        //                                while ((writeLength = stream.Read(bytes, 0, readLength)) > 0 && !isPause)
        //                                {
        //                                    //使用追加方式打开一个文件流
        //                                    using (FileStream fs = new FileStream(Application.StartupPath + "/temp.rar", FileMode.Append, FileAccess.Write))
        //                                    {
        //                                        fs.Write(bytes, 0, writeLength);
        //                                    }
        //                                    downloadSpeed += writeLength;
        //                                    rangeBegin += writeLength;
        //                                    progressBar1.Invoke((Action)(() =>
        //                                    {
        //                                        var endSecond = DateTime.Now.Second;
        //                                        if (beginSecond != endSecond)//计算速度
        //                                        {
        //                                            downloadSpeed = downloadSpeed / (endSecond - beginSecond);
        //                                            label1.Text = "下载速度" + downloadSpeed / 1024 + "KB/S";

        //                                            beginSecond = DateTime.Now.Second;
        //                                            downloadSpeed = 0;//清空
        //                                        }
        //                                        progressBar1.Value = Math.Max((int)((rangeBegin) * 100 / contentLength), 1);
        //                                    }));
        //                                }

        //                                if (rangeBegin == contentLength)
        //                                {
        //                                    label1.Invoke((Action)(() =>
        //                                    {
        //                                        label1.Text = "下载完成";
        //                                    }));
        //                                }
        //                            }
        //                        }
        //                    });
        //                }
        //                else//点击暂停
        //                {
        //                    button3.Text = "继续下载";
        //                    label1.Text = "暂停下载";
        //                }
        //            }

        //            /// <summary>
        //            /// 多线程下载文件
        //            /// </summary>
        //            /// <param name="sender"></param>
        //            /// <param name="e"></param>
        //            private async void button4_ClickAsync(object sender, EventArgs e)
        //            {
        //                using (HttpClient http = new HttpClient())
        //                {
        //                    var url = "http://localhost:813/FileDownload/FileDownload5";
        //                    var httpResponseMessage = await http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        //                    var contentLength = httpResponseMessage.Content.Headers.ContentLength.Value;
        //                    var size = contentLength / 10; //这里为了方便，就直接分成10个线程下载。（当然这是不合理的）
        //                    var tasks = new List<Task>();
        //                    for (int i = 0; i < 10; i++)
        //                    {
        //                        var begin = i * size;
        //                        var end = begin + size - 1;
        //                        var task = FileDownload(url, begin, end, i);
        //                        tasks.Add(task);
        //                    }
        //                    for (int i = 0; i < 10; i++)
        //                    {
        //                        await tasks[i];  //当然，这里如有下载异常没有考虑、文件也没有校验。各位自己完善吧。
        //                        progressBar1.Value = (i + 1) * 10;
        //                    }
        //                    FileMerge(Application.StartupPath + @"\File", "temp.rar");
        //                    label1.Text = "下载完成";
        //                }
        //            }

        //            /// <summary>
        //            /// 文件下载
        //            /// （如果你有兴趣，可以没个线程弄个进度条）
        //            /// </summary>
        //            /// <param name="begin"></param>
        //            /// <param name="end"></param>
        //            /// <param name="index"></param>
        //            /// <returns></returns>
        //            public Task FileDownload(string url, long begin, long end, int index)
        //            {
        //                var task = Task.Run(async () =>
        //                {
        //                    using (HttpClient http = new HttpClient())
        //                    {
        //                        var request = new HttpRequestMessage { RequestUri = new Uri(url) };
        //                        request.Headers.Range = new RangeHeaderValue(begin, end);//【关键点】全局变量记录已经下载了多少，然后下次从这个位置开始下载。
        //                        var httpResponseMessage = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        //                        using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
        //                        {
        //                            var readLength = 1024000;//1000K
        //                            byte[] bytes = new byte[readLength];
        //                            int writeLength;
        //                            var beginSecond = DateTime.Now.Second;//当前时间秒
        //                            var filePaht = Application.StartupPath + "/File/";
        //                            if (!Directory.Exists(filePaht))
        //                                Directory.CreateDirectory(filePaht);

        //                            try
        //                            {
        //                                while ((writeLength = stream.Read(bytes, 0, readLength)) > 0)
        //                                {
        //                                    //使用追加方式打开一个文件流
        //                                    using (FileStream fs = new FileStream(filePaht + index, FileMode.Append, FileAccess.Write))
        //                                    {
        //                                        fs.Write(bytes, 0, writeLength);
        //                                    }
        //                                }
        //                            }
        //                            catch (Exception)
        //                            {
        //                                //如果出现异常则删掉这个文件
        //                                File.Delete(filePaht + index);
        //                            }
        //                        }
        //                    }
        //                });

        //                return task;
        //            }

        //            /// <summary>
        //            /// 合并文件
        //            /// </summary>
        //            /// <param name="path"></param>
        //            /// <returns></returns>
        //            public bool FileMerge(string path, string fileName)
        //            {
        //                //这里排序一定要正确，转成数字后排序（字符串会按1 10 11排序，默认10比2小）
        //                foreach (var filePath in Directory.GetFiles(path).OrderBy(t => int.Parse(Path.GetFileNameWithoutExtension(t))))
        //                {
        //                    using (FileStream fs = new FileStream(Directory.GetParent(path).FullName + @"\" + fileName, FileMode.Append, FileAccess.Write))
        //                    {
        //                        byte[] bytes = System.IO.File.ReadAllBytes(filePath);//读取文件到字节数组
        //                        fs.Write(bytes, 0, bytes.Length);//写入文件
        //                    }
        //                    System.IO.File.Delete(filePath);
        //                }
        //                Directory.Delete(path);
        //                return true;
        //            }
        //        }
        //    }

        #endregion
    }
}