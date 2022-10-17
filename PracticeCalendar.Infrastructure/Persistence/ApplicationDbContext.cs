using Microsoft.EntityFrameworkCore;
using PracticeCalendar.Domain.Common;
using PracticeCalendar.Domain.Common.Interfaces;
using PracticeCalendar.Domain.Entities.PracticeEvent;
using PracticeCalendar.Domain.Entities.Product;
using System.Reflection;

namespace PracticeCalendar.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDomainEventService domainEventService;

        public DbSet<Attendee> Atendees => Set<Attendee>();
        public DbSet<PracticeEvent> PracticeEvents => Set<PracticeEvent>();

        public DbSet<Product> Products => Set<Product>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventService domainEventService)
      : base(options)
        {
            this.domainEventService = domainEventService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var events = ChangeTracker.Entries<EntityBase>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(events);

            return result;
        }

        private async Task DispatchEvents(DomainEventBase[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await domainEventService.Publish(@event);
            }
        }
    }
}
