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
                cfg.AddProfile<StoreProfile>();
                cfg.AddProfile<ProductProfile>();
                cfg.AddProfile<CategoryProfile>();
            }
        );

        var mapper = new Mapper(config);

        return mapper;
    }
}