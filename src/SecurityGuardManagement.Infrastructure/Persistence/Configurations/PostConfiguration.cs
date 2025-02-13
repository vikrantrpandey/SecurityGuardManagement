using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecurityGuardManagement.Domain.Entities;
using SecurityGuardManagement.Domain.ValueObjects;

namespace SecurityGuardManagement.Infrastructure.Persistence.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.OwnsOne(p => p.PostAddress, address =>
        {
            address.Property(a => a.Street).HasMaxLength(100);
            address.Property(a => a.City).HasMaxLength(50);
            address.Property(a => a.State).HasMaxLength(50);
            address.Property(a => a.Country).HasMaxLength(50);
            address.Property(a => a.PostalCode).HasMaxLength(20);
        });

        // Seed data
        var address = new Address
        {
            Street = "123 Main Street",
            City = "New York",
            State = "NY",
            Country = "USA",
            PostalCode = "10001"
        };

        builder.HasData(new
        {
            Id = 1,
            Name = "Main Office Security",
            Description = "24/7 security post at the main office entrance",
            ClientId = 1,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "system",
            LastModifiedAt = (DateTime?)null,
            LastModifiedBy = (string)null
        });

        builder.OwnsOne(p => p.PostAddress).HasData(new
        {
            PostId = 1,
            Street = address.Street,
            City = address.City,
            State = address.State,
            Country = address.Country,
            PostalCode = address.PostalCode
        });

        builder.HasData(new
        {
            Id = 2,
            Name = "Warehouse Security",
            Description = "Security post at the warehouse facility",
            ClientId = 1,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "system",
            LastModifiedAt = (DateTime?)null,
            LastModifiedBy = (string)null
        });

        builder.OwnsOne(p => p.PostAddress).HasData(new
        {
            PostId = 2,
            Street = "456 Industrial Ave",
            City = "New York",
            State = "NY",
            Country = "USA",
            PostalCode = "10002"
        });
    }
}
