using AutoDrawingDemo.Common;
using AutoDrawingDemo.ViewModels;
using AutoDrawingDemo.Views;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using AutoDrawingDemo.BatchWorks;
using Compass.Wpf.BatchWorks;
using SolidWorks.Interop.sldworks;

namespace AutoDrawingDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    var serviceCollection = new ServiceCollection();
        //    serviceCollection.AddDbContext<DataContext>(options =>
        //    {
        //        options.UseSqlite("Data Source=data.db");
        //    });
        //    base.OnStartup(e);
        //}

        /// <summary>
        /// 初始化时跳转登录页面，并配置默认启动页
        /// </summary>
        protected override void OnInitialized()
        {
            //配置默认首页,在应用程序设置中，启动默认首页。
            var service = Current.MainWindow!.DataContext as IConfigureService;
            service!.Configure();
            base.OnInitialized();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DrawingDataView, DrawingDataViewModel>();

            #region 注册AutoMapper
            //Install-Package AutoMapper
            //Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
            //https:www.cnblogs.com/xhubobo/p/15098642.html
            //containerRegistry.RegisterSingleton<IAutoMapperProvider, AutoMapperProvider>();
            //containerRegistry.Register(typeof(IMapper), GetMapper);

            #endregion


            containerRegistry.Register<IBatchWorksService, BatchWorksService>();
            containerRegistry.Register<IDshAutoDrawing,DshAutoDrawing>();
            
            //会导致程序启动缓慢
            //containerRegistry.Register<ISldWorksService, SldWorksService>();


            //注册对话主机服务
            containerRegistry.Register<IDialogHostService, DialogHostService>();
            //注册自定义询问窗口
            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();
        }
        //private IMapper GetMapper(IContainerProvider container)
        //{
        //    var provider = container.Resolve<IAutoMapperProvider>();
        //    return provider.GetMapper();
        //}

    }
}
