using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PracticeCalendar.API.Model;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities;
using PracticeCalendar.Domain.Entities.Specifications;

namespace PrcaticeCalendar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IRepository<PracticeEvent> eventsRepo;
        private readonly IMapper mapper;
        private readonly ILogger<EventsController> logger;

        public EventsController(IRepository<PracticeEvent> eventsRepo, 
            IMapper mapper,
            ILogger<EventsController> logger)
        {
            this.eventsRepo = eventsRepo;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<List<EventModel>>> Get()
        {

            var spec = new PracticeEventsWithAttendees();
            var repoList = await eventsRepo.ListAsync(spec);
            var evList = repoList.Select(x=> {
                var model = mapper.Map<EventModel>(x);
                model.Attendees = x.Attendees.Select(m=>mapper.Map<AttendeeModel>(m)).ToArray();
                return model;
            })
            .ToList();
            return evList;
        }

        [HttpPost(Name = "Create practice event")]
        public async Task<IActionResult> CreateEvent(EventModel eventModel)
        {
            var practiceEvent = new PracticeEvent(eventModel.Title, eventModel.Description);
            foreach (var att in eventModel.Attendees)
            {
                practiceEvent.AddAttendee(new Attendee(att.Name, att.EmailAddress));
            }            
            var result = await eventsRepo.AddAsync(practiceEvent);
            await eventsRepo.SaveChangesAsync();
            return Ok(mapper.Map<EventModel>(result));
        }

        [HttpPut(Name = "Update practice event")]
        public async Task<IActionResult> UpdateEvent(EventModel eventModel)
        {
            var practiceEvent = await eventsRepo.GetByIdAsync(eventModel.Id);
            if(practiceEvent == null)
            {
                return NotFound();
            }
            practiceEvent.UpdateTitleAndDescription(eventModel.Title, eventModel.Description);
            await eventsRepo.UpdateAsync(practiceEvent);
            await eventsRepo.SaveChangesAsync();
            return Ok(mapper.Map<EventModel>(practiceEvent));
        }

        [HttpDelete(Name = "Delete practice event")]
        public async Task<IActionResult> DeleteEvent(int practiceEventId)
        {
            var org = await eventsRepo.GetByIdAsync(practiceEventId);
            if (org == null)
            {
                return NotFound();
            }
            await eventsRepo.DeleteAsync(org);
            await eventsRepo.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("accept/{eventId}/{attendeeId}")]
        public async Task<IActionResult> AttendeeAcceptEvent(int eventId, int attendeeId)
        {
            var spec = new PracticeEventByIdWithAttendees(eventId);
            var practiceEvent = await eventsRepo.FirstOrDefaultAsync(spec);
            if (practiceEvent == null)
            {
                return NotFound();
            }
            practiceEvent.AttendeeAcceptEvent(attendeeId);
            await eventsRepo.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("decline/{eventId}/{attendeeId}")]
        public async Task<IActionResult> AttendeeDeclineEvent(int eventId, int attendeeId)
        {
            var spec = new PracticeEventByIdWithAttendees(eventId);
            var practiceEvent = await eventsRepo.FirstOrDefaultAsync(spec);
            if (practiceEvent == null)
            {
                return NotFound();
            }
            practiceEvent.AttendeeDeclineEvent(attendeeId);
            await eventsRepo.SaveChangesAsync();
            return Ok();
        }
    }
}