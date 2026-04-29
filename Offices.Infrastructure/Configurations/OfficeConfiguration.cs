using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Offices.Domain.Models;

namespace Offices.Infrastructure.Configurations;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.ToTable("Offices");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhotoPath)
            .IsRequired(false) 
            .HasMaxLength(500); 

        builder.Property(x => x.City)
            .IsRequired() 
            .HasMaxLength(100);

        builder.Property(x => x.Street)
            .IsRequired() 
            .HasMaxLength(200);

        builder.Property(x => x.HouseNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.OfficeNumber)
            .IsRequired() 
            .HasMaxLength(20);

        builder.Property(x => x.RegistryPhoneNumber)
            .IsRequired() 
            .HasMaxLength(20); 

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.Ignore(x => x.FullAddress);
    }
}