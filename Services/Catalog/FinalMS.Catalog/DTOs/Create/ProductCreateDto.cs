namespace FinalMS.Catalog.DTOs.Create;

public class ProductCreateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string[] Ingredients { get; set; }
    public string Photo { get; set; }
    public string CategoryId { get; set; }
    public string StoreId { get; set; }
}
