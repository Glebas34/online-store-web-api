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


        public bool CreateShoppingCartItem(int shoppingCartId,ShoppingCartItem shoppingCartItem)
        {
            var shoppingCart = _context.ShoppingCarts.Find(shoppingCartId);
            var shoppingCartItemShoppingCart = new ShoppingCartItemShoppingCart {
                ShoppingCart = shoppingCart,
                ShoppingCartItem = shoppingCartItem,
            };
            _context.Add(shoppingCartItemShoppingCart);
            _context.Add(shoppingCartItem);
            return Save();
        }


        public bool DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _context.Remove(shoppingCartItem);
            return Save();
        }

        async public Task<ShoppingCartItem> GetShoppingCartItem(int id)
        {
            return await _context.ShoppingCartItems.FindAsync(id);
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems()
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

        public bool UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            _context.Update(shoppingCartItem);
            return Save();
        }

    }
}
