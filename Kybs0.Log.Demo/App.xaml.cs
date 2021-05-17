using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Kybs0.Log.Demo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private ILogger _log;
        public App()
        {
            _log = new Logger("YUDONGTest");
            //全局异常捕获.主要指的是UI线程。
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            //当某个异常未被捕获时出现。主要指的是非UI线程
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //task线程内未处理捕获
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _log.Error(e.Exception);
            //表示补救成功
            e.Handled = true;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                _log.Error(exception);
                //通过配置legacyUnhandledExceptionPolicy防止后台线程抛出的异常让程序崩溃退出，
                //e.IsTerminating经过配置，才会变成false
            }
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            foreach (Exception item in e.Exception.InnerExceptions)
            {
                _log.Error(item);
            }
            //设置该异常已察觉（这样处理后就不会引起程序崩溃）
            e.SetObserved();
        }
    }
}
