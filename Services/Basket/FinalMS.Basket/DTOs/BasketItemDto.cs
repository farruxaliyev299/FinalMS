namespace FinalMS.Basket.DTOs;

public class BasketItemDto
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string StoreId { get; set; }
    public string StoreName { get; set; }
    public int Quantity { get; set; }

}
