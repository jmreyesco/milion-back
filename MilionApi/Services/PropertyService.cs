using MongoDB.Driver;
using MongoDB.Bson;
using MilionApi.Models;
using MilionApi.Dtos;


namespace MilionApi.Services
    {
        public class PropertyService
        {
            private readonly IMongoCollection<Property> _properties;
            private readonly IMongoCollection<PropertyImage> _images;
    
            public PropertyService(IConfiguration config)
            {
                var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("milliodb");
            _properties = database.GetCollection<Property>("Property");
            _images = database.GetCollection<PropertyImage>("PropertyImage");
        }

        public async Task<List<PropertyDto>> GetPropertiesAsync(string name, string address, decimal? minPrice, decimal? maxPrice)
        {
            var filterBuilder = Builders<Property>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(name))
                filter &= filterBuilder.Regex("Name", new BsonRegularExpression(name, "i"));
            if (!string.IsNullOrEmpty(address))
                filter &= filterBuilder.Regex("Address", new BsonRegularExpression(address, "i"));
            if (minPrice.HasValue)
                filter &= filterBuilder.Gte("Price", minPrice.Value);
            if (maxPrice.HasValue)
                filter &= filterBuilder.Lte("Price", maxPrice.Value);

            var properties = await _properties.Find(filter).ToListAsync();

            var dtos = new List<PropertyDto>();
            foreach (var prop in properties)
            {
                var image = await _images.Find(i => i.IdProperty == prop.IdProperty.ToString() && i.Enabled)
                                         .FirstOrDefaultAsync();
                dtos.Add(new PropertyDto
                {
                    IdOwner = prop.IdOwner,
                    Name = prop.Name,
                    Address = prop.Address,
                    Price = prop.Price,
                    Image = image?.File
                });
            }
            return dtos;
        }
    }

}
