using System;
using AutoDrawingDemo.Datas;
using SolidWorks.Interop.sldworks;

namespace AutoDrawingDemo.BatchWorks;

public class MarinePart : IDisposable
{
    #region DSH OuterFrame外框
    public void MNSF0119(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        //上下叶片(x方向)假设铆螺母间距150，计算铆螺母数量，距离边缘54
        var xNutNumber = (int)((dataDto.Length - 100d - 3d - 54d * 2d) / 150d) + 1;
        var xNutDis = (dataDto.Length - 100d - 3d - 54d * 2d) / (xNutNumber - 1);
        //上下叶片(x方向)假设焊接间距150，计算焊接数量，距离边缘20
        var xWeldNumber = (int)((dataDto.Length - 100d - 3d - 20d * 2d) / 200d) + 1;
        var xWeldDis = (dataDto.Length - 100d - 3d - 20d * 2d) / (xNutNumber - 1);

        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        //swModelLevel2.ChangeDim("Thickness@Sheet-Metal",3d);//修改板厚
        //swModelLevel2.ChangeDim("D2@Sketch2",50d+3d);//法兰为50+板厚
        swModelLevel2.ChangeDim("D1@Sketch2", dataDto.Length);//长
        swModelLevel2.ChangeDim("D2@Sketch9", dataDto.Width);//宽(纵向深度)
        swModelLevel2.ChangeDim("D6@Sketch9", dataDto.Length-100d-6d);//100为法兰宽度，6为板厚3*2，因为现在不考虑板厚，因此减去固定值
        //swModelLevel2.ChangeDim("D5@Sketch9",3d/2d);//板厚/2
        swModelLevel2.ChangeDim("D1@LPattern1", xNutNumber);
        swModelLevel2.ChangeDim("D3@LPattern1", xNutDis);

        swModelLevel2.ChangeDim("D1@Sketch20", dataDto.FlangeHoleDia);
        //计算得出x向法兰第二个孔与第一个孔的间距
        swModelLevel2.ChangeDim("D1@Sketch21", (dataDto.Length-dataDto.XFlangeHoleNumber*dataDto.FlangeHoleDis-20d*2)/2d);
        swModelLevel2.ChangeDim("D1@LPattern2", dataDto.XFlangeHoleNumber+1);
        swModelLevel2.ChangeDim("D3@LPattern2", dataDto.FlangeHoleDis);
        swModelLevel2.ChangeDim("D1@LPattern3", xWeldNumber);
        swModelLevel2.ChangeDim("D3@LPattern3", xWeldDis);
    }

    public void MNSF0120(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        //上下叶片(x方向)假设铆螺母间距150，计算铆螺母数量，距离边缘54
        var xNutNumber = (int)((dataDto.Length - 100d - 3d - 54d * 2d) / 150d) + 1;
        var xNutDis = (dataDto.Length - 100d - 3d - 54d * 2d) / (xNutNumber - 1);
        //上下叶片(x方向)假设焊接间距150，计算焊接数量，距离边缘20
        var xWeldNumber = (int)((dataDto.Length - 100d - 3d - 20d * 2d) / 200d) + 1;
        var xWeldDis = (dataDto.Length - 100d - 3d - 20d * 2d) / (xNutNumber - 1);


        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        //swModelLevel2.ChangeDim("Thickness@Sheet-Metal",3d);//修改板厚
        //swModelLevel2.ChangeDim("D2@Sketch2",50d+3d);//法兰为50+板厚
        swModelLevel2.ChangeDim("D1@Sketch2", dataDto.Length);//长
        swModelLevel2.ChangeDim("D2@Sketch9", dataDto.Width);//宽(纵向深度)
        swModelLevel2.ChangeDim("D1@Sketch9", dataDto.Length-100d-6d);//100为法兰宽度，6为板厚3*2，因为现在不考虑板厚，因此减去固定值
        //swModelLevel2.ChangeDim("D6@Sketch9",3d/2);//板厚/2
        swModelLevel2.ChangeDim("D1@LPattern1", xNutNumber);
        swModelLevel2.ChangeDim("D3@LPattern1", xNutDis);

        swModelLevel2.ChangeDim("D1@Sketch20", dataDto.FlangeHoleDia);
        //计算得出x向法兰第二个孔与第一个孔的间距
        swModelLevel2.ChangeDim("D1@Sketch21", (dataDto.Length-dataDto.XFlangeHoleNumber*dataDto.FlangeHoleDis-20d*2)/2d);
        swModelLevel2.ChangeDim("D1@LPattern2", dataDto.XFlangeHoleNumber+1);
        swModelLevel2.ChangeDim("D3@LPattern2", dataDto.FlangeHoleDis);

        swModelLevel2.ChangeDim("D1@LPattern3", xWeldNumber);
        swModelLevel2.ChangeDim("D3@LPattern3", xWeldDis);
    }


