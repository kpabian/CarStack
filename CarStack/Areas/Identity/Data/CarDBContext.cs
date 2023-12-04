using CarStack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarStack.Areas.Identity.Data;

public class CarDBContext(DbContextOptions<CarDBContext> options) : IdentityDbContext<CarStackUser, CarStackRole, int>(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AplicationUserEntityConfiguration());

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

