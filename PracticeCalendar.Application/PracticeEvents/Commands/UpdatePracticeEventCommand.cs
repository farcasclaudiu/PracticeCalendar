using Mapster;
using MediatR;
using PracticeCalendar.Application.PracticeEvents.Queries;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities;
using PracticeCalendar.Domain.Exceptions;

namespace PracticeCalendar.Application.PracticeEvents.Commands
{
    public record UpdatePracticeEventCommand : IRequest<PracticeEventDto>
    {
        public UpdatePracticeEventCommand(PracticeEventDto eventDto)
        {
            Event = eventDto;
        }

        public PracticeEventDto Event { get; }
    }

    public class UpdatePracticeEventCommandHandler : IRequestHandler<UpdatePracticeEventCommand, PracticeEventDto>
    {
        private readonly IRepository<PracticeEvent> eventsRepo;

        public UpdatePracticeEventCommandHandler(IRepository<PracticeEvent> eventsRepo)
        {
            this.eventsRepo = eventsRepo;
        }

        public async Task<PracticeEventDto> Handle(UpdatePracticeEventCommand request, CancellationToken cancellationToken)
        {
            var eventModel = request.Event;
            var practiceEvent = await eventsRepo.GetByIdAsync(eventModel.Id, cancellationToken);
            if (practiceEvent == null)
            {
                throw new PracticeEventNotFoundException();
            }
            practiceEvent.UpdateTitleAndDescription(eventModel.Title, eventModel.Description);
            await eventsRepo.UpdateAsync(practiceEvent, cancellationToken);
            await eventsRepo.SaveChangesAsync(cancellationToken);
            return practiceEvent.Adapt<PracticeEventDto>();
        }
    }
}
