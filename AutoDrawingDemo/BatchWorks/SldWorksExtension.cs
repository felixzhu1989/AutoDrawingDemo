using System;
using System.IO;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace AutoDrawingDemo.BatchWorks;

public static class SldWorksExtension
{
    /// <summary>
    /// 给文件名添加后缀的方法
    /// </summary>
    public static string AddSuffix(this string partName, string suffix)
    {
        //从-号拆分，-前添加suffix，例如：FNHE0001-1 -> FNHE0001_Item-M1-210203-1 其中（_Item-M1-210203）是suffix
        var endIndex = partName.LastIndexOf("-", StringComparison.Ordinal);
        return $"{partName.Substring(0, endIndex)}{suffix}{partName.Substring(endIndex)}";
    }

    #region 绘图代码扩展方法

    public static AssemblyDoc GetSubAssemblyDoc(this AssemblyDoc swAssy,out ModelDoc2 swModelLevel1, string suffix, string assyName)
    {
        var swCompLevel1 = swAssy.GetComponentByNameWithSuffix(suffix, assyName);
        swModelLevel1 = (swCompLevel1.GetModelDoc2() as ModelDoc2)!;
        return (swModelLevel1 as AssemblyDoc)!;
    }


    /// <summary>
    /// 选择零部件带后缀
    /// </summary>
    public static Component2 GetComponentByNameWithSuffix(this AssemblyDoc swAssy, string suffix, string partName)
    {
        return swAssy.GetComponentByName(partName.AddSuffix(suffix));
    }
    /// <summary>
    /// 更改尺寸，int数量
    /// </summary>
    public static void ChangeDim(this ModelDoc2 swModel, string dimName, int intValue)
    {
        var dim = (IDimension)swModel.Parameter(dimName);
        dim.SystemValue=intValue;
    }
    /// <summary>
    /// 更改尺寸，double距离,已除1000
    /// </summary>
    public static void ChangeDim(this ModelDoc2 swModel, string dimName, double dblValue)
    {
        var dim = (IDimension)swModel.Parameter(dimName);
        dim.SystemValue=dblValue / 1000d;
    }
    /// <summary>
    /// 部件压缩特征
    /// </summary>
    public static void Suppress(this Component2 swComp, string featureName)
    {
        swComp.FeatureByName(featureName).SetSuppression2(0, 2, null);
    }
    /// <summary>
    /// 部件解压特征
    /// </summary>
    public static void UnSuppress(this Component2 swComp, string featureName)
    {
        swComp.FeatureByName(featureName).SetSuppression2(1, 2, null);
    }

    /// <summary>
    /// 装配体压缩特征
    /// </summary>
    public static void Suppress(this AssemblyDoc swAssy, string featureName)
    {
        var feat =(IFeature) swAssy.FeatureByName(featureName);
        feat.SetSuppression2(0, 2, null);
    }
    /// <summary>
    /// 装配体解压特征
    /// </summary>
    public static void UnSuppress(this AssemblyDoc swAssy, string featureName)
    {
        var feat = (IFeature)swAssy.FeatureByName(featureName);
        feat.SetSuppression2(1, 2, null);
    }

    /// <summary>
    /// 装配体解压部件
    /// </summary>
    public static void Suppress(this AssemblyDoc swAssy, string suffix, string compName)
    {
        swAssy.GetComponentByNameWithSuffix(suffix, compName).SetSuppression2(0);
    }
    /// <summary>
    /// 装配体不解压部件
    /// </summary>
    public static Component2 UnSuppress(this AssemblyDoc swAssy, string suffix, string compName)
    {
        var swComp = swAssy.GetComponentByNameWithSuffix(suffix, compName);
        swComp.SetSuppression2(2);
        return swComp;
    }



    #endregion





    //packango
    public static void PackAndGo(this ISldWorks swApp, string modelPath,string packDir,string suffix)
    {
        try
        {
            if (!File.Exists(modelPath))
            {
                throw new FileNotFoundException();
            }
            int errors = 0;
            int warnings = 0;
            //打开需要pack的模型
            var swModel = swApp.OpenDoc6(modelPath,
                (int)swDocumentTypes_e.swDocASSEMBLY, (int)
                swOpenDocOptions_e.swOpenDocOptions_Silent,
                "", ref errors, ref warnings);
            var swModelExt = swModel.Extension;
            var swPackAndGo = swModelExt.GetPackAndGo();
            swPackAndGo.IncludeDrawings = false;
            swPackAndGo.IncludeSimulationResults = false;
            swPackAndGo.IncludeToolboxComponents = false;
            swPackAndGo.IncludeSuppressed = true;

            // Set folder where to save the files,目标存放文件夹
            swPackAndGo.SetSaveToName(true, packDir);
            swPackAndGo.FlattenToSingleFolder = true;
            swPackAndGo.AddSuffix = suffix;

            // 执行Pack and Go
            swModelExt.SavePackAndGo(swPackAndGo);
            swApp.CloseDoc(modelPath);
        }
        catch
        {
            swApp.CloseDoc(modelPath);
            swApp.CommandInProgress = false;
            throw;
        }
    }




}