using avito.Models;

namespace avito.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        bool DeleteCategory(Category category);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool Save();
        bool CategoryExists(int id);
    }
}
