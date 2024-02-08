using avito.Data;
using avito.Interfaces;
using avito.Models;
using Microsoft.EntityFrameworkCore;

namespace avito.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
            
        }

        public bool DeleteProduct(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public bool DeleteProducts(List<Product> products)
        {
            _context.RemoveRange(products);
            return Save();
        }

        async public Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public decimal GetProductRating(int id)
        {
            var product = _context.Products.Find(id);
            var reviews = product.Reviews;
            decimal sum = 0;
            if (reviews != null)
            {
                foreach (var review in reviews)
                {
                    sum += review.Rating;
                }
                return sum / reviews.Count();
            }
            return 0;
        }

        async public Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
        
        public bool ProductExists(int id, string name, decimal price)
        {
            return _context.Products.Any(p => p.Id == id&&p.Name==name&&p.Price==price);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0?true:false;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
