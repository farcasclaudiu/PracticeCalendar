using FluentAssertions;
using PracticeCalendar.Application.PracticeEvents.Queries.GetPracticeEvents;
using PracticeCalendar.Domain.Entities;

using static PracticeCalendar.UnitTests.Integration.Testing;

namespace PracticeCalendar.UnitTests.Integration.PracticeEvents
{
    public class GetPracticeEventsTest : BaseTest
    {

        [Fact]
        public async Task ShouldReturnZeroResult()
        {
            await RunBeforeAnyTests();

            var query = new GetPracticeEventsQuery();

            var result = await SendAsync(query);

            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task ShouldReturnAllListsAndItems()
        {
            await RunBeforeAnyTests();

            await AddAsync(new PracticeEvent("Test Event", "Event description")
            {
                Id = 1,
                Attendees = { 
                    new Attendee("Claudiu F", "claudiuf@busybee.com")
                    {
                      Id = 1
                    }
                }
            });

            var query = new GetPracticeEventsQuery();

            var result = await SendAsync(query);

            result.Should().HaveCount(1);
            result.First().Attendees.Should().HaveCount(1);
        }
    }
}
