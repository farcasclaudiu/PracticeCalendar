using MediatR;

namespace PracticeCalendar.Domain.Common
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEventBase
    {
        public TDomainEvent DomainEvent { get; }

        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
