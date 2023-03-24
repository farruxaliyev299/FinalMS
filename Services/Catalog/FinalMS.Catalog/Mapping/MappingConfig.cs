using AutoMapper;
using FinalMS.Catalog.DTOs;
using FinalMS.Catalog.DTOs.Create;
using FinalMS.Catalog.DTOs.Update;
using FinalMS.Catalog.Models;

namespace FinalMS.Catalog.Mapping;

public class MappingConfig: Profile
{
    public MappingConfig()
    {
        CreateMap<Category,CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap();

        CreateMap<Product,ProductDto>().ReverseMap();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();
        
        CreateMap<Store,StoreDto>().ReverseMap();
        CreateMap<Store, StoreCreateDto>().ReverseMap();
        CreateMap<Store, StoreUpdateDto>().ReverseMap();
    }
}