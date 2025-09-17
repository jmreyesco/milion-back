using Xunit;
using MilionApi.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace MilionApi.Tests
{
    public class PropertyServiceTests
    {
        [Fact]
        public async Task GetPropertiesAsync_WithNoFilters_ReturnsList()
        {
            // Arrange: Usa una configuración real
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("ConnectionStrings:MongoDb", "mongodb://localhost:27017")
                })
                .Build();

            var service = new PropertyService(config);

            // Act
            var result = await service.GetPropertiesAsync(null, null, null, null);

            // Assert
            Assert.NotNull(result);
        }
    }
}