using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MilionApi.Models
{

    public class PropertyTrace
    {
        [BsonId]
        public ObjectId IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; }  = string.Empty;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdProperty { get; set; }  = string.Empty;
    }
}