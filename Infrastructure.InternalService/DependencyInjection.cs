using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.Options;

namespace XblApp.InternalService
{
    public static partial class DependencyInjection
    {
        public static void AddInfrastructureInternalServices(this IHostApplicationBuilder builder)
        {
            IConfigurationSection result = builder.Configuration.GetSection("Jwt");
            builder.Services.Configure<JwtOptions>(result);

            builder.Services.AddScoped<ITokenService, TokenService>();
            //builder.Services.AddSingleton<IEmailSender, EmailSenderService>();
        }
    }
}
