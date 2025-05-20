using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Database.Models;
using XblApp.Database.Seeding;
using XblApp.DependencyInjection;

namespace XblApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.RegisterApplicationServices();
            WebApplication? app = builder.Build();

            await app.ConfigureMiddleware();
            app.RegisterEndpoints();

            app.Run();
        }
    }

    public static partial class ServiceInitializer
    {
        public static WebApplicationBuilder RegisterApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddApplicationDatabase(builder.Configuration)
            .AddApplicationIdentity();

            builder.Services.AddRazorPages(); // специфично для Razor
            builder.Services.AddHttpContextAccessor();

            return builder;
        }
    }

    public static partial class MiddlewareInitializer
    {
        public static async Task<WebApplication> ConfigureMiddleware(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            await app.SetupDatabaseAsync();//todo перенести в другое место

            app.UseAuthorization();

            return app;
        }
    }

    public static partial class EndpointMapper
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapRazorPages();

            return app;
        }
    }

    public static class DatabaseStartupHelpers
    {
        public static async Task<WebApplication> SetupDatabaseAsync(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = scope.ServiceProvider.GetRequiredService<XblAppDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var config = services.GetRequiredService<IConfiguration>();
                bool usingNoSqlDb = config.GetValue<bool>("Database:useNoSqlDb");//по умолчанию: false

                try
                {
                    if (!usingNoSqlDb)
                    {
                        bool arePendingMigrations = context.Database.GetPendingMigrations().Any();

                        if (arePendingMigrations)
                            await context.Database.MigrateAsync();

                        await context.SeedDbDefaultUserAsync(userManager);
                        //await context.SeedDbGamersAndGamesAsync();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return app;
        }
    }
}
