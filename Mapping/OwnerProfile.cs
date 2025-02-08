using AutoMapper;
using ShopApp.DTO.Owner;
using ShopApp.Entities;

namespace ShopApp.Mapping;

public class OwnerProfile : Profile
{
    public OwnerProfile()
    {
        CreateMap<Owner, OwnerDTO>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.OwnerDetails.Address))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.OwnerDetails.Phone));
    }
}