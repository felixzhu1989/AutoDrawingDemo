using Prism.Commands;
using Prism.Services.Dialogs;

namespace AutoDrawingDemo.Common;

/// <summary>
/// 自定义对话服务,prism对话服务
/// </summary>
public interface IDialogHostAware
{
    string DialogHostName { get; set; }//顶级节点，所属DialogHost的名称
    void OnDialogOpen(IDialogParameters parameters);//打开Dialog所要执行的操作，传递参数
    DelegateCommand SaveCommand { get; set; }//保存命令
    DelegateCommand CancelCommand { get; set; }//取消命令
}
