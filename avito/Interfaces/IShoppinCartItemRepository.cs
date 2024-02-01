using avito.Models;

namespace avito.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        Task<List<ShoppingCartItem>> GetShoppingCartItems();
        Task<ShoppingCartItem> GetShoppingCartItem(int id);
        bool DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem);
        bool CreateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        bool UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        bool Save();
        bool ShoppingCartItemExists(int id);
    }
}
