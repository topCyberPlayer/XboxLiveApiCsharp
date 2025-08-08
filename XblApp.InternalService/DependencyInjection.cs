using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XblApp.Domain.Interfaces;

namespace XblApp.InternalService
{
    public static partial class DependencyInjection
    {
        public static void AddInfrastructureInternalServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddSingleton<IEmailSender, EmailSenderService>();
        }
    }
}
