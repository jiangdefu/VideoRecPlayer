/** 功能描述：记录运行信息到日志文件中
 *  作    者：姜德富
 *  创建日期：2014-11-05
 *  修改记录：
 *      修改时间：2015-09-06
 *      修改内容：增加日志类型文件夹
 *      修改作者：姜德富
 *      
 *          
 *      修改时间：2016-01-12
 *      修改内容：增加删除日志相关函数
 *      修改作者：姜德富
 * 
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace EV9000Player.Util
{
    public class RunLogReport
    {
        public string Path_Type;                                            //日志的目录类型
        public int nDay;                                                    //删除几天前的日志（默认删除30天前日志）
        public Thread delLogThread;
        public bool isDelErrorLog;                                          //是否删除错误日志（默认false不删除）
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pathType">日志文件夹类型</param>
        public RunLogReport(string pathType, int nday, bool delErrorLog)
        {
            this.Path_Type = pathType;                                      //设置日志文件类型
            this.nDay = nday;                                                    //默认删除30天前的日志
            this.isDelErrorLog = delErrorLog;
            delLogThread = new Thread(new ParameterizedThreadStart(delLog));
        }
        /// <summary>
        /// 开始删除日志
        /// </summary>
        public void startDelLog()
        {
            delLogThread.Start(this);
        }
        /// <summary>
        /// 停止删除日志
        /// </summary>
        public void stopDelLog()
        {
            try
            {
                delLogThread.Abort();
            }
            catch (Exception ew)
            {
                this.writeRunErrorMsg("在停止删除日志线程时[stopDelLog()]发生异常，异常原因：" + ew.Message);
            }
        }
        /// <summary>
        /// 删除日志
        /// </summary>
        private static void delLog(Object obj)
        {
            if (obj == null)
                return;
            RunLogReport runLogReport = (RunLogReport)obj;
            DateTime nCurrentDay = DateTime.Now.AddDays(-runLogReport.nDay);
            try
            {
                string filepath = "Log\\RunLog\\" + runLogReport.Path_Type;
                if (Directory.Exists(filepath))
                {
                    string[] files = Directory.GetFiles(filepath);
                    if (files.Length > 0)
                    {
                        foreach (string logDir in files)
                        {
                            if (!logDir.Equals(""))
                            {
                                DateTime fileDateTime = DateTime.ParseExact(logDir, "yyyy-M-d-H", System.Globalization.CultureInfo.InvariantCulture);
                                if (fileDateTime.CompareTo(nCurrentDay) < 0)
                                {
                                    Directory.Delete(logDir);
                                }
                            }
                        }
                    }
                }
                if (runLogReport.isDelErrorLog)
                {
                    filepath = "Log\\ErrorLog\\" + runLogReport.Path_Type;
                    if (Directory.Exists(filepath))
                    {
                        string[] files = Directory.GetFiles(filepath);
                        if (files.Length > 0)
                        {
                            foreach (string logDir in files)
                            {
                                if (!logDir.Equals(""))
                                {
                                    DateTime fileDateTime = DateTime.ParseExact(logDir, "yyyy-M-d-H", System.Globalization.CultureInfo.InvariantCulture);
                                    if (fileDateTime.CompareTo(nCurrentDay) < 0)
                                    {
                                        Directory.Delete(logDir);
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                runLogReport.writeRunErrorMsg("删除日志文件错误,错误代码：" + e.Message);
            }

        }
        /// <summary>
        /// 设置日志目录类型
        /// </summary>
        /// <param name="pathtype">日志目录类型</param>
        public void setPathType(string pathtype)
        {
            this.Path_Type = pathtype;
        }
        /// <summary>
        /// 设置删除几天前的日志
        /// </summary>
        /// <param name="day">天数</param>
        public void setDelLogSpace(int day)
        {
            this.nDay = day;
        }
        /// <summary>
        /// 设置是否删除错误日志
        /// </summary>
        /// <param name="error">是否删除错误日志 true：删除 false：不删除</param>
        public void setIsDelErrorLog(bool error)
        {
            this.isDelErrorLog = error;
        }
        /// <summary>
        /// 获取日志目录类型
        /// </summary>
        /// <returns></returns>
        public string getPathType()
        {
            return Path_Type;
        }
        /// <summary>
        /// 记录运行日志到日志文件中
        /// </summary>
        /// <param name="msg">日志内容</param>
        public void writeRunMsg(String msg)
        {
            lock (this)
            {
                FileStream fs = null;
                StreamWriter sw = null;
                String date = DateTime.Now.ToString("yyyy-MM-dd-HH");
                String filename = "Log/RunLog/" + Path_Type + "/" + date + ".log";
                if (!Directory.Exists(filename.Substring(0, filename.LastIndexOf('/'))))
                {
                    Directory.CreateDirectory(filename.Substring(0, filename.LastIndexOf('/')));
                }
                if (File.Exists(filename))
                {
                    fs = File.Open(filename, FileMode.Append);
                }
                else
                {
                    fs = File.Open(filename, FileMode.CreateNew);
                }
                sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + msg);
                Console.WriteLine("RunLog<" + Path_Type + ">:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + msg);
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 记录运行错误日志到错误日志文件中
        /// </summary>
        /// <param name="msg">日志内容</param>
        public void writeRunErrorMsg(String msg)
        {
            lock (this)
            {
                FileStream fs = null;
                StreamWriter sw = null;
                String date = DateTime.Now.ToString("yyyy-MM-dd-HH");
                String filename = "Log/ErrorLog/" + Path_Type + "/" + date + ".log";
                if (!Directory.Exists(filename.Substring(0, filename.LastIndexOf('/'))))
                {
                    Directory.CreateDirectory(filename.Substring(0, filename.LastIndexOf('/')));
                }
                if (File.Exists(filename))
                {
                    fs = File.Open(filename, FileMode.Append);
                }
                else
                    fs = File.Open(filename, FileMode.CreateNew);
                sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + msg);
                Console.WriteLine("ErrorLog<" + Path_Type + ">:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + msg);
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 写日志到指定文件中
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="msg">日志内容</param>
        public void writeLogToFile(String filename, String msg)
        {
            lock (this)
            {
                FileStream fs = null;
                StreamWriter sw = null;
                if ((filename != null && !filename.Equals("")))
                {
                    if (File.Exists("Log\\" + filename))
                    {
                        fs = File.Open(filename, FileMode.Append);
                    }
                    else
                    {
                        fs = File.Open(filename, FileMode.CreateNew);
                    }
                }
                sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + msg);
                sw.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~RunLogReport()
        {

        }
    }
}
