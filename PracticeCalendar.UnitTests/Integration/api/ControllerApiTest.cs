using System.Net;
using FluentAssertions;
using PracticeCalendar.Domain.Entities.PracticeEvent;
using PracticeCalendar.UnitTests.Integration;
using static PracticeCalendar.UnitTests.Integration.Testing;

public class ControllerApiTest : BaseTest
{
    [Fact]
    public async Task GetAllEventsEmptyTestAsync()
    {
        await RunBeforeAnyTests();
        var http = GetHttpClient();

        var httpResponse = await http.GetAsync("/api/events");

        httpResponse.IsSuccessStatusCode.Should().BeTrue();
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseStr = await httpResponse.Content.ReadAsStringAsync();
        responseStr.Should().Be("[]");
    }

    [Fact]
    public async Task GetAllEventsManyTestAsync()
    {
        await RunBeforeAnyTests();
        await AddAsync(new PracticeEvent("Test Event", "Event description",
                DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1))
        {
            Id = 1,
            Attendees = {
                    new Attendee("Claudiu F", "claudiuf@busybee.com")
                    {
                      Id = 1
                    }
                }
        });

        var http = GetHttpClient();

        var httpResponse = await http.GetAsync("/api/events");

        httpResponse.IsSuccessStatusCode.Should().BeTrue();
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseStr = await httpResponse.Content.ReadAsStringAsync();
        responseStr.Length.Should().Be(257);
    }
}