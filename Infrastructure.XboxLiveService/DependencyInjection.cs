using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XblApp.Domain.Entities.JsonModels;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.XboxLiveService.AchievementServices;

namespace XblApp.XboxLiveService
{
    public static partial class DependencyInjection
    {
        public static void AddInfrastructureXblServices(this IHostApplicationBuilder builder)
        {
            IConfigurationSection a = builder.Configuration.GetSection("Authentication:Microsoft");
            builder.Services.Configure<AuthenticationConfig>(a);
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
        public static void AddHttpClientsFromConfig(this IHostApplicationBuilder builder)
        {
            var authConfigs = builder.Configuration.GetSection("XboxAuthenticationServices").Get<Dictionary<string, HttpClientConfig>>();

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

            var xboxConfigs = builder.Configuration.GetSection("XboxServices").Get<Dictionary<string, HttpClientConfig>>();

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
        public string BaseAddress { get; set; }
        public Dictionary<string, string> Headers { get; set; } = [];
    }
}
