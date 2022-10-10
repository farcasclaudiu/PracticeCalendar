using PracticeCalendar.Domain.Common;
using PracticeCalendar.Domain.Entities;

namespace PracticeCalendar.Domain.Events
{
    public sealed class AttendeeDeclinedEvent : DomainEventBase
    {
        public AttendeeDeclinedEvent(PracticeEvent practiceEvent, Attendee attendee)
        {
            PracticeEvent = practiceEvent;
            Attendee = attendee;
        }

        public PracticeEvent PracticeEvent { get; }
        public Attendee Attendee { get; }
    }
}
