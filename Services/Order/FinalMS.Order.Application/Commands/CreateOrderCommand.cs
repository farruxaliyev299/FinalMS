using FinalMS.Order.Application.DTOs;
using FinalMS.Order.Domain.OrderAggregate;
using FinalMS.Order.Infrastructure;
using FinalMS.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FinalMS.Order.Application.Commands;

public class CreateOrderCommand: IRequest<Response<CreatedOrderDto>>
{
    public AddressDto Address { get; set; }
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAdress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.Line);

            var newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAdress);

            foreach (var item in request.OrderItems)
            {
                newOrder.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.ProductQuantity, item.PictureUrl);
            }

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, StatusCodes.Status201Created);
        }
    }
}
