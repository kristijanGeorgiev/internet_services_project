﻿using ComputerStore.Application.Interfaces;
using ComputerStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ComputerStore.Infrastructure.Data;

namespace ComputerStore.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Categories)
                .ToListAsync();
        }


        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddAsync(Product product)
        {
           
            foreach (var category in product.Categories)
            {
                _context.Attach(category);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(int id, Product updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<List<Product>> GetByIdsAsync(List<int> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .Include(p => p.Categories)
                .ToListAsync();
        }
        public async Task<List<Category>> GetExistingCategoriesByNameAsync(List<string> names)
        {
            return await _context.Categories
                .Where(c => names.Contains(c.Name))
                .ToListAsync();
        }
        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task AddCategoriesAsync(List<Category> categories)
        {
            _context.Categories.AddRange(categories);
            await _context.SaveChangesAsync();
        }

    }
}
