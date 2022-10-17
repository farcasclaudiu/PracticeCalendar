using FluentAssertions;
using Mapster;
using PracticeCalendar.Application.PracticeEvents.Queries;
using PracticeCalendar.Domain.Entities.PracticeEvent;

namespace PracticeCalendar.UnitTests.Application.Mapping
{
    public class PracticeEventMappingTests
    {
        readonly string _eventTitle = "Event1";
        readonly string _eventDescription = "Description";
        readonly string _attendeeName = "Claudiu Farcas";
        readonly string _attendeeEmail = "claudiu.farcas@testingbee.com";
        readonly DateTime _eventStartTime = new DateTime(2022, 10, 10, 10, 25, 35);
        readonly DateTime _eventEndTime = new DateTime(2022, 10, 12, 9, 15, 00);

        public PracticeEventMappingTests()
        {
            TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
            TypeAdapterConfig.GlobalSettings.NewConfig<PracticeEventDto, PracticeEvent>()
                .ConstructUsing(src => new PracticeEvent(src.Title, src.Description, src.StartTime, src.EndTime));
        }

        [Fact]
        public void PracticeEvent_to_PracticeEventDto()
        {
            var practiceEvent = new PracticeEvent(_eventTitle, _eventDescription, _eventStartTime, _eventEndTime);
            var testAttendee = new Attendee(_attendeeName, _attendeeEmail);
            practiceEvent.AddAttendee(testAttendee);

            var dto = practiceEvent.Adapt<PracticeEventDto>();

            dto.Should().NotBeNull();
            dto.Title.Should().Be(_eventTitle);
            dto.Description.Should().Be(_eventDescription);
            dto.StartTime.Should().Be(_eventStartTime);
            dto.EndTime.Should().Be(_eventEndTime);
            dto.Attendees.Should().HaveCount(1);
            dto.Attendees[0].Name.Should().Be(_attendeeName);
            dto.Attendees[0].EmailAddress.Should().Be(_attendeeEmail);
            dto.Attendees[0].IsAttending.Should().BeFalse();
        }

        [Fact]
        public void PracticeEventDto_to_PracticeEvent()
        {
            var practiceEventDto = new PracticeEventDto
            {
                Title = _eventTitle,
                Description = _eventDescription,
                StartTime = _eventStartTime,
                EndTime = _eventEndTime
            };
            var attendeeDto = new AttendeeDto
            {
                Name = _attendeeName,
                EmailAddress = _attendeeEmail
            };
            practiceEventDto.Attendees.Add(attendeeDto);

            var entity = practiceEventDto.Adapt<PracticeEvent>();

            entity.Should().NotBeNull();
            entity.Title.Should().Be(_eventTitle);
            entity.Description.Should().Be(_eventDescription);
            entity.StartTime.Should().Be(_eventStartTime);
            entity.EndTime.Should().Be(_eventEndTime);
            entity.Attendees.Should().HaveCount(1);
            entity.Attendees[0].Name.Should().Be(_attendeeName);
            entity.Attendees[0].EmailAddress.Should().Be(_attendeeEmail);
            entity.Attendees[0].IsAttending.Should().BeFalse();
        }
    }
}
