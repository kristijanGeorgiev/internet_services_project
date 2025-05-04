using Xunit;
using Moq;
using ComputerStore.Application.Services;
using ComputerStore.Application.Interfaces;
using ComputerStore.Application.DTOs;

namespace ComputerStore.Tests.UnitTest
{
    public class StockImportServiceTests
    {
        [Fact]
        public async Task ImportAsync_CallsRepositoryImportOnce()
        {

            var mockRepo = new Mock<IStockRepository>();
            var service = new StockImportService(mockRepo.Object);

            var testData = new List<StockDto>
            {
                new()
                {
                    Name = "Test CPU",
                    Description = "Test description",
                    Categories = new List<string> { "CPU" },
                    Price = 199.99M,
                    Quantity = 5
                }
            };


            await service.ImportAsync(testData);


            mockRepo.Verify(repo => repo.ImportAsync(It.IsAny<List<StockDto>>()), Times.Once);
        }
    }
}