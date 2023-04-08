namespace FinalMS.Order.Application.DTOs;

public class OrderItemDto
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public int ProductQuantity { get; set; }
    public decimal Price { get; set; }
}
