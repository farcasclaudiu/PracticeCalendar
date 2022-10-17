using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticeCalendar.Domain.Entities.PracticeEvent;

namespace PracticeCalendar.Infrastructure.Persistence.Configuration
{
    public class PracticeEventConfiguration : IEntityTypeConfiguration<PracticeEvent>
    {
        public void Configure(EntityTypeBuilder<PracticeEvent> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(120)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
