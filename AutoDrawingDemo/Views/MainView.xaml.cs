using AutoDrawingDemo.Common;
using Prism.Services.Dialogs;
using System.Windows;
using System.Windows.Input;
using AutoDrawingDemo.Extensions;
using Prism.Events;

namespace AutoDrawingDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IEventAggregator aggregator, IDialogHostService dialogHost)
        {
            InitializeComponent();
            //注册snackbar提示消息,只订阅来自Main的消息，默认的消息。
            aggregator.RegisterMessage(arg =>
            {
                Snackbar.MessageQueue!.Enqueue(arg.Message);//往消息队列中添加消息
            }, "Main");


            //移动窗体
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed) DragMove();
            };
            //最大化，最小化窗体
            ColorZone.MouseDoubleClick += (s, e) =>
            {
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            };
            BtnMin.Click += (s, e) => { WindowState = WindowState.Minimized; };
            BtnMax.Click += (s, e) =>
            {
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            };
            //系统退出询问
            BtnClose.Click += async (s, e) =>
            {
                var dialogResult = await dialogHost.Question("温馨提示", "确认退出系统吗");
                if (dialogResult.Result != ButtonResult.OK) return;
                Close();
            };

        }

    }
}
