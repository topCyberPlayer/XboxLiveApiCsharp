using XblApp.DependencyInjection;
using XblApp.Database.Extensions;

namespace XblApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables(); // ← важно для docker-compose

            builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddApplicationDatabase(builder.Configuration)
            .AddApplicationIdentity();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            builder.Services.AddRazorPages(); // специфично для Razor
            builder.Services.AddHttpContextAccessor();
            WebApplication? app = builder.Build();

            await app.SetupApplicationDatabaseAsync();
            app.ConfigureMiddleware();
            app.MapRazorPages();

            app.Run();
        }
    }

    public static partial class MiddlewareInitializer
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
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

            app.UseAuthorization();

            return app;
        }
    }

    
}
