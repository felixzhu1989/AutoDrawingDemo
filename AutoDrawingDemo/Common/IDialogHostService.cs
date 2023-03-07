using System.Threading.Tasks;
using Prism.Services.Dialogs;

namespace AutoDrawingDemo.Common;
/// <summary>
/// 自定义对话服务
/// </summary>
public interface IDialogHostService: IDialogService
{
    Task<IDialogResult> ShowDialog(string name,IDialogParameters? parameters,string dialogHostName= "RootDialog");
}
