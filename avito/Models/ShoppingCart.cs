namespace avito.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public ICollection<ShoppingCartItem>? Items { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
