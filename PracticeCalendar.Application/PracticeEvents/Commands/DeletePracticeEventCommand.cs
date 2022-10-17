using MediatR;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities.PracticeEvent;
using PracticeCalendar.Domain.Exceptions;

namespace PracticeCalendar.Application.PracticeEvents.Commands
{
    public record DeletePracticeEventCommand : IRequest
    {
        public DeletePracticeEventCommand(int practiceEventId)
        {
            PracticeEventId = practiceEventId;
        }

        public int PracticeEventId { get; }
    }
    public class DeletePracticeEventCommandHandler : IRequestHandler<DeletePracticeEventCommand>
    {
        private readonly IRepository<PracticeEvent> eventsRepo;

        public DeletePracticeEventCommandHandler(IRepository<PracticeEvent> eventsRepo)
        {
            this.eventsRepo = eventsRepo;
        }

        public async Task<Unit> Handle(DeletePracticeEventCommand request, CancellationToken cancellationToken)
        {
            var org = await eventsRepo.GetByIdAsync(request.PracticeEventId, cancellationToken);
            if (org == null)
            {
                throw new PracticeEventNotFoundException();
            }
            await eventsRepo.DeleteAsync(org, cancellationToken);
            await eventsRepo.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
