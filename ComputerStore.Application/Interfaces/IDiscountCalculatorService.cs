using Core.Application.DTOs;

namespace ComputerStore.Application.Interfaces
{
    public interface IDiscountCalculatorService
    {
        Task<DiscountResultDto> CalculateDiscountAsync(IEnumerable<BasketItemDto> basket);
    }
}