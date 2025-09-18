namespace MilionApi.Dtos
{

    public class PropertyDto
    {
        public string IdOwner { get; set; }  = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
    }

}