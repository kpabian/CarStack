using CarStack.Areas.Identity.Data;
using CarStack.Repositories;
using CarStack.Repositories.IRepositories;
using CarStack.Services.IServices;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarStack;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("CarDBContextConnection") ?? throw new InvalidOperationException("Connection string 'CarDBContextConnection' not found.");

        builder.Services.AddDbContext<CarDBContext>(options => options.UseSqlite(connectionString));

        builder.Services.AddDefaultIdentity<CarStackUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<CarStackRole>()
            .AddEntityFrameworkStores<CarDBContext>();

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddControllersWithViews();
        builder.Services.AddMemoryCache();
        builder.Services.AddSession();

        // Register repositories
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // Register services
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
        builder.Services.AddScoped<IUserService, UserService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();
        app.MapRazorPages();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        List<CarStackUser> users = [
            new CarStackUser { UserName = "jan@user.com", Email = "jan@user.com", FirstName = "Jan", LastName = "U¿ytkowniczy", LicenseNumber = "0" },
            new CarStackUser { UserName = "andrzej@admin.com", Email = "andrzej@admin.com", FirstName = "Andrzej", LastName = "Admin", LicenseNumber = "0" },
        ];

        using var scope = app.Services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CarStackUser>>();
        string password = "Test123!";

        foreach (var user in users)
        {
            if (await userManager.FindByEmailAsync(user.Email!) != null) continue;
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user!, "User");
            if (user.LastName == "Admin") await userManager.AddToRoleAsync(user, "Admin");
        }

        app.Run();
    }
}
