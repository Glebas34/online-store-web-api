﻿using avito.Data;
using avito.Interfaces;
using avito.Models;
using Microsoft.EntityFrameworkCore;

namespace avito.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) {
            _context = context;
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(p => p.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        async public Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        async public Task<Category> GetCategory(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public bool Save()
        {
            var saved=_context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
