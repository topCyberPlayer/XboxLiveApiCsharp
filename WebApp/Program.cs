using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Services;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            
            string? connectionString = builder.Configuration.GetConnectionString("WebAppContext")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            builder.Services.RegisterApplicationServices(connectionString);

            WebApplication app = builder.Build();
            app.ConfigureMiddleware();
            app.RegisterEndpoints();

            app.Run();
        }
    }

    public static partial class ServiceInitializer
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<AuthenticationServiceXbl>();
            services.AddScoped<AuthenticationServiceDb>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<GamerService>();
            services.AddScoped<GameService>();

            services.AddHttpClient();
            services.AddDbContext<WebSiteContext>(options => options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<WebSiteContext>();
            services.AddRazorPages();

            return services;
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
                app.UseDeveloperExceptionPage();
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
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }

    public static partial class EndpointMapper
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapRazorPages();

            app.MapGet("/test", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            app.MapGet("/secret", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}. My secret")
                .RequireAuthorization();

            return app;
        }
    }
}