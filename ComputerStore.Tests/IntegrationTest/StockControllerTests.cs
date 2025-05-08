using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using ComputerStore.Application.DTOs;
using Microsoft.VisualStudio.TestPlatform.TestHost;
namespace ComputerStore.Tests.Integration
{
    public class StockControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public StockControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_ImportStock_ReturnsOk()
        {
            
            var importProducts = new List<StockDto>
    {
        new StockDto
        {
            Name = "Test GPU",
            Categories = new List<string> { "GPU" },
            Price = 200,
            Quantity = 5
        }
    };

          
            var response = await _client.PostAsJsonAsync("/api/Stock/import", importProducts);

            
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode, $"Expected success status code but got {response.StatusCode}");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

    }
}
