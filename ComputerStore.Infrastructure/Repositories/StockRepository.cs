using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;
using ComputerStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ComputerStore.Infrastructure.Data;

namespace ComputerStore.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ImportAsync(List<StockDto> importProducts)
        {
            foreach (var item in importProducts)
            {
                var categoryNames = item.Categories.Select(c => c.Trim()).ToList();

                var categories = new List<Category>();
                foreach (var catName in categoryNames)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == catName);
                    if (category == null)
                    {
                        category = new Category { Name = catName };
                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync(); // Save to get the Id
                    }
                    categories.Add(category);
                }

                var existingProduct = await _context.Products
                    .Include(p => p.Categories)
                    .FirstOrDefaultAsync(p => p.Name == item.Name);

                if (existingProduct != null)
                {
                    existingProduct.Quantity += item.Quantity;
                    existingProduct.Price = item.Price;
                }
                else
                {
                    var newProduct = new Product
                    {
                        Name = item.Name,
                        Price = item.Price,
                        Description = item.Description,
                        Categories = categories,
                        Quantity = item.Quantity
                    };
                    _context.Products.Add(newProduct);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
