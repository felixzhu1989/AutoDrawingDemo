using System.Collections.Generic;
using System.Threading.Tasks;
using AutoDrawingDemo.Datas;

namespace AutoDrawingDemo.BatchWorks;

public interface IBatchWorksService
{
    Task BatchDrawingAsync(List<DshDataDto> dataDtos);
}