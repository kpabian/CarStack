using CarStack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarStack.Areas.Identity.Data;

public class CarDBContext(DbContextOptions<CarDBContext> options) : IdentityDbContext<CarStackUser, CarStackRole, int>(options)
{
    public DbSet<CarStackUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AplicationUserEntityConfiguration());

        builder.Entity<CarStackRole>().HasData(
            new CarStackRole { Id = 1, Name = "User", NormalizedName = "USER" },
            new CarStackRole { Id = 2, Name = "Moderator", NormalizedName = "MODERATOR" },
            new CarStackRole { Id = 3, Name = "Admin", NormalizedName = "ADMIN"}
        );
    }
}

public class AplicationUserEntityConfiguration : IEntityTypeConfiguration<CarStackUser>
{
    public void Configure(EntityTypeBuilder<CarStackUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
    }
}

