using Microsoft.AspNetCore.Identity;
using XblApp.Application;
using XblApp.Infrastructure;
using XblApp.Infrastructure.Contexts;
using XblApp.Infrastructure.Models;
using XblApp.InternalService;
using XblApp.XboxLiveService;

namespace XblApp.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.AddApplicationServices();
            builder.AddInfrastructureRepositoryServices();
            builder.AddInfrastructureInternalServices();
            builder.AddInfrastructureXblServices();

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<XblAppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureCookieAuthentication();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();

            WebApplication? app = builder.Build();

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

            //app.UseHttpsRedirection();
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
