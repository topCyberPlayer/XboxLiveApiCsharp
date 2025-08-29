using Application;
using Infrastructure;
using Infrastructure.InternalService;
using Infrastructure.Seed;
using Infrastructure.XboxLiveService;

namespace XblApp.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
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

            builder.Services.ConfigureCookieAuthentication();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();

            WebApplication? app = builder.Build();

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

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();

            await app.InitializeInfrastructureIdentityAsync();

            app.Run();
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
