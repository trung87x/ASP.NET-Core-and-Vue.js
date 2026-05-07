using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelApp.Domain.Entities;

namespace TravelApp.Infrastructure.Persistence.Configurations
{
    public class TourPackageConfiguration : IEntityTypeConfiguration<TourPackage>
    {
        public void Configure(EntityTypeBuilder<TourPackage> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(t => t.WhatToExpect)
                .HasMaxLength(2000);

            builder.Property(t => t.MapLocation)
                .HasMaxLength(500);

            builder.Property(t => t.Price)
                .IsRequired();
        }
    }
}
