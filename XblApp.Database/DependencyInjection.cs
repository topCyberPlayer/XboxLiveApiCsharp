using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Domain.Interfaces.IRepository;

namespace XblApp.Database
{
    public static partial class DependencyInjection
    {
        public static void AddInfrastructureRepositoryServices(this IHostApplicationBuilder builder)
        {
            string? connectionString = builder.Configuration.GetConnectionString("MsSqlConnection");

            builder.Services.AddDbContext<XblAppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            builder.Services.AddScoped<IGamerRepository, GamerRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();
        }
    }
}
