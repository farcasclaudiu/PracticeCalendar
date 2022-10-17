using PracticeCalendar.Domain.Common;
using PracticeCalendar.Domain.Entities.PracticeEvent;

namespace PracticeCalendar.Domain.Events
{
    public sealed class EventUpdateTitleAndDescriptionEvent : DomainEventBase
    {
        public EventUpdateTitleAndDescriptionEvent(PracticeEvent practiceEvent)
        {
            PracticeEvent = practiceEvent;
        }

        public PracticeEvent PracticeEvent { get; }
    }
}
