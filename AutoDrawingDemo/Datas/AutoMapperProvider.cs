/*using AutoMapper;
using Prism.Ioc;
using System;

namespace AutoDrawingDemo.Datas;
public interface IAutoMapperProvider
{
    IMapper GetMapper();
}
public class AutoMapperProvider : IAutoMapperProvider
{
    private readonly MapperConfiguration _configuration;
    public AutoMapperProvider(IContainerProvider containerProvider)
    {
        _configuration= new MapperConfiguration(configure =>
        {
            configure.ConstructServicesUsing(containerProvider.Resolve);
            //扫描profile文件
            configure.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
        });
    }
    public IMapper GetMapper()
    {
        return _configuration.CreateMapper();
    }
}*/