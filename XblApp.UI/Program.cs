using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XblApp.Application;
using XblApp.Database.Contexts;
using XblApp.Database.Models;
using XblApp.Database.Repositories;
using XblApp.Database.Seeding;
using XblApp.Domain.Interfaces;
using XblApp.XboxLiveService;

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
            builder.Services.Configure<AuthenticationConfig>(builder.Configuration.GetSection("Authentication:Microsoft"));
            builder.Services.AddHttpClientsFromConfig(builder.Configuration);
            
            builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            builder.Services.AddScoped<IGamerRepository, GamerRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();

            builder.Services.AddScoped<TokenHandler>();
            builder.Services.AddScoped<IXboxLiveAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IXboxLiveGamerService, GamerService>();
            builder.Services.AddScoped<IXboxLiveGameService, GameService>();
            builder.Services.AddScoped<IXboxLiveAchievementService, AchievementService>();
            builder.Services.AddScoped<AuthenticationUseCase>();
            builder.Services.AddScoped<GamerProfileUseCase>();
            builder.Services.AddScoped<GameUseCase>();

            builder.Services.AddDbContext<XblAppDbContext>(options =>
            {
                string? connectionString = builder.Configuration.GetConnectionString("MsSqlConnection");
                options.UseSqlServer(connectionString);
            });

            builder.Services
                .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<XblAppDbContext>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();

            string? dbProvider = builder.Configuration.GetConnectionString("DatabaseProvider");

            

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
            await app.SetupDatabaseAsync();

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

                try
                {
                    bool arePendingMigrations = context.Database.GetPendingMigrations().Any();

                    if (arePendingMigrations)
                        await context.Database.MigrateAsync();

                    await context.SeedDbDefaultUserAsync(userManager);
                    //await context.SeedDbGamersAndGamesAsync();
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return app;
        }
    }

    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClientsFromConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var authConfigs = configuration.GetSection("XboxAuthenticationServices").Get<Dictionary<string, HttpClientConfig>>();

            foreach (var authConfig in authConfigs)
            {
                services.AddHttpClient(authConfig.Key, (HttpClient client) =>
                {
                    client.BaseAddress = new Uri(authConfig.Value.BaseAddress);

                    foreach (var header in authConfig.Value.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                });
            }

            var xboxConfigs = configuration.GetSection("XboxServices").Get<Dictionary<string, HttpClientConfig>>();

            foreach (var xboxConfig in xboxConfigs)
            {
                services.AddHttpClient(xboxConfig.Key, (HttpClient client) =>
                {
                    client.BaseAddress = new Uri(xboxConfig.Value.BaseAddress);

                    foreach (var header in xboxConfig.Value.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }).AddHttpMessageHandler<TokenHandler>();
            }

            return services;
        }
    }

    public class HttpClientConfig
    {
        public string BaseAddress { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new();
    }
}
