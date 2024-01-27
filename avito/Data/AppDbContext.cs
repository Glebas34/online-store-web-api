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
        public DbSet<ShoppingCart> ShoppingCarts { get;set;}
        public DbSet<ShoppingCartItem> ShoppingCartItems { get;set;}
        public DbSet<Category> Categories { get;set;}
        public DbSet<ShoppingCartItemShoppingCart> ShoppingCartItemShoppingCart { get;set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShoppingCartItemShoppingCart>()
                .HasKey(x => new { x.ShoppingCartId, x.ShoppingCartItemId });
            builder.Entity<ShoppingCartItemShoppingCart>()
                .HasOne(i => i.ShoppingCartItem)
                .WithMany(sc => sc.ShoppingCartItemShoppingCarts)
                .HasForeignKey(sc => sc.ShoppingCartId);
            builder.Entity<ShoppingCartItemShoppingCart>()
                .HasOne(sc => sc.ShoppingCart)
                .WithMany(i => i.ShoppingCartItemShoppingCarts)
                .HasForeignKey(i => i.ShoppingCartItemId);
        }
    }
}
