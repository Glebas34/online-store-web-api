using avito.Models;

namespace avito.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        Task<List<ShoppingCartItem>> GetShoppingCartItems();
        Task<ShoppingCartItem> GetShoppingCartItem(int id);
        Task<List<ShoppingCartItem>> GetShoppingCartItemsOfProduct(int productId);
        Task<List<ShoppingCartItem>> GetItemsOfShoppingCart(int shoppingCartId);
        bool DeleteShoppingCartItem(ShoppingCartItem shoppingCartItem);
        bool CreateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        bool UpdateShoppingCartItem(ShoppingCartItem shoppingCartItem);
        bool Save();
        bool ShoppingCartItemExists(int id);
        bool DeleteShoppingCartItems(List<ShoppingCartItem> items);
    }
}
