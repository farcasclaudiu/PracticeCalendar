using PracticeCalendar.Domain.Common;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.ValueObjects;

namespace PracticeCalendar.Domain.Entities.Product
{
    public class Product : EntityBase, IAggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public Price UnitPrice { get; set; } = Price.Empty;
    }
}