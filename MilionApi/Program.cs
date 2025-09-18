using MilionApi.Services;
using MilionApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Configuración de MongoDB
builder.Services.AddSingleton<PropertyService>();

builder.Services.AddCors();

builder.Services.AddControllers();
var app = builder.Build();

// Inicialización de datos (seed)
void SeedDatabase(IConfiguration config)
{
    var client = new MongoClient(config.GetConnectionString("MongoDb"));
    var db = client.GetDatabase("milliodb");

    var owners = db.GetCollection<Owner>("Owner");
    var properties = db.GetCollection<Property>("Property");
    var images = db.GetCollection<PropertyImage>("PropertyImage");
    var traces = db.GetCollection<PropertyTrace>("PropertyTrace");

    if (owners.CountDocuments(Builders<Owner>.Filter.Empty) == 0)
    {
        var owner = new Owner
        {
            Name = "Juan Perez",
            Address = "Calle Falsa 123",
            Photo = "https://via.placeholder.com/150",
            Birthday = DateTime.Parse("1980-01-01")
        };
        owners.InsertOne(owner);

        var property = new Property
        {
            Name = "Casa Moderna",
            Address = "Calle Falsa 123",
            Price = 350000,
            CodeInternal = "A001",
            Year = 2020,
            IdOwner = owner.IdOwner.ToString()
        };
        properties.InsertOne(property);

        var image = new PropertyImage
        {
            IdProperty = property.IdProperty.ToString(),
            File = "https://via.placeholder.com/300x200",
            Enabled = true
        };
        images.InsertOne(image);

        var trace = new PropertyTrace
        {
            DateSale = DateTime.Now,
            Name = "Venta inicial",
            Value = 350000,
            Tax = 5000,
            IdProperty = property.IdProperty.ToString()
        };
        traces.InsertOne(trace);
    }
}

// Llamada a la función de seed, metodo de inicializacion de datos
SeedDatabase(builder.Configuration);


//Cofiguracion de cors
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.MapControllers();
app.Run();