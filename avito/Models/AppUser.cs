using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace avito.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Product>? Products { get; set; }
        public int? ShoppingCartId {get;set;}
        public decimal? Rating { get;set;}
        public ShoppingCart? ShoppingCart { get; set;}
        public ICollection<Review>? OwnReviews { get; set; }
        public ICollection<Review>? UserReviews { get; set; }


    }
}
