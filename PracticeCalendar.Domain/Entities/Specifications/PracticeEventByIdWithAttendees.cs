using Ardalis.Specification;

namespace PracticeCalendar.Domain.Entities.Specifications
{
    public class PracticeEventByIdWithAttendees : Specification<PracticeEvent>
    {
        public PracticeEventByIdWithAttendees(int eventId)
        {
            Query.Where(x=>x.Id == eventId)
                .Include(x => x.Attendees);
        }
    }
}
