using Mapster;
using MediatR;
using PracticeCalendar.Application.PracticeEvents.Queries;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities;

namespace PracticeCalendar.Application.PracticeEvents.Commands
{
    public record CreatePracticeEventCommand : IRequest<PracticeEventDto>
    {
        public CreatePracticeEventCommand(PracticeEventDto eventDto)
        {
            Event = eventDto;
        }

        public PracticeEventDto Event { get; }
    }

    public class CreatePracticeEventCommandHandler : IRequestHandler<CreatePracticeEventCommand, PracticeEventDto>
    {
        private readonly IRepository<PracticeEvent> eventsRepo;

        public CreatePracticeEventCommandHandler(IRepository<PracticeEvent> eventsRepo)
        {
            this.eventsRepo = eventsRepo;
        }

        public async Task<PracticeEventDto> Handle(CreatePracticeEventCommand request, CancellationToken cancellationToken)
        {
            var input = request.Event;
            var practiceEvent = new PracticeEvent(input.Title, input.Description);
            foreach (var att in input.Attendees)
            {
                practiceEvent.AddAttendee(new Attendee(att.Name, att.EmailAddress));
            }
            var result = await eventsRepo.AddAsync(practiceEvent, cancellationToken);
            await eventsRepo.SaveChangesAsync(cancellationToken);
            return result.Adapt<PracticeEventDto>();
        }
    }
}
