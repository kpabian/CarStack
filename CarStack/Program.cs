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
        app.Run();
    }
}
