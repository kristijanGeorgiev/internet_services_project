using ComputerStore.Application.DTOs;

namespace ComputerStore.Application.Interfaces
{
    public interface IStockRepository
    {
        Task ImportAsync(List<StockDto> importProducts);
    }
}
