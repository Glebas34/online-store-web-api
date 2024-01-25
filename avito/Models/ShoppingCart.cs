namespace avito.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public ICollection<ShoppingCartItem>? Items { get; set; }
        public AppUser User { get; set; }
    }
}
