namespace PracticeCalendar.Domain.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEventBase domainEvent);
    }
}
