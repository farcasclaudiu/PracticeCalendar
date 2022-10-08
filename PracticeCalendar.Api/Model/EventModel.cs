using AutoMapper;
using PracticeCalendar.Domain.Entities;

namespace PracticeCalendar.API.Model
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public AttendeeModel[] Attendees { get; set; } = Array.Empty<AttendeeModel>();
    }

    public class AttendeeModel
    {
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
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
