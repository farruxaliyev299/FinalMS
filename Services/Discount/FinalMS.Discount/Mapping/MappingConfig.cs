using AutoMapper;
using FinalMS.Discount.DTOs;
using FinalMS.Discount.DTOs.Create;
using FinalMS.Discount.DTOs.Update;

namespace FinalMS.Discount.Mapping;

public class MappingConfig: Profile
{
    public MappingConfig()
    {
        CreateMap<Models.Discount, DiscountDto>().ReverseMap();
        CreateMap<Models.Discount, DiscountCreateDto>().ReverseMap();
        CreateMap<Models.Discount, DiscountUpdateDto>().ReverseMap();
    }
}
