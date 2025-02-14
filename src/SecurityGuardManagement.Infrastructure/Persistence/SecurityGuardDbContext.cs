using Microsoft.EntityFrameworkCore;
using SecurityGuardManagement.Domain.Entities;
using SecurityGuardManagement.Domain.ValueObjects;
using SecurityGuardManagement.Domain.Common;
using SecurityGuardManagement.Domain.Enums;

namespace SecurityGuardManagement.Infrastructure.Persistence;

public class SecurityGuardDbContext : DbContext
{
    public SecurityGuardDbContext(DbContextOptions<SecurityGuardDbContext> options) : base(options)
    {
    }

    public DbSet<Guard> Guards { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<GuardAssignment> GuardAssignments { get; set; }
    public DbSet<GuardEducation> GuardEducations { get; set; }
    public DbSet<GuardTraining> GuardTrainings { get; set; }
    public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
    public DbSet<GuardAddress> GuardAddresses { get; set; }
    public DbSet<GuardLevel> GuardLevels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure value objects as owned entities
        modelBuilder.Entity<Guard>().OwnsOne(g => g.PersonalInfo);
        modelBuilder.Entity<Guard>().OwnsOne(g => g.ContactInfo);
        modelBuilder.Entity<Client>().OwnsOne(c => c.ContactInfo);
        modelBuilder.Entity<Post>().OwnsOne(p => p.PostAddress);
        modelBuilder.Entity<GuardAddress>().OwnsOne(ga => ga.Address);

        // Configure relationships
        modelBuilder.Entity<GuardAssignment>()
            .HasOne(ga => ga.Guard)
            .WithMany(g => g.Assignments)
            .HasForeignKey(ga => ga.GuardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GuardAssignment>()
            .HasOne(ga => ga.Post)
            .WithMany(p => p.Assignments)
            .HasForeignKey(ga => ga.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GuardEducation>()
            .HasOne(ge => ge.Guard)
            .WithMany(g => g.Education)
            .HasForeignKey(ge => ge.GuardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GuardTraining>()
            .HasOne(gt => gt.Guard)
            .WithMany(g => g.Training)
            .HasForeignKey(gt => gt.GuardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EmploymentHistory>()
            .HasOne(eh => eh.Guard)
            .WithMany(g => g.EmploymentHistory)
            .HasForeignKey(eh => eh.GuardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Client)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GuardAddress>()
            .HasOne(ga => ga.Guard)
            .WithMany()
            .HasForeignKey(ga => ga.GuardId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Guard>()
            .HasOne(g => g.GuardLevel)
            .WithMany()
            .HasForeignKey(g => g.GuardLevelId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure unique indexes
        modelBuilder.Entity<Guard>()
            .HasIndex(g => g.GuardIdNumber)
            .IsUnique();

        modelBuilder.Entity<Guard>()
            .HasIndex(g => g.PANNumber)
            .IsUnique()
            .HasFilter("[PANNumber] IS NOT NULL");

        modelBuilder.Entity<Guard>()
            .HasIndex(g => g.SocialSecurityNumber)
            .IsUnique()
            .HasFilter("[SocialSecurityNumber] IS NOT NULL");

        // Configure decimal precision
        modelBuilder.Entity<GuardLevel>()
            .Property(gl => gl.BaseSalary)
            .HasPrecision(18, 2);

        // Seed GuardLevel data
        var seedTime = new DateTime(2025, 1, 1);
        modelBuilder.Entity<GuardLevel>().HasData(
            new GuardLevel
            {
                Id = 1,
                Title = "Junior Guard",
                Description = "Entry level security guard",
                BaseSalary = 25000M,
                AuthorizationLevel = 1,
                CreatedAt = seedTime
            },
            new GuardLevel
            {
                Id = 2,
                Title = "Senior Guard",
                Description = "Experienced security guard",
                BaseSalary = 35000M,
                AuthorizationLevel = 2,
                CreatedAt = seedTime
            },
            new GuardLevel
            {
                Id = 3,
                Title = "Supervisor",
                Description = "Security supervisor",
                BaseSalary = 45000M,
                AuthorizationLevel = 3,
                CreatedAt = seedTime
            }
        );

        // Seed Client data
        modelBuilder.Entity<Client>().HasData(
            new
            {
                Id = 1,
                Name = "Example Corporation",
                Status = ClientStatus.Active,
                CreatedAt = seedTime
            }
        );

        modelBuilder.Entity<Client>()
            .OwnsOne(c => c.ContactInfo)
            .HasData(
                new
                {
                    ClientId = 1,
                    Phone = "+9779841000000",
                    Email = "contact@example.com"
                }
            );

        // Seed Post data
        modelBuilder.Entity<Post>().HasData(
            new
            {
                Id = 1,
                ClientId = 1,
                Name = "Main Office Security",
                Description = "Main entrance security post",
                Status = PostStatus.Active,
                RequiredGuardCount = 2,
                CreatedAt = seedTime
            }
        );

        modelBuilder.Entity<Post>()
            .OwnsOne(p => p.PostAddress)
            .HasData(
                new
                {
                    PostId = 1,
                    Street = "123 Main Street",
                    City = "Kathmandu",
                    District = "Kathmandu",
                    Province = "Bagmati",
                    State = "Bagmati",
                    Country = "Nepal",
                    PostalCode = "44600"
                }
            );
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
