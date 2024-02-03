using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace avito.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Product>? Products { get; set; }
        [ForeignKey(nameof(ShoppingCart))]
        public int ShoppingCartId { get; set; } 
        public ShoppingCart? ShoppingCart { get; set;}
    }
}
