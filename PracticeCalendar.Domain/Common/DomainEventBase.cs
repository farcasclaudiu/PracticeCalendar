namespace PracticeCalendar.Domain.Common
{
    public class DomainEventBase
    {
        public DateTime EventDate { get; protected set; } = DateTime.UtcNow;
        public bool IsPublished { get; set; }
    }
}
