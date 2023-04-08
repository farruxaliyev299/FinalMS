namespace FinalMS.Shared.Messages;

public class ProductNameChangedEvent
{
    public string ProductId { get; set; }
    public string UpdatedProductName { get; set; }
}
