using ComputerStore.Domain.Entities;

namespace ComputerStore.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<bool> UpdateAsync(int id, Product product);
        Task<Product?> DeleteAsync(int id);
        Task<List<Product>> GetByIdsAsync(List<int> productIds);
        Task<List<Category>> GetExistingCategoriesByNameAsync(List<string> names);
        Task<Category?> GetCategoryByNameAsync(string name);
        Task AddCategoriesAsync(List<Category> categories);
    }
}
