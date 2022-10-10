using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PracticeCalendar.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(new Mapster.TypeAdapterConfig()));
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
