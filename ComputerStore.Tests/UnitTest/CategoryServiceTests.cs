using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ComputerStore.Application.Services;
using ComputerStore.Application.Interfaces;
using ComputerStore.Domain.Entities;

namespace ComputerStore.Tests.UnitTest
{
    public class CategoryServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new() { Id = 1, Name = "CPUs" },
                new() { Id = 2, Name = "Keyboards" }
            };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);

            var service = new CategoryService(mockRepo.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateAsync_CallsRepositoryWithCategory()
        {
            // Arrange
            var category = new Category
            {
                Name = "GPUs",
                Description = "Graphics Processing Units"
            };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Category>())).ReturnsAsync(category);

            var service = new CategoryService(mockRepo.Object);

            // Act
            await service.CreateAsync(category);

            // Assert
            mockRepo.Verify(r => r.AddAsync(It.Is<Category>(c => c.Name == "GPUs")), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectCategory()
        {
            // Arrange
            var category = new Category { Id = 5, Name = "Monitors" };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(category);

            var service = new CategoryService(mockRepo.Object);

            // Act
            var result = await service.GetByIdAsync(5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Monitors", result!.Name);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryWithCorrectData()
        {
            // Arrange
            var updatedCategory = new Category { Id = 7, Name = "Updated Mice" };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(r => r.UpdateAsync(7, updatedCategory)).ReturnsAsync(true);

            var service = new CategoryService(mockRepo.Object);

            // Act
            var result = await service.UpdateAsync(7, updatedCategory);

            // Assert
            Assert.True(result);
            mockRepo.Verify(r => r.UpdateAsync(7, updatedCategory), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepositoryAndReturnsDeletedCategory()
        {
            // Arrange
            var categoryToDelete = new Category { Id = 9, Name = "RAM" };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(r => r.DeleteAsync(9)).ReturnsAsync(categoryToDelete);

            var service = new CategoryService(mockRepo.Object);

            // Act
            var result = await service.DeleteAsync(9);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(9, result!.Id);
            mockRepo.Verify(r => r.DeleteAsync(9), Times.Once);
        }
    }
}
