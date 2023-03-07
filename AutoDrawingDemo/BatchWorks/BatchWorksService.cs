using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Shapes;
using AutoDrawingDemo.Datas;

namespace AutoDrawingDemo.BatchWorks;

public class BatchWorksService:IBatchWorksService
{
    private readonly IDshAutoDrawing _dshAutoDrawing;

    public BatchWorksService(IDshAutoDrawing dshAutoDrawing)
    {
        _dshAutoDrawing = dshAutoDrawing;
    }
    public async Task BatchDrawingAsync(List<DshDataDto> dataDtos)
    {
        try
        {
            using var usingSldWorks = new SldWorksUsing();
            var swApp = usingSldWorks.GetApplication();
            if (swApp == null) throw new Exception("无法连接SolidWorks程序！");
            swApp.CommandInProgress = true;
            foreach (var dataDto in dataDtos)
            {
                await _dshAutoDrawing.AutoDrawingAsync(swApp, dataDto);
            }
            swApp.CommandInProgress = false;
        }
        catch
        {
            throw;
        }
    }
}