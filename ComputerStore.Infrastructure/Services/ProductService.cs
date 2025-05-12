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
            return await _repository.AddAsync(product);
        }


        public async Task<bool> UpdateAsync(int id, Product updatedProduct)
        {
            return await _repository.UpdateAsync(id, updatedProduct);
        }


        public async Task<Product?> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
