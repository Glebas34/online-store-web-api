﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using avito.Models;
using Microsoft.EntityFrameworkCore.Internal;

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
        public DbSet<Review> Reviews { get;set;}
    }
}
