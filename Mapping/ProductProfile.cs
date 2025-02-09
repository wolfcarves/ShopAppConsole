using AutoMapper;
using ShopApp.Entities;
using ShopApp.DTO;

namespace ShopApp.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.Store.Id))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src =>
                 src.ProductCategories.FirstOrDefault().CategoryId));
    }
}