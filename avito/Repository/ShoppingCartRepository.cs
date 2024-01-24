using avito.Data;
using avito.Interfaces;
using avito.Models;
using Microsoft.EntityFrameworkCore;

namespace avito.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;
        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.Add(shoppingCart);
            return Save();
        }

        public bool DeleteShoppingCart(ShoppingCart shoppingCart)
        {
            _context.Remove(shoppingCart);
            return Save();
        }

        async public Task<ShoppingCart> GetShoppingCart(int id)
        {
            return await _context.ShoppingCarts.FindAsync(id);
        }

        public async Task<List<ShoppingCart>> GetShoppingCarts()
        {
            return await _context.ShoppingCarts.ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ShoppingCartExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.Update(shoppingCart);
            return Save();
        }
    }
}
