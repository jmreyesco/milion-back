using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;



namespace MilionApi.Models
{
public class Owner
{
    [BsonId]
    public ObjectId IdOwner { get; set; }
    public string Name { get; set; }  = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public DateTime Birthday { get; set; } 
}

}