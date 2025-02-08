using AutoMapper;
using ShopApp.DTO;

namespace ShopApp.Mapping;

public class StoreProfile : Profile
{
    public StoreProfile()
    {
        CreateMap<Store, StoreDTO>()
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Owner.Id));
    }
}