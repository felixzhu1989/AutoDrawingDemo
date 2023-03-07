using System;
using SolidWorks.Interop.sldworks;

namespace Compass.Wpf.BatchWorks;

public class SldWorksService : ISldWorksService
{
    public ISldWorks SwApp { get; }
    public SldWorksService()
    {
        SwApp=(Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application")!) as ISldWorks)!;
        SwApp.Visible=true;
    }
}