using AutoMapper;
using ShopApp.Entities;
using ShopApp.DTO;

namespace ShopApp.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDTO>();
    }
}