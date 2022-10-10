using MediatR;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities;
using PracticeCalendar.Domain.Entities.Specifications;
using PracticeCalendar.Domain.Exceptions;

namespace PracticeCalendar.Application.PracticeEvents.Commands
{
    public record AttendeeAcceptEventCommand : IRequest
    {
        public AttendeeAcceptEventCommand(int eventId, int attendeeId)
        {
            EventId = eventId;
            AttendeeId = attendeeId;
        }

        public int EventId { get; }
        public int AttendeeId { get; }
    }

    public class AttendeeAcceptEventCommandHandler : IRequestHandler<AttendeeAcceptEventCommand>
    {
        private readonly IRepository<PracticeEvent> eventsRepo;

        public AttendeeAcceptEventCommandHandler(IRepository<PracticeEvent> eventsRepo)
        {
            this.eventsRepo = eventsRepo;
        }

        public async Task<Unit> Handle(AttendeeAcceptEventCommand request, CancellationToken cancellationToken)
        {
            var spec = new PracticeEventByIdWithAttendees(request.EventId);
            var practiceEvent = await eventsRepo.FirstOrDefaultAsync(spec, cancellationToken);
            if (practiceEvent == null)
            {
                throw new PracticeEventNotFoundException();
            }
            practiceEvent.AttendeeAcceptEvent(request.AttendeeId);
            await eventsRepo.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
