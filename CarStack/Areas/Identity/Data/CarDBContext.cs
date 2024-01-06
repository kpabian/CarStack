using CarStack.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarStack.Areas.Identity.Data;

public class CarDBContext(DbContextOptions<CarDBContext> options) : IdentityDbContext<CarStackUser, CarStackRole, int>(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<CarStackUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AplicationUserEntityConfiguration());

        //Configuration for Car-Manufacturer
        builder.Entity<Car>()
            .HasOne(e => e.Manufacturer)
                .WithMany(o => o.Cars)
                .HasForeignKey(e => e.ManufacturerId);

        //Configuration for Car-User
        builder.Entity<Car>()
            .HasOne(e => e.Manufacturer)
                .WithMany(o => o.Cars)
                .HasForeignKey(e => e.ManufacturerId);

        builder.Entity<Manufacturer>().HasData(
            new Manufacturer { Id = 1, Name = "Audi", City = "Ingolstadt", Country = "Germany", PostalCode = "85045" },
            new Manufacturer { Id = 2, Name = "BMW", City = "Munich", Country = "Germany", PostalCode = "80331" },
            new Manufacturer { Id = 3, Name = "Mercedes-Benz", City = "Stuttgart", Country = "Germany", PostalCode = "70546" },
            new Manufacturer { Id = 4, Name = "Volkswagen", City = "Wolfsburg", Country = "Germany", PostalCode = "38436" },
            new Manufacturer { Id = 5, Name = "Toyota", City = "Toyota City", Country = "Japan", PostalCode = "471-8571" },
            new Manufacturer { Id = 6, Name = "Ford", City = "Dearborn", Country = "USA", PostalCode = "48126" },
            new Manufacturer { Id = 7, Name = "Chevrolet", City = "Detroit", Country = "USA", PostalCode = "48202" },
            new Manufacturer { Id = 8, Name = "Honda", City = "Tokyo", Country = "Japan", PostalCode = "108-8331" },
            new Manufacturer { Id = 9, Name = "Nissan", City = "Yokohama", Country = "Japan", PostalCode = "220-0012" },
            new Manufacturer { Id = 10, Name = "Hyundai", City = "Seoul", Country = "South Korea", PostalCode = "04514" }
        );

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

