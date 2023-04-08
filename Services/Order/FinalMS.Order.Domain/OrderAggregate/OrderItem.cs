using FinalMS.Order.Domain.Core;

namespace FinalMS.Order.Domain.OrderAggregate;

public class OrderItem: Entity
{
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string PictureUrl { get; private set; }
    public int ProductQuantity { get; private set; }
    public decimal Price { get; private set; }

    public OrderItem()
    {
        
    }

    public OrderItem(string productId, string productName, string pictureUrl, int productQuantity, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        PictureUrl = pictureUrl;
        ProductQuantity = productQuantity;
        Price = price;
    }

    public void UpdateOrderItem(string productName, string pictureUrl, int productQuantity, decimal price)
    {
        ProductName = productName;
        Price = price;
        ProductQuantity = productQuantity;
        PictureUrl = pictureUrl;
    }

    public decimal TotalPrice => this.ProductQuantity * this.Price;
}
