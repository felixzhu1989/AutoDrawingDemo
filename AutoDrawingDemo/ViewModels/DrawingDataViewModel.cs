using System;
using Compass.Wpf.ViewModels;
using Prism.Commands;
using Prism.Ioc;
using Prism.Services.Dialogs;
using AutoDrawingDemo.Common;
using AutoDrawingDemo.Extensions;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoDrawingDemo.Datas;
using System.Threading.Tasks;
using AutoDrawingDemo.BatchWorks;
using Prism.Regions;

namespace AutoDrawingDemo.ViewModels;

public class DrawingDataViewModel : NavigationViewModel
{
    private readonly IContainerProvider _containerProvider;
    private readonly IDialogHostService _dialogHost;
    private readonly IEventAggregator _aggregator;
    private readonly IBatchWorksService _batchWorksService;

    //抬头
    private string title;
    public string Title
    {
        get => title;
        set { title = value; RaisePropertyChanged(); }
    }




    #region 右侧展开栏
    private bool isRightDrawerOpen;
    /// <summary>
    /// 右侧窗口是否展开
    /// </summary>
    public bool IsRightDrawerOpen
    {
        get => isRightDrawerOpen;
        set { isRightDrawerOpen = value; RaisePropertyChanged(); }
    }
    private string rightDrawerTitle;
    public string RightDrawerTitle
    {
        get => rightDrawerTitle;
        set { rightDrawerTitle = value; RaisePropertyChanged(); }
    }

    //当前分段
    private DshDataDto currentDataDto;
    public DshDataDto CurrentDataDto
    {
        get => currentDataDto;
        set { currentDataDto = value; RaisePropertyChanged(); }
    }
    #endregion


    #region DataGrid数据
    private ObservableCollection<DshDataDto> dataDtos;
    public ObservableCollection<DshDataDto> DataDtos
    {
        get => dataDtos;
        set { dataDtos = value; RaisePropertyChanged(); }
    }

    public bool? IsAllDataDtosSelected
    {
        get
        {
            var selected = DataDtos.Select(item => item.IsSelected).Distinct().ToList();
            return selected.Count == 1 ? selected.Single() : null;
        }
        set
        {
            if (value.HasValue)
            {
                SelectAll(value.Value, DataDtos);
                RaisePropertyChanged();
            }
        }
    }
    private static void SelectAll(bool select, IEnumerable<DshDataDto> models)
    {
        foreach (var model in models)
        {
            model.IsSelected = select;
        }
    }
    #endregion

    #region Progress
    private bool showProgressBar;
    public bool ShowProgressBar
    {
        get => showProgressBar;
        set
        {
            showProgressBar = value;
            RaisePropertyChanged();
        }
    }
    private string progressTips;
    public string ProgressTips
    {
        get => progressTips;
        set
        {
            progressTips = value;
            RaisePropertyChanged();
        }
    }
    #endregion

    public DelegateCommand<string> ExecuteCommand { get; }//根据提供的不同参数执行不同的逻辑
    public DelegateCommand<DshDataDto> UpdateDataCommand { get; }
    public DrawingDataViewModel(IContainerProvider containerProvider) : base(containerProvider)
    {
        _containerProvider = containerProvider;
        _dialogHost = containerProvider.Resolve<IDialogHostService>();
        _aggregator=containerProvider.Resolve<IEventAggregator>();
        _batchWorksService=containerProvider.Resolve<IBatchWorksService>();
        DataDtos = new ObservableCollection<DshDataDto>();
        ExecuteCommand = new DelegateCommand<string>(Execute);
        UpdateDataCommand = new DelegateCommand<DshDataDto>(Update);
    }



    private void Execute(string obj)
    {
        switch (obj)
        {
            case "Add": Add(); break;
            case "Save": Save(); break;
            case "Delete": Delete(); break;
            case "AutoDrawing":
                AutoDrawing(); break;
        }
    }

