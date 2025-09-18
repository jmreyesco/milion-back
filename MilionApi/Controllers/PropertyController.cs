using Microsoft.AspNetCore.Mvc;
using MilionApi.Services;
using MilionApi.Dtos;

namespace MilionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly PropertyService _service;

        public PropertyController(PropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PropertyDto>>> Get(
            [FromQuery] string? name,
            [FromQuery] string? address,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var result = await _service.GetPropertiesAsync(name, address, minPrice, maxPrice);
            return Ok(result);
        }
    }
}