using AutoMapper;

namespace FinalMS.Order.Application.Mapping;

public static class ObjectMapper
{
    private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingConfig>();
        });
        return config.CreateMapper();
    });

    public static IMapper Mapper => lazy.Value;
}
