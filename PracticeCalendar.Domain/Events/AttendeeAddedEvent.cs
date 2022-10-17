using PracticeCalendar.Domain.Common;
using PracticeCalendar.Domain.Entities.PracticeEvent;

namespace PracticeCalendar.Domain.Events
{
    public sealed class AttendeeAddedEvent : DomainEventBase
    {
        public AttendeeAddedEvent(PracticeEvent eventAggregate, Attendee addedAtendee)
        {
            EventAggregate = eventAggregate;
            AddedAtendee = addedAtendee;
        }

        public PracticeEvent EventAggregate { get; }
        public Attendee AddedAtendee { get; set; }
    }
}
