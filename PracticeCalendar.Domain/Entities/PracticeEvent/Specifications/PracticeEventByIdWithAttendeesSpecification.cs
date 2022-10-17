using Ardalis.Specification;

namespace PracticeCalendar.Domain.Entities.PracticeEvent.Specifications
{
    public class PracticeEventByIdWithAttendeesSpecification : Specification<PracticeEvent>
    {
        public PracticeEventByIdWithAttendeesSpecification(int eventId)
        {
            Query.Where(x => x.Id == eventId)
                .Include(x => x.Attendees);
        }
    }
}
