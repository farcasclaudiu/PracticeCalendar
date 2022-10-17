using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticeCalendar.Domain.Entities.Product;
using PracticeCalendar.Domain.ValueObjects;

namespace PracticeCalendar.Infrastructure.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.Category)
                .HasMaxLength(100)
                .IsRequired();
            builder.OwnsOne(product => product.UnitPrice, price =>
            {
                price.Property(p => p.Currency)
                    .HasColumnName(nameof(Price.Currency))
                    .HasMaxLength(5)
                    .IsRequired();
                price.Property(p => p.Value)
                    .HasColumnName(nameof(Price.Value))
                    .IsRequired();
            });
        }
    }
}
