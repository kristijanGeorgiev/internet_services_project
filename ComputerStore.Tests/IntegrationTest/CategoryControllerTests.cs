using System.Net;
using System.Net.Http.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using ComputerStore.Domain.Entities;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ComputerStore.Application.DTOs;

namespace ComputerStore.Tests.Integration
{
    public class CategoryControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CategoryControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllCategories_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync("/api/categories");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostCategory_ReturnsOk_AndCanRetrieve()
        {
            // Arrange
            var newCategory = new Category
            {
                Name = "Test Category",
                Description = "For testing",
                Products = new List<Product>() // Optional if Products are required
            };

            // Act
            var postResponse = await _client.PostAsJsonAsync("/api/categories", newCategory);

            // Assert Post
            postResponse.EnsureSuccessStatusCode();
            var created = await postResponse.Content.ReadFromJsonAsync<Category>();
            Assert.NotNull(created);
            Assert.Equal("Test Category", created!.Name);

            // Act Get by ID
            var getResponse = await _client.GetAsync($"/api/categories/{created.Id}");
            var fetched = await getResponse.Content.ReadFromJsonAsync<Category>();

            // Assert Get
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.Equal(created.Id, fetched!.Id);
        }
        [Fact]
        public async Task UpdateCategory_ReturnsNoContent()
        {
            // First create
            var category = new CategoryDto
            {
                Name = "Original Category",
                Description = "Before Update"
            };

            var postResponse = await _client.PostAsJsonAsync("/api/categories", category);
            var created = await postResponse.Content.ReadFromJsonAsync<CategoryDto>();

            // Modify
            created!.Name = "Updated Category";
            created.Description = "After Update";

            // PUT
            var putResponse = await _client.PutAsJsonAsync($"/api/categories/{created.Id}", created);
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

            // GET
            var getResponse = await _client.GetAsync($"/api/categories/{created.Id}");
            var updated = await getResponse.Content.ReadFromJsonAsync<CategoryDto>();

            Assert.Equal("Updated Category", updated!.Name);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsNoContent()
        {
            // First create
            var category = new CategoryDto
            {
                Name = "To Delete",
                Description = "Will be deleted"
            };

            var postResponse = await _client.PostAsJsonAsync("/api/categories",  category);
            var created = await postResponse.Content.ReadFromJsonAsync<CategoryDto>();

            // DELETE
            var deleteResponse = await _client.DeleteAsync($"/api/categories/{created!.Id}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            // Confirm it is deleted
            var getResponse = await _client.GetAsync($"/api/categories/{created.Id}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