    public void MNSF0121(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netHeight = dataDto.Height - 100d - 3d * 2d;//实际高度减去法兰宽度和板厚

        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        //swModelLevel2.ChangeDim("Thickness@Sheet-Metal",3d);//修改板厚
        swModelLevel2.ChangeDim("D1@Sketch2", netHeight);
        swModelLevel2.ChangeDim("D2@Sketch9", dataDto.Width);//宽(纵向深度)
        //计算得出y向法兰第一个孔与边缘的间距
        swModelLevel2.ChangeDim("D3@Sketch21", (netHeight-dataDto.FlangeHoleDis*dataDto.YFlangeHoleNumber)/2d);
        swModelLevel2.ChangeDim("D1@LPattern2", dataDto.YFlangeHoleNumber+1);
        swModelLevel2.ChangeDim("D3@LPattern2", dataDto.FlangeHoleDis);
        //swModelLevel2.ChangeDim("D5@Sketch9",3d/2);//板厚/2
    }

    public void MNSF0122(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netHeight = dataDto.Height - 100d - 3d * 2d;//实际高度减去法兰宽度和板厚

        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        //swModelLevel2.ChangeDim("Thickness@Sheet-Metal",3d);//修改板厚
        swModelLevel2.ChangeDim("D1@Sketch2", netHeight);
        swModelLevel2.ChangeDim("D2@Sketch9", dataDto.Width);//宽(纵向深度)
        //计算得出y向法兰第一个孔与边缘的间距
        swModelLevel2.ChangeDim("D3@Sketch21", (netHeight-dataDto.FlangeHoleDis*dataDto.YFlangeHoleNumber)/2d);
        swModelLevel2.ChangeDim("D1@LPattern2", dataDto.YFlangeHoleNumber+1);
        swModelLevel2.ChangeDim("D3@LPattern2", dataDto.FlangeHoleDis);
        //swModelLevel2.ChangeDim("D5@Sketch9",3d/2);//板厚/2
    }
    #endregion

    #region DSH InnerFrame内框
    //MNSO0040
    public void MNSO0040(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netLength = dataDto.Length - 100d - 3d * 2d;
        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D2@Sketch1", netLength);
    }
    //MNSO0039
    public void MNSO0039(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netLength = dataDto.Length - 100d - 3d * 2d;
        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D2@Sketch1", netLength);
    }


    //MNSO0041
    public void MNSO0041(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netHeight = dataDto.Height - 100d - 3 * 2d - 33d * 2d;
        var yWeldNumber = (int)((netHeight-40d*2d) / 150d) + 1;
        var yWeldDis = (netHeight-40d*2d) / (yWeldNumber - 1);

        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D2@Base-Flange1", netHeight);
        swModelLevel2.ChangeDim("D1@LPattern3", yWeldNumber);
        swModelLevel2.ChangeDim("D3@LPattern3", yWeldDis);
    }

    //MNSO0037
    public void MNSO0037(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netLength = dataDto.Length - 100d - 3d*2d-28d*2d;
        //上下叶片(x方向)假设铆螺母间距150，计算铆螺母数量，距离边缘54
        var xNutNumber = (int)((dataDto.Length - 100d - 3d - 54d * 2d) / 150d) + 1;
        var xNutDis = (dataDto.Length - 100d - 3d - 54d * 2d) / (xNutNumber - 1);
        var bladeNumber = (int)((netLength - 45d) / 28.5d) + 1;//？？这样计算叶片数量？？

        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D2@Base-Flange1", netLength);
        swModelLevel2.ChangeDim("D1@LPattern4", xNutNumber);
        swModelLevel2.ChangeDim("D3@LPattern4", xNutDis);
        swModelLevel2.ChangeDim("D1@LPattern3", bladeNumber);
    }

