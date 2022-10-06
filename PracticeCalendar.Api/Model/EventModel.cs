using AutoMapper;
using PracticeCalendar.Domain.Entities;

namespace PracticeCalendar.API.Model
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public AttendeeModel[] Attendees { get; set; } = new AttendeeModel[0];
    }

    public class AttendeeModel
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAttending { get; set; }
    }

    public class EventModelProfile : Profile
    {
        public EventModelProfile()
        {
            CreateMap<PracticeEvent, EventModel>();
            CreateMap<EventModel, PracticeEvent>();
            CreateMap<Attendee, AttendeeModel>();
        }
    }
}
