using ComputerStore.Application.Interfaces;
using Core.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IDiscountCalculatorService _discountService;

        public BasketController(IDiscountCalculatorService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost("calculate-discount")]
        public async Task<ActionResult<DiscountResultDto>> CalculateDiscount([FromBody] List<BasketItemDto> basket)
        {
            try
            {
                var result = await _discountService.CalculateDiscountAsync(basket);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}