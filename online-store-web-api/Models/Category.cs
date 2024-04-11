namespace avito.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Product> Products { get; set; }
    }
}
