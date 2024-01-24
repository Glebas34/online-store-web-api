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

        public bool CreateShoppinCartItem(ShoppingCartItem shoppingCartItem)
        {
            throw new NotImplementedException();
        }

        public bool CreateShoppingCart(ShoppingCartItem shoppingCartItem)
        {
            _context.Add(shoppingCartItem);
            return Save();
        }

        public bool DeleteShoppinCartItem(ShoppingCartItem shoppingCartItem)
        {
            throw new NotImplementedException();
        }

        public bool DeleteShoppingCart(ShoppingCartItem shoppingCartItem)
        {
            _context.Remove(shoppingCartItem);
            return Save();
        }

        public Task<ShoppingCartItem> GetShoppinCartItem(int id)
        {
            throw new NotImplementedException();
        }

        async public Task<ShoppingCartItem> GetShoppingCart(int id)
        {
            return await _context.ShoppingCartItems.FindAsync(id);
        }

        public Task<List<ShoppingCartItem>> GetShoppingCartItems()
        {
            throw new NotImplementedException();
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

        public bool ShoppingCartItemExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateShoppinCartItem(ShoppingCartItem shoppingCartItem)
        {
            throw new NotImplementedException();
        }

        public bool UpdateShoppingCart(ShoppingCart shoppingCartItem)
        {
            _context.Update(shoppingCartItem);
            return Save();
        }
    }
}
