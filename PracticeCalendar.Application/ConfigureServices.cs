using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PracticeCalendar.Application.PracticeEvents.Queries;
using PracticeCalendar.Domain.Entities;
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
            services.AddSingleton<IMapper>(new Mapper(mapsterConfig));

            return services;
        }
    }
}
