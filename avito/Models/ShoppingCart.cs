namespace avito.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public AppUser User { get; set; }
    }
}
