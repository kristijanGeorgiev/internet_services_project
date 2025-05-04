using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;

namespace ComputerStore.Application.Services;

public class StockImportService : IStockImportService
{
    private readonly IStockRepository _repository;

    public StockImportService(IStockRepository repository)
    {
        _repository = repository;
    }

    public async Task ImportAsync(List<StockDto> importProducts)
    {
        await _repository.ImportAsync(importProducts);
    }
}