using FluentAssertions;
using PracticeCalendar.Domain.Entities.PracticeEvent;

namespace PracticeCalendar.UnitTests.Domain
{
    public class PracticeEventTest
    {
        readonly string _eventTitle = "Event1";
        readonly string _eventDescription = "Description";
        readonly string _attendeeName = "Claudiu Farcas";
        readonly string _atendeeEmail = "claudiu.farcas@testingbee.com";

        [Fact]
        public void InitializeProperties()
        {
            var practiceEvent = new PracticeEvent(_eventTitle, _eventDescription, DateTime.Now, DateTime.Now);
            practiceEvent.Title.Should().Be(_eventTitle);
            practiceEvent.Description.Should().Be(_eventDescription);
            practiceEvent.Attendees.Should().HaveCount(0);
        }

        [Fact]
        public void InitializeWithNullShouldThrowException()
        {
            Action act = () =>
            {
                var practiceEvent = new PracticeEvent(null!, _eventDescription, DateTime.Now, DateTime.Now);
            };
            act.Should().Throw<ArgumentNullException>();

            act = () =>
            {
                var practiceEvent = new PracticeEvent(_eventTitle, null!, DateTime.Now, DateTime.Now);
            };
            act.Should().Throw<ArgumentNullException>();

            act = () =>
            {
                var practiceEvent = new PracticeEvent(string.Empty, _eventDescription, DateTime.Now, DateTime.Now);
            };
            act.Should().Throw<ArgumentException>();

            act = () =>
            {
                var practiceEvent = new PracticeEvent(_eventTitle, string.Empty, DateTime.Now, DateTime.Now);
            };
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AddAttendeeToEvent()
        {
            var practiceEvent = new PracticeEvent(_eventTitle, _eventDescription, DateTime.Now, DateTime.Now);
            var testAttendee = new Attendee(_attendeeName, _atendeeEmail);
            practiceEvent.AddAttendee(testAttendee);
            practiceEvent.Attendees.Should().HaveCount(1);
        }
    }
}