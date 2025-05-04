using ComputerStore.Application.DTOs;

namespace ComputerStore.Application.Interfaces
{
    public interface IStockImportService
    {
        Task ImportAsync(List<StockDto> importProducts);
    }
}