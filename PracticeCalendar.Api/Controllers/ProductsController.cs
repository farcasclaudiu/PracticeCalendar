using Microsoft.AspNetCore.Mvc;
using PracticeCalendar.API.Controllers;
using PracticeCalendar.Application.Products.Queries;
using PracticeCalendar.Application.Products.Queries.GetProducts;

namespace PracticeCalendar.Api.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        private readonly ILogger<ProductsController> logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get()
        {
            return await Mediator.Send(new GetProductsQuery());
        }
    }
}