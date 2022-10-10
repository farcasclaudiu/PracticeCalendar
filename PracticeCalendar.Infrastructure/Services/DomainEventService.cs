using MediatR;
using Microsoft.Extensions.Logging;
using PracticeCalendar.Domain.Common;
using PracticeCalendar.Domain.Common.Interfaces;

namespace PracticeCalendar.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> logger;
        private readonly IPublisher mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }
        public async Task Publish(DomainEventBase domainEvent)
        {
            logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            await mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEventBase domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
        }
    }
}
