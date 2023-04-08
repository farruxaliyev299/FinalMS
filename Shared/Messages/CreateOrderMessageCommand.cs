namespace FinalMS.Shared.Messages;

public class CreateOrderMessageCommand
{
    public string BuyerId { get; set; }
    public string Province { get; set; }
    public string District { get; set; }
    public string Street { get; set; }
    public string Line { get; set; }
    public List<OrderItem> Items { get; set; }

    public CreateOrderMessageCommand()
    {
        Items = new List<OrderItem>();
    }
}

public class OrderItem
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public int ProductQuantity { get; set; }
    public decimal Price { get; set; }
}
