using MediatR;
using Microsoft.Extensions.Logging;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities;
using PracticeCalendar.Domain.Entities.Specifications;

namespace PracticeCalendar.Application.PracticeEvents.Queries.GetPracticeEvents
{
    public record class GetPracticeEventsQuery : IRequest<List<PracticeEvent>>
    {
    }

    public class GetPracticeEventsQueryHandler : IRequestHandler<GetPracticeEventsQuery, List<PracticeEvent>>
    {
        private readonly ILogger<GetPracticeEventsQueryHandler> logger;
        private readonly IRepository<PracticeEvent> eventsRepo;

        public GetPracticeEventsQueryHandler(IRepository<PracticeEvent> eventsRepo, 
            ILogger<GetPracticeEventsQueryHandler> logger)
        {
            this.eventsRepo = eventsRepo;
            this.logger = logger;
        }

        public async Task<List<PracticeEvent>> Handle(GetPracticeEventsQuery request, CancellationToken cancellationToken)
        {
            var spec = new PracticeEventsWithAttendees();
            var evList = await eventsRepo.ListAsync(spec);
            return evList;
        }
    }
}
