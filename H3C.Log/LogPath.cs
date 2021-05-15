using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kybs0.Log
{
    /// <summary>
    /// 日志路径
    /// </summary>
    public class LogPath
    {
        private readonly string _appName;

        public LogPath(string appName)
        {
            _appName = appName;
        }
        /// <summary>
        /// 提示日志路径
        /// </summary>
        public string InfoPath=> GetLogFilePath($"info_{DateTime.Now:yyyy-MM-dd}.txt");
        /// <summary>
        /// 异常日志路径
        /// </summary>
        public string ErrorPath=> GetLogFilePath($"error_{DateTime.Now:yyyy-MM-dd}.txt");
        /// <summary>
        /// 功能日志路径
        /// </summary>
        public string MessagePath => GetLogFilePath($"message_{DateTime.Now:yyyy-MM-dd}.txt");
        private string GetLogFilePath(string fileName)
        {
            string logFolder = GetLogFolder();
            string logFilePath = Path.Combine(logFolder, fileName);

            try
            {
                if (!File.Exists(logFilePath))
                {
                    var aaa = File.Create(logFilePath);
                    aaa.Dispose();
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return logFilePath;
        }

        private string _logFolder = string.Empty;
        public string GetLogFolder()
        {
            if (!string.IsNullOrWhiteSpace(_logFolder))
            {
                return _logFolder;
            }
            var dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var logFolder = Path.Combine(dataFolder, _appName,"Log");
            CreateFolder(logFolder);
            if (Directory.Exists(logFolder))
            {
                _logFolder = logFolder;
            }
            return _logFolder;
        }
        public void CreateFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}
