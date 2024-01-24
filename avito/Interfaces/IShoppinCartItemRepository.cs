using avito.Models;

namespace avito.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        Task<List<ShoppingCartItem>> GetShoppingCartItems();
        Task<ShoppingCartItem> GetShoppinCartItem(int id);
        bool DeleteShoppinCartItem(ShoppingCartItem shoppingCartItem);
        bool CreateShoppinCartItem(ShoppingCartItem shoppingCartItem);
        bool UpdateShoppinCartItem(ShoppingCartItem shoppingCartItem);
        bool Save();
        bool ShoppingCartItemExists(int id);
    }
}
