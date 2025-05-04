namespace Core.Application.DTOs
{
    public class DiscountResultDto
    {
        public decimal TotalPrice { get; set; }
        public decimal DiscountApplied { get; set; }
        public decimal FinalPrice => TotalPrice - DiscountApplied;
    }
}