using FinalMS.Order.Infrastructure;
using FinalMS.Shared.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FinalMS.Order.Application.Consumers;

public class ProductNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
{
    private readonly OrderDbContext _context;

    public ProductNameChangedEventConsumer(OrderDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
    {
        var orderItems = await _context.OrderItems.Where(oi => oi.ProductId == context.Message.ProductId).ToListAsync();

        foreach (var item in orderItems)
        {
            item.UpdateOrderItem(context.Message.UpdatedProductName, item.PictureUrl, item.ProductQuantity, item.Price);
        }

        await _context.SaveChangesAsync();
    }
}
