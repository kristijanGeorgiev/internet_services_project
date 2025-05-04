using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;
using ComputerStore.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ComputerStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockImportService _importService;
        private readonly ILogger<StockController> _logger;
        private readonly JsonLoaderService _jsonLoader;
        public StockController(IStockImportService importService, ILogger<StockController> logger, JsonLoaderService jsonLoader)
        {
            _importService = importService;
            _logger = logger;
            _jsonLoader = jsonLoader;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportStock([FromBody] List<StockDto> products)
        {
            if (products == null || !products.Any())
                return BadRequest("No products provided.");

            await _importService.ImportAsync(products);
            return Ok("Stock imported successfully.");
        }
        [HttpGet("load-json")]
        public IActionResult LoadJson()
        {
            var products = _jsonLoader.LoadStockFromJson();
            return Ok(products);
        }
        [HttpPost("upload-json")]
        public async Task<IActionResult> UploadJson(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = new StreamReader(file.OpenReadStream());
            var json = await stream.ReadToEndAsync();

            var products = JsonSerializer.Deserialize<List<StockDto>>(json);

            return Ok(products);
        }

    }
}