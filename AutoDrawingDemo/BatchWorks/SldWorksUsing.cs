using SolidWorks.Interop.sldworks;
using System;

namespace AutoDrawingDemo.BatchWorks;

public class SldWorksUsing : IDisposable
{
    private ISldWorks? _swApp;
    /// <summary>
    /// 连接或打开SolidWorks程序
    /// </summary>
    public ISldWorks? GetApplication()
    {
        if (_swApp == null)
        {
            _swApp = Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")!) as ISldWorks;
            if (_swApp != null)
            {
                _swApp.Visible = true;
                return _swApp;
            }
        }
        return _swApp;
    }
    public void Dispose()
    {
        _swApp=null;
    }
}