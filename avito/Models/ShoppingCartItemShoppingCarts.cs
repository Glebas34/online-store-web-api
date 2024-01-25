namespace avito.Models
{
    public class ShoppingCartItemShoppingCarts
    {
        public int ShoppingCartItemId {  get; set; }
        public int ShoppingCartId {  get; set; }
        public ShoppingCartItem ShoppingCartItem { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
