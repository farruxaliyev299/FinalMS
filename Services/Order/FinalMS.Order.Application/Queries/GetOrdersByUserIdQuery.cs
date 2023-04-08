using FinalMS.Order.Application.DTOs;
using FinalMS.Order.Application.Mapping;
using FinalMS.Order.Infrastructure;
using FinalMS.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FinalMS.Order.Application.Queries;

public class GetOrdersByUserIdQuery: IRequest<Response<List<OrderDto>>>
{
    public string UserId { get; set; }

    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).Where(o => o.BuyerId == request.UserId).ToListAsync();

            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), StatusCodes.Status200OK);
            }

            return Response<List<OrderDto>>.Success(ObjectMapper.Mapper.Map<List<OrderDto>>(orders), StatusCodes.Status200OK);
        }
    }
}
