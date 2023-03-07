using AutoDrawingDemo.Common;
using AutoDrawingDemo.Extensions;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace AutoDrawingDemo.ViewModels;

public class MainViewModel : BindableBase, IConfigureService
{
    private readonly IRegionManager _regionManager;

    public DelegateCommand HomeCommand { get; }
    public MainViewModel(IRegionManager regionManager, IContainerProvider container)
    {
        _regionManager = regionManager;
        HomeCommand = new DelegateCommand(() =>
        {
            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("DrawingDataView");
        });
    }
    /// <summary>
    /// 初始化配置默认首页
    /// </summary>
    public void Configure()
    {
        _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("DrawingDataView");
    }
}