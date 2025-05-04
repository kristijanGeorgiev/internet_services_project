using System.Net;
using System.Net.Http.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using ComputerStore.API;
using ComputerStore.Application.DTOs;

namespace ComputerStore.Tests.Integration
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/products");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostProduct_ReturnsCreated_AndCanRetrieve()
        {
            // Arrange
            var product = new ProductDto
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 99.99m
            };

            // Act
            var postResponse = await _client.PostAsJsonAsync("/api/products", product);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            var created = await postResponse.Content.ReadFromJsonAsync<ProductDto>();
            Assert.NotNull(created);
            Assert.Equal("Test Product", created!.Name);

            // GET by ID
            var getResponse = await _client.GetAsync($"/api/products/{created.Id}");
            var fetched = await getResponse.Content.ReadFromJsonAsync<ProductDto>();

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.Equal(created.Id, fetched!.Id);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent()
        {
            // First create
            var product = new ProductDto
            {
                Name = "Original Product",
                Description = "Before Update"
            };

            var postResponse = await _client.PostAsJsonAsync("/api/products", product);
            var created = await postResponse.Content.ReadFromJsonAsync<ProductDto>();

            // Modify
            created!.Name = "Updated Product";
            created.Description = "After Update";

            // PUT
            var putResponse = await _client.PutAsJsonAsync($"/api/products/{created.Id}", created);
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

            // GET
            var getResponse = await _client.GetAsync($"/api/products/{created.Id}");
            var updated = await getResponse.Content.ReadFromJsonAsync<ProductDto>();

            Assert.Equal("Updated Product", updated!.Name);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent()
        {
            // First create
            var product = new ProductDto
            {
                Name = "To Delete",
                Description = "Will be deleted"
            };

            var postResponse = await _client.PostAsJsonAsync("/api/products", product);
            var created = await postResponse.Content.ReadFromJsonAsync<ProductDto>();

            // DELETE
            var deleteResponse = await _client.DeleteAsync($"/api/products/{created!.Id}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Confirm it is deleted
            var getResponse = await _client.GetAsync($"/api/products/{created.Id}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}