using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ComputerStore.Application.Services;
using ComputerStore.Domain.Entities;
using ComputerStore.Application.Interfaces;

namespace ComputerStore.Tests.UnitTest
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
<<<<<<< HEAD
           
=======
            
>>>>>>> edc802387b21bd173eec587af82adce2556b4e1a
            var products = new List<Product>
            {
                new() { Id = 1, Name = "CPU", Price = 299.99M },
                new() { Id = 2, Name = "Keyboard", Price = 49.99M }
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            var service = new ProductService(mockRepo.Object);

            
            var result = await service.GetAllAsync();

            
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateAsync_CallsRepositoryWithProduct()
        {
            
            var product = new Product
            {
                Name = "GPU",
                Price = 499.99M
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>())).ReturnsAsync(product);

            var service = new ProductService(mockRepo.Object);

            
            await service.CreateAsync(product);

            
            mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p.Name == "GPU")), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectProduct()
        {
            
            var product = new Product { Id = 10, Name = "Monitor", Price = 150.00M };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(10)).ReturnsAsync(product);

            var service = new ProductService(mockRepo.Object);

            
            var result = await service.GetByIdAsync(10);

            
            Assert.NotNull(result);
            Assert.Equal("Monitor", result!.Name);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryWithCorrectData()
        {
            
            var updatedProduct = new Product { Id = 5, Name = "Updated Mouse", Price = 29.99M };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.UpdateAsync(5, updatedProduct)).ReturnsAsync(true);

            var service = new ProductService(mockRepo.Object);

<<<<<<< HEAD
            
=======
           
>>>>>>> edc802387b21bd173eec587af82adce2556b4e1a
            var result = await service.UpdateAsync(5, updatedProduct);

            
            Assert.True(true);
            mockRepo.Verify(r => r.UpdateAsync(5, updatedProduct), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryAndReturnsDeletedProduct()
        {
<<<<<<< HEAD
            
=======
           
>>>>>>> edc802387b21bd173eec587af82adce2556b4e1a
            var productToDelete = new Product { Id = 3, Name = "RAM", Price = 80.00M };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.DeleteAsync(3)).ReturnsAsync(productToDelete);

            var service = new ProductService(mockRepo.Object);

            
            var result = await service.DeleteAsync(3);

            
            Assert.NotNull(result);
            Assert.Equal(3, result!.Id);
            mockRepo.Verify(r => r.DeleteAsync(3), Times.Once);
        }
    }
}


