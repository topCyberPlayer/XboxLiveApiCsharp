using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using XblApp.Application;
using XblApp.Database.Contexts;
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
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
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
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IXboxLiveGamerService, GamerService>();
            builder.Services.AddScoped<IXboxLiveGameService, GameService>();
            builder.Services.AddScoped<AuthenticationUseCase>();
            builder.Services.AddScoped<GamerProfileUseCase>();
            builder.Services.AddScoped<GameUseCase>();
            
            builder.Services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<XblAppDbContext>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();

            string? dbProvider = builder.Configuration.GetConnectionString("DatabaseProvider");

            switch (dbProvider)
            {
                case "MsSql":
                    builder.Services.AddDbContext<XblAppDbContext, MsSqlDbContext>(options =>
                    {
                        options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"));
                        options.EnableSensitiveDataLogging();
                        options.AddInterceptors(new TotalAchievementsInterceptor());
                    });
                    break;
                case "PostgreSql":
                    builder.Services.AddDbContext<XblAppDbContext, PostgresDbContext>(options =>
                    {
                        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSqlConnection"));
                    });
                    break;
            }

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

                try
                {
                    bool arePendingMigrations = context.Database.GetPendingMigrations().Any();

                    if (arePendingMigrations)
                        await context.Database.MigrateAsync();

                    await context.SeedDatabase();
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
            var clientsConfig = configuration.GetSection("HttpClients").Get<Dictionary<string, HttpClientConfig>>();

            foreach (var clientConfig in clientsConfig)
            {
                services.AddHttpClient(clientConfig.Key, (HttpClient client) =>
                {
                    client.BaseAddress = new Uri(clientConfig.Value.BaseAddress);

                    foreach (var header in clientConfig.Value.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                });
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
