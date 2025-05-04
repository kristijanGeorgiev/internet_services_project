using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using ComputerStore.Application.DTOs;

namespace ComputerStore.Infrastructure.Services
{
    public class JsonLoaderService
    {
        private readonly IWebHostEnvironment _env;

        public JsonLoaderService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<StockDto> LoadStockFromJson()
        {
            string filePath = Path.Combine(_env.WebRootPath, "data", "stock.json");

            if (!File.Exists(filePath))
                return new List<StockDto>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<StockDto>>(json) ?? new List<StockDto>();
        }
    }
}
