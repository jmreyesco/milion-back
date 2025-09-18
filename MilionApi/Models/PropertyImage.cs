using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MilionApi.Models
{
    public class PropertyImage
    {
        [BsonId]
        public ObjectId IdPropertyImage { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdProperty { get; set; }  = string.Empty;
        public string File { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}