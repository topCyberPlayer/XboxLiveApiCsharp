using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XblApp.Application.XboxLiveUseCases;
using XblApp.Database.Contexts;
using XblApp.Database.Models;
using XblApp.Database.Repositories;
using XblApp.Domain.Interfaces;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;
using XblApp.XboxLiveService;
using XblApp.XboxLiveService.XboxLiveServices;
using XblApp.XboxLiveService.XboxLiveServices.AchievementServices;

namespace XblApp.DependencyInjection
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationConfig>(configuration.GetSection("Authentication:Microsoft"));
            services.AddHttpClientsFromConfig(configuration);

            services.AddSingleton<IEmailSender, XboxLiveService.NoOpEmailSender>();

            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IGamerRepository, GamerRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IAchievementRepository, AchievementRepository>();

            services.AddScoped<TokenHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IXboxLiveAuthenticationService, AuthenticationService>();
            services.AddScoped<IXboxLiveGamerService, GamerService>();
            services.AddScoped<IXboxLiveGameService, GameService>();
            services.AddScoped<IXboxLiveAchievementService<AchievementX1Json>, AchievementX1Service>();
            services.AddScoped<IXboxLiveAchievementService<AchievementX360Json>, AchievementX360Service>();
            services.AddScoped<AuthenticationUseCase>();
            services.AddScoped<GamerProfileUseCase>();
            services.AddScoped<GameUseCase>();

            return services;
        }
    }

    public static partial class DatabaseDependencyInjection
    {
        public static IServiceCollection AddApplicationDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<XblAppDbContext>(options =>
            {
                string? connectionString = configuration.GetConnectionString("MsSqlConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }

    public static class IdentityDependencyInjection
    {
        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<XblAppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }

    public static class ConfigurationExtensions
    {
        public static WebApplicationBuilder ConfigureAppConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder;
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
        public Dictionary<string, string> Headers { get; set; } = [];
    }
}
