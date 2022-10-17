using Ardalis.Specification;

namespace PracticeCalendar.Domain.Entities.PracticeEvent.Specifications
{
    public class PracticeEventsWithAttendeesSpecification : Specification<PracticeEvent>
    {
        public PracticeEventsWithAttendeesSpecification()
        {
            Query.AsNoTracking()
                .Include(x => x.Attendees);
        }
    }
}
