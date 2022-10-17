using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticeCalendar.Domain.Entities.PracticeEvent;

namespace PracticeCalendar.Infrastructure.Persistence.Configuration
{
    public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.EmailAddress)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
