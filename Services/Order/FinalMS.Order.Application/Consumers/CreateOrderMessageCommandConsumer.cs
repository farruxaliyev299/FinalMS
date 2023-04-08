using FinalMS.Order.Infrastructure;
using FinalMS.Shared.Messages;
using MassTransit;

namespace FinalMS.Order.Application.Consumers;

public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
{
    private readonly OrderDbContext _context;

    public CreateOrderMessageCommandConsumer(OrderDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
    {
        var newAdress = new Domain.OrderAggregate.Address(context.Message.Province, context.Message.District, context.Message.Street, context.Message.Line);

        var newOrder = new Domain.OrderAggregate.Order(context.Message.BuyerId, newAdress);

        foreach (var oItem in context.Message.Items)
        {
            newOrder.AddOrderItem(oItem.ProductId, oItem.ProductName, oItem.Price, oItem.ProductQuantity, oItem.PictureUrl);
        }

        await _context.Orders.AddAsync(newOrder);

        await _context.SaveChangesAsync();
    }
}
