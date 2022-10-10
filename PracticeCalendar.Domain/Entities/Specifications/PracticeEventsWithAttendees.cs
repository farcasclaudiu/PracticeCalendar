using Ardalis.Specification;

namespace PracticeCalendar.Domain.Entities.Specifications
{
    public class PracticeEventsWithAttendees : Specification<PracticeEvent>
    {
        public PracticeEventsWithAttendees()
        {
            Query.AsNoTracking()
                .Include(x => x.Attendees);
        }
    }
}
