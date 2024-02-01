using Microsoft.EntityFrameworkCore;

namespace avito.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }   
        public ShoppingCart? ShoppingCart { get; set; }
    }
}
