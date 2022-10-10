using MediatR;
using PracticeCalendar.Domain.Common;
using PracticeCalendar.Domain.Events;
using PracticeCalendar.Domain.Interfaces;

namespace PracticeCalendar.Application.PracticeEvents.Events
{
    public class AttendeeAddedEventNotification : INotificationHandler<DomainEventNotification<AttendeeAddedEvent>>
    {
        private readonly IEmailSender emailSender;

        public AttendeeAddedEventNotification(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public async Task Handle(DomainEventNotification<AttendeeAddedEvent> notification, CancellationToken cancellationToken)
        {
            var sendTo = notification.DomainEvent.AddedAtendee.EmailAddress;
            await emailSender.SendEmailAsync(sendTo, "system", 
                "You have been added to the event", "Confirmed you have been added to the event.");
        }
    }
}
