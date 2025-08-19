using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XblApp.Application.InnerUseCases;
using XblApp.Application.XboxLiveUseCases;

namespace XblApp.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<RegisterUserUseCase>();
            builder.Services.AddScoped<AuthenticationUseCase>();
            builder.Services.AddScoped<GamerProfileUseCase>();
            builder.Services.AddScoped<GameUseCase>();
        }
    }
}
