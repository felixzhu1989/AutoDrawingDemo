using System.IO;
using System.Threading.Tasks;
using AutoDrawingDemo.Datas;
using Compass.Wpf.BatchWorks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace AutoDrawingDemo.BatchWorks;

public class DshAutoDrawing : IDshAutoDrawing
{

    public async Task AutoDrawingAsync(ISldWorks swApp,DshDataDto dataDto)
    {
        try
        {
            #region 文件夹准备与打包

            //获取程序的位置
            var currentDir = System.Environment.CurrentDirectory;
            //模型位置
            var modelPath = System.IO.Path.Combine(currentDir, "DSH", "DSH.SLDASM");
            //packandgo文件夹位置
            var packDir = System.IO.Path.Combine(currentDir, dataDto.Name);
            //判断打包目标文件夹是否存在，不存在则创建
            if (!Directory.Exists(packDir)) Directory.CreateDirectory(packDir);
            var suffix = $"_{dataDto.Name}";
            //packandgo装配体位置
            var packPath = System.IO.Path.Combine(packDir, $"DSH{suffix}.SLDASM");
            //判断pack后的装配体是否存在，存在就直接打开，否则先执行pack
            if (!File.Exists(packPath)) swApp.PackAndGo(modelPath, packDir, suffix);

            #endregion

            #region 打开打包后的文件

            int errors = 0;
            int warnings = 0;
            //打开Pack后的模型
            //顶级Model
            var swModelTop = swApp.OpenDoc6(packPath,
                (int)swDocumentTypes_e.swDocASSEMBLY,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent,
                "", ref errors, ref warnings);
            //顶级Assy
            var swAssyTop = swModelTop as AssemblyDoc;
            //打开装配体后必须重建，使Pack后的零件名都更新到带后缀的状态，否则程序出错
            swModelTop.ForceRebuild3(true); //TopOnly参数设置成true，只重建顶层，不重建零件内部

            #endregion

            using var marine = new MarinePart();

            #region 顶层操作

            #endregion

            #region OuterFrame
            var swAssyLevel1 = swAssyTop.GetSubAssemblyDoc(out ModelDoc2 swModelLevel1, suffix, "OuterFrame-1");

            //MNSF0119-1，上外框
            marine.MNSF0119(swAssyLevel1, suffix, "MNSF0119-1", dataDto);

            //MNSF0120-1，下外框
            marine.MNSF0120(swAssyLevel1, suffix, "MNSF0120-1", dataDto);


            //MNSF0121-1，右外框
            marine.MNSF0121(swAssyLevel1, suffix, "MNSF0121-1", dataDto);

            //MNSF0122-1，左外框
            marine.MNSF0122(swAssyLevel1, suffix, "MNSF0122-1", dataDto);


            #endregion

            #region InnerFrame
            swAssyLevel1 = swAssyTop.GetSubAssemblyDoc(out swModelLevel1, suffix, "InnerFrame-1");
            //修改子装配阵列
            var netLength = dataDto.Length - 100d - 3d * 2d;
            var supportNumber = netLength<=700d ? 2 : netLength>700d && netLength<=1200d ? 3 : 4;
            var supportDis = (netLength - 270d) / (supportNumber - 1);
            swModelLevel1.ChangeDim("D1@LocalLPattern1", supportNumber);
            swModelLevel1.ChangeDim("D3@LocalLPattern1", supportDis);

            //MNSO0040-1，上导风板
            marine.MNSO0040(swAssyLevel1, suffix, "MNSO0040-1", dataDto);

            //MNSO0039-1，下导风板
            marine.MNSO0039(swAssyLevel1, suffix, "MNSO0039-1", dataDto);

            //MNSO0041-1，左右内框
            marine.MNSO0041(swAssyLevel1, suffix, "MNSO0041-1", dataDto);

            //MNSO0037-1，上卡槽板
            marine.MNSO0037(swAssyLevel1, suffix, "MNSO0037-1", dataDto);

            //MNSO0038-1，下卡槽板
            marine.MNSO0038(swAssyLevel1, suffix, "MNSO0038-1", dataDto);

            //MNSO0042-1，左右挡板
            marine.MNSO0042(swAssyLevel1, suffix, "MNSO0042-1", dataDto);

            //MNSS0007-1，上内框
            marine.MNSS0007(swAssyLevel1, suffix, "MNSS0007-1", dataDto);

            //MNSS0007-1，下内框
            marine.MNSS0008(swAssyLevel1, suffix, "MNSS0008-1", dataDto);

            #endregion

            #region 叶片装配
            swAssyLevel1 = swAssyTop.GetSubAssemblyDoc(out swModelLevel1, suffix, "Blade-1");
            //叶片阵列数量
            var bladeNumber = (int)((netLength-28d*2d - 45d) / 28.5d) + 1;
            //D1@Distance2
            swModelLevel1.ChangeDim("D1@Distance2",(bladeNumber-1)*28.5d-1);
            //修改阵列
            swModelLevel1.ChangeDim("D1@LocalLPattern1", bladeNumber);
            //选择最后一片挡板
            var endBlad = netLength - 28d * 2d - 4d-(bladeNumber-1)*28.5d;
            if (endBlad >= 56d && endBlad < 64d)
            {
                swAssyLevel1.Suppress(suffix, "LH-2020-1");
                swAssyLevel1.UnSuppress(suffix, "LH-2021-1");
                marine.DSHBladeHeight(swAssyLevel1, suffix, "LH-2021-1", dataDto);
            }
            else if(endBlad >=64d)
            {
                swAssyLevel1.UnSuppress(suffix, "LH-2020-1");
                swAssyLevel1.Suppress(suffix, "LH-2021-1");
                marine.DSHBladeHeight(swAssyLevel1, suffix, "LH-2020-1", dataDto);
            }
            else
            {
                swAssyLevel1.Suppress(suffix, "LH-2020-1");
                swAssyLevel1.Suppress(suffix, "LH-2021-1");
            }

            //2100900001-1，钢管
            marine.DSHPipeLength(swAssyLevel1, suffix, "2100900001-1", dataDto);

            //叶片高度
            marine.DSHBladeHeight(swAssyLevel1, suffix, "MNSD0001-1", dataDto);
            marine.DSHBladeHeight(swAssyLevel1, suffix, "MNSD0004-1", dataDto);
            marine.DSHBladeHeight(swAssyLevel1, suffix, "MNSD0005-1", dataDto);
            marine.DSHBladeHeight(swAssyLevel1, suffix, "MNSD0006-1", dataDto);
            
            #endregion

            #region 保存操作

            swModelTop.ForceRebuild3(true); //设置成true，直接更新顶层，速度很快，设置成false，每个零件都会更新，很慢
            swModelTop.Save(); //保存，很耗时间
            swApp.CloseDoc(packPath); //关闭，很快

            #endregion
        }
        catch
        {
            swApp.CommandInProgress = false;
            await Task.Delay(500);
            throw;
        }
    }
}