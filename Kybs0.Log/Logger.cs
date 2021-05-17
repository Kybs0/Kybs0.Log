using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Kybs0.Log
{
    public class Logger : ILogger
    {
        private readonly LogPath _logPath;

        public Logger(string appName)
        {
            _logPath = new LogPath(appName);
        }

        public string LogFolder => _logPath.GetLogFolder();

        #region 记录日志

        public void Info(string info)
        {
            try
            {
                var infos = new List<string>()
                {
                    $"记录时间：{DateTime.Now:yyyy-MM-dd hh:mm:ss} {info}"
                };
                File.AppendAllLines(_logPath.InfoPath, infos);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public void Message(string message)
        {
            try
            {
                var infos = new List<string>()
                {
                    "***************************************************************\r\n",
                    $"记录时间：{DateTime.Now:yyyy-MM-dd hh:mm:ss}",
                    $"描述：{message}",
                };
                infos[infos.Count - 1] += "\r\n";
                File.AppendAllLines(_logPath.MessagePath, infos);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public void Error(string error)
        {
            try
            {
                var infos = new List<string>()
                {
                    "***************************************************************\r\n",
                    $"记录时间：{DateTime.Now:yyyy-MM-dd hh:mm:ss}",
                    $"线程ID  ：{GetCurrentThreadId()}",
                    $"错误描述：{error}",
                };
                infos[infos.Count - 1] += "\r\n";
                File.AppendAllLines(_logPath.ErrorPath, infos);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Error(string error, Exception ex)
        {
            try
            {
                var infos = new List<string>()
                {
                    "***************************************************************\r\n",
                    $"记录时间：{DateTime.Now:yyyy-MM-dd hh:mm:ss}",
                    $"线程ID  ：{GetCurrentThreadId()}",
                    $"错误描述：{error} {ex.Message}"
                };
                if (ex.StackTrace != null)
                {
                    infos.Add($"异常堆栈：{ex.StackTrace}");
                }
                infos[infos.Count - 1] += "\r\n";
                File.AppendAllLines(_logPath.ErrorPath, infos);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public void Error(Exception ex)
        {
            try
            {
                var infos = new List<string>()
                {
                    "***************************************************************\r\n",
                    $"记录时间：{DateTime.Now:yyyy-MM-dd hh:mm:ss}",
                    $"线程ID  ：{GetCurrentThreadId()}",
                    $"错误描述：{ex.Message}",
                };
                if (ex.StackTrace != null)
                {
                    infos.Add($"异常堆栈：{ex.StackTrace}");
                }
                infos[infos.Count - 1] += "\r\n";
                File.AppendAllLines(_logPath.ErrorPath, infos);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #endregion


        private string GetCurrentThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId.ToString();
        }
    }
}
