using XblApp.DependencyInjection;
using XblApp.Database.Extensions;

namespace XblApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureAppConfiguration();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplicationDatabase(builder.Configuration);
            builder.Services.AddApplicationIdentity();
            builder.Services.ConfigureCookieAuthentication();
            builder.Services.AddRazorPages();
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

    public static class CookieAuthenticationExtensions
    {
        public static IServiceCollection ConfigureCookieAuthentication(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            return services;
        }
    }


}
