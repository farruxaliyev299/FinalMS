using FinalMS.Catalog.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FinalMS.Catalog.DTOs;

public class ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string[] Ingredients { get; set; }
    public string Photo { get; set; }
    public string CategoryId { get; set; }
    public string StoreId { get; set; }
    public DateTime CreatedTime { get; set; }

    public CategoryDto Category { get; set; }
    public Store Store { get; set; }
}
