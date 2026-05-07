using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelApp.Domain.Entities;

namespace TravelApp.Infrastructure.Persistence.Configurations
{
    public class TourListConfiguration : IEntityTypeConfiguration<TourList>
    {
        public void Configure(EntityTypeBuilder<TourList> builder)
        {
            builder.Property(t => t.City)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.Country)
                .HasMaxLength(200);

            builder.Property(t => t.About)
                .HasMaxLength(2000);
        }
    }
}
