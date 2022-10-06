using Microsoft.EntityFrameworkCore;
using PracticeCalendar.Domain.Entities;
using System.Reflection;

namespace PracticeCalendar.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Attendee> Atendees => Set<Attendee>();
        public DbSet<PracticeEvent> PracticeEvents => Set<PracticeEvent>();
    }
}
