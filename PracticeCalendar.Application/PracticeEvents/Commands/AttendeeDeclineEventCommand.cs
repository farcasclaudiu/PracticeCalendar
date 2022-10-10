using MediatR;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities;
using PracticeCalendar.Domain.Entities.Specifications;
using PracticeCalendar.Domain.Exceptions;

namespace PracticeCalendar.Application.PracticeEvents.Commands
{
    public record AttendeeDeclineEventCommand : IRequest
    {
        public AttendeeDeclineEventCommand(int eventId, int attendeeId)
        {
            EventId = eventId;
            AttendeeId = attendeeId;
        }

        public int EventId { get; }
        public int AttendeeId { get; }
    }

    public class AttendeeDeclineEventCommandHandler : IRequestHandler<AttendeeDeclineEventCommand>
    {
        private readonly IRepository<PracticeEvent> eventsRepo;

        public AttendeeDeclineEventCommandHandler(IRepository<PracticeEvent> eventsRepo)
        {
            this.eventsRepo = eventsRepo;
        }

        public async Task<Unit> Handle(AttendeeDeclineEventCommand request, CancellationToken cancellationToken)
        {
            var spec = new PracticeEventByIdWithAttendees(request.EventId);
            var practiceEvent = await eventsRepo.FirstOrDefaultAsync(spec, cancellationToken);
            if (practiceEvent == null)
            {
                throw new PracticeEventNotFoundException();
            }
            practiceEvent.AttendeeDeclineEvent(request.AttendeeId);
            
            return Unit.Value;
        }
    }
}
