using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using avito.Models;

namespace avito.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products{get;set;}
        public DbSet<Review> Reviews { get;set;}
        public DbSet<ShoppingCart> ShoppingCarts { get;set;}
        public DbSet<ShoppingCartItem> ShoppingCartItems { get;set;}
        public DbSet<Category> Categories { get;set;}
        public DbSet<ShoppingCartItemShoppingCarts> ShoppingCartItemShoppingCarts { get;set;}
    }
}