    #region 增删改查
    private void Add()
    {
        CurrentDataDto=new DshDataDto();
        IsRightDrawerOpen = true;
        RightDrawerTitle = "添加数据";
    }
    private void Update(DshDataDto obj)
    {
        CurrentDataDto=obj;
        IsRightDrawerOpen = true;
        RightDrawerTitle = "修改数据";
    }
    private async void Delete()
    {
        //todo:判断当前CurrentDataDto是否为修改
        if (CurrentDataDto.Id > 0)
        {
            //弹窗提示用户确定需要删除吗？
            //删除询问
            var dialogResult = await _dialogHost.Question("删除确认", $"确认删除当前数据吗?");
            if (dialogResult.Result != ButtonResult.OK) return;
            //todo:执行删除，并更新界面显示
            DataDtos.Remove(CurrentDataDto);
            IsRightDrawerOpen=false;
        }
        CurrentDataDto=new DshDataDto();
    }
    private void Save()
    {
        //todo:数据验证
        if (string.IsNullOrEmpty(CurrentDataDto.Name))
        {
            _aggregator.SendMessage("名称不能为空");
            return;
        }
        if (CurrentDataDto.Id > 0)
        {
            //todo:修改时需要什么操作
            CurrentDataDto.Name=CurrentDataDto.Name.ToUpper();
            IsRightDrawerOpen = false;
        }

        else
        {
            //todo：增加时时需要什么操作
            var maxId = DataDtos.Count>0 ? DataDtos.Max(x => x.Id) : 0;
            CurrentDataDto.Id= maxId+1;
            CurrentDataDto.Name=CurrentDataDto.Name.ToUpper();
            DataDtos.Add(CurrentDataDto);
            BindSelect();
            IsRightDrawerOpen = false;
        }

    }
    #endregion


    #region 开启批量作图
    private async void AutoDrawing()
    {
        //获取勾选的ModuleDto
        List<DshDataDto> selectedDataDto = DataDtos.Where(x => x.IsSelected).ToList();
        if (selectedDataDto.Count == 0)
        {
            _aggregator.SendMessage("请勾选需要作图的数据!");
            return;
        }
        _aggregator.SendMessage("自动作图开始");
        ProgressTips = "正在作图，请勿导航到其他页面！";
        ShowProgressBar =true;
        await Task.Delay(2000);

        try
        {
            await _batchWorksService.BatchDrawingAsync(selectedDataDto);
        }
        catch (Exception e)
        {
            _aggregator.SendMessage(e.Message);
        }

        ProgressTips = "作图完成！";
        _aggregator.SendMessage("自动作图完成");
        await Task.Delay(1000);
        ShowProgressBar = false;
        IsAllDataDtosSelected = false;
    }
    #endregion


    #region 初始化
    private void GetDataDtos()
    {
        //ProjectParameter parameter = new() { ProjectId = Project.Id };
        //var moduleDtosResult = await _service.GetModuleListAsync(parameter);
        //if (moduleDtosResult.Status)
        //{
        //    ModuleDtos.Clear();
        //    ModuleDtos.AddRange(moduleDtosResult.Result);
        //}

        //模拟数据初始化
        DataDtos.Clear();
        DataDtos.Add(new DshDataDto { Id = 1, Name ="GQ3001", Length = 550, Width = 160, Height = 900, FlangeHoleDia = 12, FlangeHoleDis = 150, XFlangeHoleNumber = 2, YFlangeHoleNumber = 4 });
        DataDtos.Add(new DshDataDto { Id = 2, Name ="GQ1702", Length = 550, Width = 160, Height = 550, FlangeHoleDia = 12, FlangeHoleDis = 150, XFlangeHoleNumber = 2, YFlangeHoleNumber = 2 });
        DataDtos.Add(new DshDataDto { Id = 3, Name ="GQ2001", Length = 800, Width = 160, Height = 700, FlangeHoleDia = 12, FlangeHoleDis = 150, XFlangeHoleNumber = 4, YFlangeHoleNumber = 3 });
        BindSelect();
        IsAllDataDtosSelected = false;
    }

    private void BindSelect()
    {
        //绑定勾选数据变更
        foreach (var model in DataDtos)
        {
            model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(DshDataDto.IsSelected))
                    RaisePropertyChanged(nameof(IsAllDataDtosSelected));
            };
        }
    }


    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);
        ShowProgressBar = false;
        GetDataDtos();
    }
    #endregion
}