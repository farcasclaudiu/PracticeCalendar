using PracticeCalendar.Domain.ValueObjects;

namespace PracticeCalendar.Application.Products.Queries
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string UnitPriceCurrency { get; set; } = Price.DEFAULT_CURRENCY;
    }
}