using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities.Product;
using PracticeCalendar.Domain.Entities.Product.Specifications;

namespace PracticeCalendar.Application.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<List<ProductDto>>
    {

    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly ILogger<GetProductsQueryHandler> logger;
        private readonly IRepository<Product> eventsRepo;
        private readonly IMapper mapper;

        public GetProductsQueryHandler(IRepository<Product> eventsRepo,
            ILogger<GetProductsQueryHandler> logger,
            IMapper mapper)
        {
            this.eventsRepo = eventsRepo;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var spec = new AllProductsSpecification();
            var evList = await eventsRepo.ListAsync(spec, cancellationToken);
            var lst = evList.Adapt<List<ProductDto>>(mapper.Config);
            return lst;
        }
    }
}