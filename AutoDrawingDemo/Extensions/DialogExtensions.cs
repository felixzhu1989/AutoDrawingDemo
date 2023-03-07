using System;
using System.Threading.Tasks;
using AutoDrawingDemo.Common;
using Prism.Events;
using Prism.Services.Dialogs;

namespace AutoDrawingDemo.Extensions;
public static class DialogExtensions
{
    /// <summary>
    /// 公共的询问窗口,扩展方法
    /// </summary>
    /// <param name="dialogHost">指定的dialoghost会话主机</param>
    /// <param name="title">标题</param>
    /// <param name="content">询问内容</param>
    /// <param name="dialogHostName">唯一的会话主机名称</param>
    /// <returns></returns>
    public static async Task<IDialogResult> Question(this IDialogHostService dialogHost, string title, string content, string dialogHostName = "RootDialog")
    {
        DialogParameters param = new DialogParameters
        {
            { "Title", title },
            { "Content", content },
            { "dialogHostName", dialogHostName }
        };
        var dialogResult = await dialogHost.ShowDialog("MsgView", param, dialogHostName);
        return dialogResult;
    }

    

    /// <summary>
    /// 注册提示消息
    /// </summary>    
    public static void RegisterMessage(this IEventAggregator aggregator,Action<MessageModel> action,string filterName="Main")
    {        
        aggregator.GetEvent<MessageEvent>().Subscribe(action, ThreadOption.PublisherThread, true, (m) =>
        {
            return m.Filter.Equals(filterName);
        });
    }
    /// <summary>
    /// 发送提示消息
    /// </summary>   
    public static void SendMessage(this IEventAggregator aggregator,string message, string filterName = "Main")
    {
        aggregator.GetEvent<MessageEvent>().Publish(new MessageModel()
        {
            Filter=filterName,
            Message=message
        });
    }
}