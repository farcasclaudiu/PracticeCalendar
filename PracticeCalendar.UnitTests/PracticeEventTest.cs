using FluentAssertions;
using PracticeCalendar.Domain.Entities;

namespace PracticeCalendar.UnitTests
{
    public class PracticeEventTest
    {
        string _eventTitle = "Event1";
        string _eventDescription = "Description";

        string _attendeeName = "Claudiu Farcas";
        string _atendeeEmail = "claudiu.farcas@testingbee.com";

        [Fact]
        public void InitializeProperties()
        {
            var practiceEvent = new PracticeEvent(_eventTitle, _eventDescription);
            practiceEvent.Title.Should().Be(_eventTitle);
            practiceEvent.Description.Should().Be(_eventDescription);
            practiceEvent.Attendees.Should().HaveCount(0);
        }

        [Fact]
        public void InitializeWithNullShouldThrowException()
        {
            Action act = () => {
                var practiceEvent = new PracticeEvent(null, _eventDescription);
            };
            act.Should().Throw<ArgumentNullException>();

            act = () => {
                var practiceEvent = new PracticeEvent(_eventTitle, null);
            };
            act.Should().Throw<ArgumentNullException>();

            act = () => {
                var practiceEvent = new PracticeEvent(string.Empty, _eventDescription);
            };
            act.Should().Throw<ArgumentException>();

            act = () => {
                var practiceEvent = new PracticeEvent(_eventTitle, string.Empty);
            };
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AddAttendeeToEvent()
        {
            var practiceEvent = new PracticeEvent(_eventTitle, _eventDescription);
            var testAttendee = new Attendee(_attendeeName, _atendeeEmail);
            practiceEvent.AddAttendee(testAttendee);
            practiceEvent.Attendees.Should().HaveCount(1);
        }
    }
}