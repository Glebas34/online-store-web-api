using avito.Models;

namespace avito.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        bool DeleteProduct(Product product);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool Save();
        bool ProductExists(int id);
        public bool ProductExists(int id, string name, decimal price);
        bool DeleteProducts(List<Product> products);
        decimal GetProductRating(int id);
    }
}
