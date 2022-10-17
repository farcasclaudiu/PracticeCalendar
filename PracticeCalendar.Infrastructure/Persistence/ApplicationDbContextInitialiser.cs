using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PracticeCalendar.Domain.Entities.PracticeEvent;

namespace PracticeCalendar.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> logger;
        private readonly ApplicationDbContext context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
            ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (context.Database.IsSqlite())
                {
                    await context.Database.EnsureCreatedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default data
            // Seed, if necessary
            if (!context.PracticeEvents.Any())
            {
                context.PracticeEvents.Add(new PracticeEvent("Event 1", "Event 1 desc",
                    DateTime.Now.AddHours(-1),
                    DateTime.Now.AddHours(1)));

                await context.SaveChangesAsync();
            }
        }
    }
}