    //MNSO0038
    public void MNSO0038(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netLength = dataDto.Length - 100d - 3d*2d -28d*2d;
        //上下叶片(x方向)假设铆螺母间距150，计算铆螺母数量，距离边缘54
        var xNutNumber = (int)((dataDto.Length - 100d - 3d - 54d * 2d) / 150d) + 1;
        var xNutDis = (dataDto.Length - 100d - 3d - 54d * 2d) / (xNutNumber - 1);
        var bladeNumber = (int)((netLength - 45d) / 28.5d) + 1;//？？这样计算叶片数量？？

        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D2@Base-Flange1", netLength);
        swModelLevel2.ChangeDim("D1@LPattern4", xNutNumber);
        swModelLevel2.ChangeDim("D3@LPattern4", xNutDis);
        swModelLevel2.ChangeDim("D1@LPattern3", bladeNumber);
    }

    //MNSO0042
    public void MNSO0042(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netHeight = dataDto.Height - 100d - 3 * 2d - 33d * 2d;
        var yWeldNumber = (int)((netHeight-40d*2d) / 150d) + 1;
        var yWeldDis = (netHeight-40d*2d) / (yWeldNumber - 1);

        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D4@Sketch1", netHeight);
        swModelLevel2.ChangeDim("D1@LPattern1", yWeldNumber);
        swModelLevel2.ChangeDim("D3@LPattern1", yWeldDis);
    }

    //MNSS0007
    public void MNSS0007(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netLength = dataDto.Length - 100d - 3d * 2d;
        var supportNumber = netLength<=700d ? 2 : netLength>700d && netLength<=1200d ? 3 : 4;
        var supportDis = (netLength - 270d) / (supportNumber - 1);
        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D2@Base-Flange1", netLength);
        swModelLevel2.ChangeDim("D1@LPattern4", supportNumber);
        swModelLevel2.ChangeDim("D3@LPattern4", supportDis);
    }

    //MNSS0008
    public void MNSS0008(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netLength = dataDto.Length - 100d - 3d * 2d;
        var supportNumber = netLength<=700d ? 2 : netLength>700d && netLength<=1200d ? 3 : 4;
        var supportDis = (netLength - 270d) / (supportNumber - 1);
        var bladeNumber = (int)((netLength-28d*2d - 45d) / 28.5d) + 1;//？？这样计算叶片数量？？
        var xDrainNumber = (int)((netLength-48d*2d) / 150d) + 1;
        var xDrainDis = (netLength-48d*2d) / (xDrainNumber - 1);

        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D2@Base-Flange1", netLength);
        swModelLevel2.ChangeDim("D1@LPattern4", supportNumber);
        swModelLevel2.ChangeDim("D3@LPattern4", supportDis);
        swModelLevel2.ChangeDim("D1@LPattern5", bladeNumber/2);
        swModelLevel2.ChangeDim("D2@LPattern5", bladeNumber/2);
        swModelLevel2.ChangeDim("D1@LPattern3", xDrainNumber);
        swModelLevel2.ChangeDim("D3@LPattern3", xDrainDis);
    }

    #endregion

    #region 叶片装配
    //DSHPipeLength
    public void DSHPipeLength(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netLength = dataDto.Length - 100d - 3d * 2d;
        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D1@Extrude1", netLength-28d*2d-15d);
    }

    public void DSHBladeHeight(AssemblyDoc swAssy, string suffix, string partName, DshDataDto dataDto)
    {
        var netHeight = dataDto.Height-100d - 36d * 2d- 3d;
        var swCompLevel2 = swAssy.GetComponentByNameWithSuffix(suffix, partName);
        var swModelLevel2 = swCompLevel2.GetModelDoc2() as ModelDoc2;
        swModelLevel2.ChangeDim("D2@Base-Flange1", netHeight);
    }

    #endregion

    public void Dispose()
    {
    }
}