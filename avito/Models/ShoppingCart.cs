﻿namespace avito.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public ICollection<ShoppingCartItemShoppingCart> ShoppingCartItemShoppingCarts { get; set; }
        public AppUser User { get; set; }
    }
}
