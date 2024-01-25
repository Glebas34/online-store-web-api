namespace avito.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public AppUser Seller { get; set; }
        public float Price { get; set; }
    }
}
