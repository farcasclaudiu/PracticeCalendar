using PracticeCalendar.Domain.Common;

namespace PracticeCalendar.Domain.ValueObjects
{
    public class Price : ValueObject
    {
        public static Price Empty = new Price();
        public static string DEFAULT_CURRENCY = "EUR";

        public decimal Value { get; set; }
        public string Currency { get; set; } = Price.DEFAULT_CURRENCY;
    }
}