using ComputerStore.Application.Interfaces;
using ComputerStore.Domain.Entities;
using Core.Application.DTOs;

namespace Infrastructure.Services
{
    public class DiscountCalculatorService : IDiscountCalculatorService
    {
        private readonly IProductRepository _productRepository;

        public DiscountCalculatorService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<DiscountResultDto> CalculateDiscountAsync(IEnumerable<BasketItemDto> basket)
        {
            var productIds = basket.Select(x => x.ProductId).ToList();
            var products = await _productRepository.GetByIdsAsync(productIds);

            var basketProducts = from item in basket
                                 from i in Enumerable.Range(0, item.Quantity)
                                 join product in products on item.ProductId equals product.Id
                                 from category in product.Categories
                                 select new
                                 {
                                     ProductId = product.Id,
                                     CategoryID = category.Id,
                                     ProductPrice = product.Price
                                 };

            var categoryGroups = basketProducts
                .GroupBy(x => x.CategoryID)
                .Where(g => g.Count() > 1)
                .ToList();

            decimal discount = 0m;
            foreach (var group in categoryGroups)
            {
                var firstProduct = group.First();
                discount += firstProduct.ProductPrice * 0.05m;
            }


            // Total Price
            decimal total = basketProducts.Sum(x => x.ProductPrice);

            return new DiscountResultDto
            {
                TotalPrice = total,
                DiscountApplied = discount
            };
        }

        }
    }