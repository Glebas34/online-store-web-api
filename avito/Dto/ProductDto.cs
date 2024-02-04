using avito.Models;

namespace avito.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public uint Avaliable {  get; set; }
        public decimal Price { get; set; }
    }
}
