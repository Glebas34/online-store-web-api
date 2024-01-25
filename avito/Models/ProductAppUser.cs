namespace avito.Models
{
    public class ProductAppUser
    {
        public int ProductId { get; set; }
        public int AppUserId { get; set; }
        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}
