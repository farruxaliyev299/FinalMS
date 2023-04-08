using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinalMS.Catalog.Models;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    public string Description { get; set; } 
    public string Photo { get; set; }
    public string[] Ingredients { get; set; }
    public string CategoryId { get; set; }
    public string StoreId { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedTime { get; set; }

    [BsonIgnore]
    public Category Category { get; set; }

    [BsonIgnore]
    public Store Store { get; set; }

}
