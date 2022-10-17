using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PracticeCalendar.Application.PracticeEvents.Queries;
using PracticeCalendar.Application.Products.Queries;
using PracticeCalendar.Domain.Entities.PracticeEvent;
using PracticeCalendar.Domain.Entities.Product;
using PracticeCalendar.Domain.ValueObjects;
using System.Reflection;

namespace PracticeCalendar.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMapsterMappings();
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }

        private static IServiceCollection AddMapsterMappings(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
            TypeAdapterConfig.GlobalSettings.NewConfig<PracticeEventDto, PracticeEvent>()
                .ConstructUsing(src => new PracticeEvent(src.Title, src.Description, src.StartTime, src.EndTime));

            var mapsterConfig = new TypeAdapterConfig();
            mapsterConfig.NewConfig<PracticeEventDto, PracticeEvent>()
                .MapToConstructor(true)
                .ConstructUsing(src => new PracticeEvent(src.Title, src.Description, src.StartTime, src.EndTime));
            mapsterConfig.NewConfig<ProductDto, Product>()
                .MapToConstructor(true)
                .ConstructUsing(src => new Product()
                {
                    Id = src.Id,
                    Category = src.Category,
                    Name = src.Name,
                    UnitPrice = new Price
                    {
                        Value = src.UnitPrice,
                        Currency = src.UnitPriceCurrency
                    }
                });
            mapsterConfig
                .ForType<Product, ProductDto>()
                .MapWith(src => new ProductDto
                {
                    Id = src.Id,
                    Name = src.Name,
                    Category = src.Category,
                    UnitPrice = src.UnitPrice.Value,
                    UnitPriceCurrency = src.UnitPrice.Currency
                })
                ;
            services.AddSingleton<IMapper>(new Mapper(mapsterConfig));

            return services;
        }
    }
}
