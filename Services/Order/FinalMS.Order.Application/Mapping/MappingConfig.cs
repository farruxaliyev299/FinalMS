using AutoMapper;
using FinalMS.Order.Application.DTOs;

namespace FinalMS.Order.Application.Mapping;

public class MappingConfig: Profile
{
    public MappingConfig()
    {
        CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
        CreateMap<Domain.OrderAggregate.OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Domain.OrderAggregate.Address, AddressDto>().ReverseMap();
    }
}
