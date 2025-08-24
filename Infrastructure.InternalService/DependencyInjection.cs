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
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

            builder.Services.AddScoped<ITokenService, TokenService>();
            //builder.Services.AddSingleton<IEmailSender, EmailSenderService>();
        }
    }
}
