using Domain.Entities.JsonModels;
using Domain.Interfaces.XboxLiveService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XboxLiveService.AchievementServices;

namespace Infrastructure.XboxLiveService
{
    public static partial class DependencyInjection
    {
        public static void AddInfrastructureXblServices(this IHostApplicationBuilder builder)
        {
            builder.AddAuthenticationFromConfig();
            builder.AddHttpClientsFromConfig();

            builder.Services.AddScoped<IXboxLiveAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IXboxLiveGamerService, GamerService>();
            builder.Services.AddScoped<IXboxLiveGameService, GameService>();
            builder.Services.AddScoped<IXboxLiveAchievementService<AchievementX1Json>, AchievementX1Service>();
            builder.Services.AddScoped<IXboxLiveAchievementService<AchievementX360Json>, AchievementX360Service>();
            builder.Services.AddScoped<TokenHandler>();
        }
    }

    public static class HttpClientExtensions
    {
        public static void AddAuthenticationFromConfig(this IHostApplicationBuilder builder)
        {
            builder.Services.Configure<AuthenticationConfig>(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"]
                    ?? throw new ArgumentNullException("В secret.json не заполнен Microsoft:ClientId");

                options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"]
                    ?? throw new ArgumentNullException("В secret.json не заполнен Microsoft:ClientSecret");

                options.RedirectUri = builder.Configuration.GetConnectionString("RedirectUrl")
                    ?? throw new ArgumentNullException("В appsettings.json не заполнен RedirectUrl");
            });


            //AuthenticationConfig authConfig = new()
            //{
            //    ClientId = builder.Configuration["Authentication:Microsoft:ClientId"] ?? throw new ArgumentNullException("В secret.json не заполнен Microsoft:ClientId"),
            //    ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"] ?? throw new ArgumentNullException("В secret.json не заполнен Microsoft:ClientSecret"),
            //    //RedirectUri = builder.Configuration["ConnectionStrings:RedirectUrl"] или
            //    RedirectUri = builder.Configuration.GetConnectionString("RedirectUrl") ?? throw new ArgumentNullException("В appsettings.json не заполнен RedirectUrl")
            //};

            //builder.Services.AddSingleton(authConfig);
        }

        public static void AddHttpClientsFromConfig(this IHostApplicationBuilder builder)
        {
            Dictionary<string, HttpClientConfig> authConfigs = builder.Configuration
                .GetSection("XboxAuthenticationServices")
                .Get<Dictionary<string, HttpClientConfig>>()
                ?? throw new ArgumentNullException("В appsettings.json не заполнен раздел XboxAuthenticationServices");

            foreach (var authConfig in authConfigs)
            {
                builder.Services.AddHttpClient(authConfig.Key, (HttpClient client) =>
                {
                    client.BaseAddress = new Uri(authConfig.Value.BaseAddress);

                    foreach (var header in authConfig.Value.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                });
            }

            Dictionary<string, HttpClientConfig> xboxConfigs = builder.Configuration
                .GetSection("XboxServices")
                .Get<Dictionary<string, HttpClientConfig>>()
                ?? throw new ArgumentNullException("в appsettings.json не заполнен раздел XboxServices");

            foreach (var xboxConfig in xboxConfigs)
            {
                builder.Services.AddHttpClient(xboxConfig.Key, (HttpClient client) =>
                {
                    client.BaseAddress = new Uri(xboxConfig.Value.BaseAddress);

                    foreach (var header in xboxConfig.Value.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }).AddHttpMessageHandler<TokenHandler>();
            }
        }
    }

    public class HttpClientConfig
    {
        public required string BaseAddress { get; set; }
        public Dictionary<string, string> Headers { get; set; } = [];
    }

    public class AuthenticationConfig
    {
        public required string ClientId { get; set; }
        public required string? ClientSecret { get; set; }
        public required string RedirectUri { get; set; }
    }
}
