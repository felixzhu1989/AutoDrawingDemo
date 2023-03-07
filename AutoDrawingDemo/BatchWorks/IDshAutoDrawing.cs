using System.Threading.Tasks;
using AutoDrawingDemo.Datas;
using SolidWorks.Interop.sldworks;

namespace AutoDrawingDemo.BatchWorks;

public interface IDshAutoDrawing
{
    Task AutoDrawingAsync(ISldWorks swApp,DshDataDto dataDto);
}