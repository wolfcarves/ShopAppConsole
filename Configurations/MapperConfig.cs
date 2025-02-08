using AutoMapper;
using ShopApp.Mapping;

namespace ShopApp.Configurations;

public class MapperConfig
{
    public static IMapper InitializeMapper()
    {
        var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OwnerProfile>();
            }
        );

        var mapper = new Mapper(config);

        return mapper;
    }
}