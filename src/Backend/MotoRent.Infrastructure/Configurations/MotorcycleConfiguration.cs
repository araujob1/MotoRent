using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoRent.Domain.Entities;
using MotoRent.Domain.ValueObjects;

namespace MotoRent.Infrastructure.Configurations;

public class MotorcycleConfiguration : EntityBaseConfiguration<Motorcycle>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.ToTable("motorcycle");

        builder.Property(m => m.LicensePlate)
            .HasColumnName("license_plate")
            .HasConversion(v => v.Value, v => new LicensePlate(v))
            .IsRequired();

        builder.Property(m => m.Vin)
            .HasColumnName("vin")
            .HasConversion(v => v.Value, v => new Vin(v))
            .IsRequired();

        builder.Property(m => m.Model)
            .HasColumnName("model")
            .IsRequired();

        builder.Property(m => m.Brand)
            .HasColumnName("brand")
            .IsRequired();

        builder.Property(m => m.Year)
            .HasColumnName("year")
            .IsRequired();

        builder.HasIndex(m => m.LicensePlate)
            .HasDatabaseName("idx_motorcycle_license_plate")
            .IsUnique();

        builder.HasIndex(m => m.Vin)
            .HasDatabaseName("idx_motorcycle_vin")
            .IsUnique();
    }
}
