using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;
using ComputerStore.Domain.Entities;

namespace ComputerStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

       
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productService.CreateAsync(product);

            var createdDto = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, createdDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductDto ProductDto)
        {

            var product = _mapper.Map<Product>(ProductDto);
            product.Id = id;

            var success = await _productService.UpdateAsync(id, product);
            if (!success)
                return NotFound(new { message = "Product not found for update." });
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (deleted == null)
                return NotFound(new { message = "Product not found for deletion." });

            return NoContent();
        }
    }
}
