using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace avito.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Product>? Products { get; set; }
        public ShoppingCart? ShoppingCart { get; set;}
    }
}
