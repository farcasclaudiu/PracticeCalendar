using Microsoft.AspNetCore.Mvc;
using PracticeCalendar.API.Controllers;
using PracticeCalendar.Application.PracticeEvents.Commands;
using PracticeCalendar.Application.PracticeEvents.Queries;
using PracticeCalendar.Application.PracticeEvents.Queries.GetPracticeEvents;

namespace PrcaticeCalendar.Controllers
{
    public class EventsController : ApiControllerBase
    {
        private readonly ILogger<EventsController> logger;

        public EventsController(ILogger<EventsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<List<PracticeEventDto>>> Get()
        {
            return await Mediator.Send(new GetPracticeEventsQuery());
        }

        [HttpPost(Name = "Create practice event")]
        public async Task<ActionResult<PracticeEventDto>> CreateEvent(PracticeEventDto eventModel)
        {
            return await Mediator.Send(new CreatePracticeEventCommand(eventModel));
        }

        [HttpPut(Name = "Update practice event")]
        public async Task<ActionResult<PracticeEventDto>> UpdateEvent(PracticeEventDto eventModel)
        {
            return await Mediator.Send(new UpdatePracticeEventCommand(eventModel));
        }

        [HttpDelete(Name = "Delete practice event")]
        public async Task<ActionResult> DeleteEvent(int practiceEventId)
        {
            await Mediator.Send(new DeletePracticeEventCommand(practiceEventId));

            return Ok();
        }

        [HttpPost]
        [Route("accept/{eventId}/{attendeeId}")]
        public async Task<ActionResult> AttendeeAcceptEvent(int eventId, int attendeeId)
        {
            await Mediator.Send(new AttendeeAcceptEventCommand(eventId, attendeeId));

            return Ok();
        }

        [HttpPost]
        [Route("decline/{eventId}/{attendeeId}")]
        public async Task<ActionResult> AttendeeDeclineEvent(int eventId, int attendeeId)
        {
            await Mediator.Send(new AttendeeDeclineEventCommand(eventId, attendeeId));

            return Ok();
        }
    }
}