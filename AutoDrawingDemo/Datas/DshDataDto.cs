namespace AutoDrawingDemo.Datas;

public class DshDataDto:BaseDto
{
    private string name;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged();
        }
    }
    #region 长宽高
    private double length;
    public double Length
    {
        get => length;
        set { length = value; OnPropertyChanged(); }
    }

    private double width;
    public double Width
    {
        get => width;
        set { width = value; OnPropertyChanged(); }
    }

    private double height;
    public double Height
    {
        get => height;
        set
        {
            height = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region 法兰孔
    private double flangeHoleDia;
    public double FlangeHoleDia
    {
        get => flangeHoleDia;
        set { flangeHoleDia = value; OnPropertyChanged(); }
    }
    private double flangeHoleDis;
    public double FlangeHoleDis
    {
        get => flangeHoleDis;
        set { flangeHoleDis = value; OnPropertyChanged(); }
    }
    private int xFlangeHoleNumber;
    public int XFlangeHoleNumber
    {
        get => xFlangeHoleNumber;
        set { xFlangeHoleNumber = value; OnPropertyChanged(); }
    }
    private int yFlangeHoleNumber;
    public int YFlangeHoleNumber
    {
        get => yFlangeHoleNumber;
        set { yFlangeHoleNumber = value; OnPropertyChanged(); }
    }
    #endregion




    #region 额外属性
    private bool isSelected;
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;
            OnPropertyChanged();
        }
    }
    #endregion
}