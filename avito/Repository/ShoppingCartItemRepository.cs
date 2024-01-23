using avito.Data;
using avito.Interfaces;
using avito.Models;
using Microsoft.EntityFrameworkCore;

namespace avito.Repository
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly AppDbContext _context;
        public ShoppingCartItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CreateShoppingCart(ShoppingCartItem shoppingCartItem)
        {
            _context.Add(shoppingCartItem);
            return Save();
        }

        public bool DeleteShoppingCart(ShoppingCartItem shoppingCartItem)
        {
            _context.Remove(shoppingCartItem);
            return Save();
        }

        async public Task<ShoppingCartItem> GetShoppingCart(int id)
        {
            return await _context.ShoppingCartItems.FindAsync(id);
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCarts()
        {
            return await _context.ShoppingCartItems.ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateShoppingCart(ShoppingCart shoppingCartItem)
        {
            _context.Update(shoppingCartItem);
            return Save();
        }
    }
}
