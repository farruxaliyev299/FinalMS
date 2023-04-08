using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FinalMS.Catalog.Models;

public class Store
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}
