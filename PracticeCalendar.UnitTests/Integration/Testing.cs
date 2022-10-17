using Microsoft.Extensions.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PracticeCalendar.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PracticeCalendar.Domain.Common.Interfaces;
using Moq;
using PracticeCalendar.Domain.Entities.PracticeEvent;

namespace PracticeCalendar.UnitTests.Integration
{
    public partial class Testing
    {
        private static WebApplicationFactory<Program> _factory = null!;
        private static IConfiguration _configuration = null!;
        private static IServiceScopeFactory _scopeFactory = null!;

        public static Mock<IDomainEventService> domainEventServiceMock = null!;

        public static async Task RunBeforeAnyTests()
        {
            domainEventServiceMock = new Mock<IDomainEventService>();

            _factory = new CustomWebApplicationFactory(cfg =>
            {
                cfg.AddSingleton(svc => domainEventServiceMock.Object);
            });

            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        }

        public static HttpClient GetHttpClient()
        {
            return _factory.CreateClient();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task<int> CountAsync<TEntity>() where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Set<TEntity>().CountAsync();
        }

        public static void ResetState()
        {
            if (_scopeFactory != null)
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Set<Attendee>().RemoveRange(context.Set<Attendee>().ToList());
                context.Set<PracticeEvent>().RemoveRange(context.Set<PracticeEvent>().ToList());
                context.SaveChanges();
            }
        }
    }
}
