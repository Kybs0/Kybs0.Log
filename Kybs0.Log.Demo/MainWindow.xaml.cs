using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kybs0.Log.Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var log = new Logger("YUDONGTest");
            log.Error("test");
        }

        private void UI线程异常_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new InvalidOperationException("UI线程异常_OnClick");
            try
            {
                //AppDomain.CurrentDomain.UnhandledException能捕获
                //当前的TryCatch无法捕获，因为不在一下上下文
                Test1();
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        
        public async void Test1()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new InvalidOperationException("Test1");
        }

        private void Task线程异常_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Test2();
            }
            catch (Exception exception)
            {
                throw;
            }
        }
        public void Test2()
        {
           Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                throw new InvalidOperationException("Test2");
            });
        }
    }
}
