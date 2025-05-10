using ComputerStore.Application.Interfaces;
using ComputerStore.Domain.Entities;

namespace ComputerStore.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var categoryNames = product.Categories
                .Select(c => c.Name.Trim())
                .Distinct()
                .ToList();

            var existingCategories = await _repository.GetExistingCategoriesByNameAsync(categoryNames);

            var newCategories = categoryNames
                .Except(existingCategories.Select(c => c.Name))
                .Select(name => new Category { Name = name })
                .ToList();

            if (newCategories.Any())
            {
                await _repository.AddCategoriesAsync(newCategories);
            }

        
            var allCategories = existingCategories.Concat(newCategories).ToList();

     
            product.Categories = allCategories;

            return await _repository.AddAsync(product);
        }


        public async Task<bool> UpdateAsync(int id, Product updatedProduct)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null) return false;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Quantity = updatedProduct.Quantity;


            existingProduct.Categories.Clear();

    
            foreach (var cat in updatedProduct.Categories)
            {
                var existingCategory = await _repository.GetCategoryByNameAsync(cat.Name);
                if (existingCategory != null)
                {
                    existingProduct.Categories.Add(existingCategory);
                }
                else
                {
                    existingProduct.Categories.Add(new Category { Name = cat.Name });
                }
            }

            return await _repository.UpdateAsync(id, existingProduct);
        }


        public async Task<Product?> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
