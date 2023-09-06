using AutoMapper;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.DTOs.Secutiry;
using EcommerceRESTApi.Entities;
using EcommerceRESTApi.Models;

namespace EcommerceRESTApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductType, ProductTypeCreateDTO>().ReverseMap();
            CreateMap<ProductType, ProductTypeToShowDTO>().ReverseMap();
            CreateMap<ProductOrigin, ProductOriginCreateDTO>().ReverseMap();
            CreateMap<ProductOrigin, ProductOriginToShowDTO>().ReverseMap();

            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<ProductToShowDTO, Product>().ReverseMap()
                .ForMember(dest => dest.ProductTypeName,
                opt => opt.MapFrom(src => src.ProductType.Name)).
                ForMember(dest => dest.ProductOriginName,
                opt => opt.MapFrom(src => src.ProductOrigin.Name)).
                ForMember(dest => dest.Name, opt => opt.MapFrom(c => c.Category.Name));

            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<CategoryToShowDTO, Category>().ReverseMap();
           
            CreateMap<Items, ItemAdditionDTO>().ReverseMap();
            CreateMap<ItemToShowDTO, Items>().ReverseMap()
                .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.ProductName));

            CreateMap<DeliveryNote, DeliveryNoteCreateDTO>().ReverseMap();
            CreateMap<DeliveryNoteCloseDTO, DeliveryNote>().ReverseMap();
        }
    }
}
