using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities;
using PracticeCalendar.Domain.Entities.Specifications;

namespace PracticeCalendar.Application.PracticeEvents.Queries.GetPracticeEvents
{
    public record class GetPracticeEventsQuery : IRequest<List<PracticeEventDto>>
    {
    }

    public class GetPracticeEventsQueryHandler : IRequestHandler<GetPracticeEventsQuery, List<PracticeEventDto>>
    {
        private readonly ILogger<GetPracticeEventsQueryHandler> logger;
        private readonly IRepository<PracticeEvent> eventsRepo;
        private readonly IMapper mapper;

        public GetPracticeEventsQueryHandler(IRepository<PracticeEvent> eventsRepo,
            ILogger<GetPracticeEventsQueryHandler> logger,
            IMapper mapper)
        {
            this.eventsRepo = eventsRepo;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<List<PracticeEventDto>> Handle(GetPracticeEventsQuery request, CancellationToken cancellationToken)
        {
            var spec = new PracticeEventsWithAttendees();
            var evList = await eventsRepo.ListAsync(spec, cancellationToken);
            var lst = evList.Adapt<List<PracticeEventDto>>(mapper.Config);
            return lst;
        }
    }
}
