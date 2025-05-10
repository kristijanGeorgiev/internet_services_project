using ComputerStore.Application.Interfaces;
using ComputerStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ComputerStore.Infrastructure.Data;

namespace ComputerStore.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(int id, Category updatedCategory)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            category.Name = updatedCategory.Name;
            category.Description = updatedCategory.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}

