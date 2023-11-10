using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Services.Authentication;
using WebApp.Services.ProfileUser;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.RegisterApplicationServices();

            WebApplication app = builder.Build();
            app.ConfigureMiddleware();
            app.RegisterEndpoints();

            app.Run();
        }
    }

    public static partial class ServiceInitializer
    {
        public static IServiceCollection RegisterApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();

            string? connectionString = builder.Configuration.GetConnectionString("WebAppContext") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<WebAppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<WebAppDbContext>();
            
            builder.Services.AddRazorPages();
            
            builder.Services.AddScoped<AuthenticationProviderJson>();
            builder.Services.AddScoped<AuthenticationProviderDb>();
            builder.Services.AddScoped<ProfileUserProviderDb>();
            builder.Services.AddScoped<ProfileUserLogic>();
            builder.Services.AddScoped<ProfileUserProviderJson>();

            return builder.Services;
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (HttpContext context, Func<Task> next) =>
            {
                //SignInManager<IdentityUser> signIn = new();
                //await signIn.ExternalLoginSignInAsync("", "", true);
                //var a = await signIn.GetExternalLoginInfoAsync();
                //var b = await signIn.GetExternalAuthenticationSchemesAsync();
                //context.
                // Do work that can write to the Response.
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });

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