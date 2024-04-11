using System.ComponentModel.DataAnnotations.Schema;

namespace avito.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
