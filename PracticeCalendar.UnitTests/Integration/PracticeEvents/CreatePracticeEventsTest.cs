using FluentAssertions;
using Moq;
using PracticeCalendar.Application.PracticeEvents.Commands;
using PracticeCalendar.Application.PracticeEvents.Queries;
using PracticeCalendar.Domain.Common;
using static PracticeCalendar.UnitTests.Integration.Testing;

namespace PracticeCalendar.UnitTests.Integration.PracticeEvents
{
    public class CreatePracticeEventsTest : BaseTest
    {
        [Fact]
        public async Task ShouldCreatePracticeEvent()
        {
            await RunBeforeAnyTests();

            var query = new CreatePracticeEventCommand(new PracticeEventDto
            {
                Title = "Some title",
                Description = "Some desc",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Attendees = {
                    new AttendeeDto
                    {
                        Name = "Claudiu F",
                        EmailAddress = "claudiuf@somewhere.com"
                    },
                    new AttendeeDto
                    {
                        Name = "Claudiu F 2",
                        EmailAddress = "claudiuf2@somewhere.com"
                    }
                }
            });

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Id.Should().NotBe(0);
            result.Attendees.Count.Should().Be(2);

            //check domain events count
            domainEventServiceMock.Verify(x=>x.Publish(It.IsAny<DomainEventBase>()), Times.Exactly(2));
        }
    }
}
