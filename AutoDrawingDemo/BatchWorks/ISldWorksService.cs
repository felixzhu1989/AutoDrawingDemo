using SolidWorks.Interop.sldworks;

namespace Compass.Wpf.BatchWorks;

public interface ISldWorksService
{
    ISldWorks SwApp { get; }
}