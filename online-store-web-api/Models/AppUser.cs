using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace avito.Models
{
    public class AppUser
    {
        public int Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        [ForeignKey(nameof(ShoppingCart))]
        public int? ShoppingCartId { get; set; } 
        public ShoppingCart? ShoppingCart { get; set;}
        public ICollection<Review> reviews { get; set; }
    }
}
