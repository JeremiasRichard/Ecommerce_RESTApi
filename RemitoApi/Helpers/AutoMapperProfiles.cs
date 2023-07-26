using AutoMapper;
using RemitoApi.DTOs;
using RemitoApi.Entities;

namespace RemitoApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<ProductType, ProductTypeCreateDTO>().ReverseMap();
            
            CreateMap<ProductOrigin, ProductOriginCreateDTO>().ReverseMap();
            
            CreateMap<CategoryType, CategoryTypeCreateDTO>().ReverseMap();
            
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            
            CreateMap<Product,ProductCreateDTO>().ReverseMap();
            
            CreateMap<ProductToShowDTO,Product>().ReverseMap().
                ForMember(dest => dest.CategoryTypeName, 
                opt => opt.MapFrom(src => src.Category.CategoryType.Name));
            
            CreateMap<CategoryToShowDTO, Category>().ReverseMap().
                ForMember(dest => dest.CategoryTypeName, opt =>
                opt.MapFrom(src => src.CategoryType.Name));
             
        }
    }
}
