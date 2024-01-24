using avito.Models;

namespace avito.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<List<ShoppingCart>> GetShoppingCarts();
        Task<ShoppingCart> GetShoppingCart(int id);
        bool DeleteShoppingCart(ShoppingCart shoppingCart);
        bool CreateShoppingCart(ShoppingCart shoppingCart);
        bool UpdateShoppingCart(ShoppingCart shoppingCart);
        bool Save();
        bool ShoppingCartExists(int id);
    }
}
