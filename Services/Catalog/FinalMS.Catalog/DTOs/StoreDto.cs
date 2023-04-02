using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FinalMS.Catalog.DTOs;

public class StoreDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

}
