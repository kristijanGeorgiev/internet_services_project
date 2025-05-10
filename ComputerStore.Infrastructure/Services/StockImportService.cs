using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;

namespace ComputerStore.Application.Services
{
    public class StockImportService : IStockImportService
    {
        private readonly IStockRepository _repository;

        public StockImportService(IStockRepository repository)
        {
            _repository = repository;
        }

        public async Task ImportAsync(List<StockDto> importProducts)
        {
            if (importProducts == null || !importProducts.Any())
            {
                throw new ArgumentException("No stock data provided.");
            }

            await _repository.ImportAsync(importProducts);
        }
    }
}
