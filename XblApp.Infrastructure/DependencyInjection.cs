using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.Interfaces.Repository;
using XblApp.Infrastructure.Contexts;
using XblApp.Infrastructure.Models;
using XblApp.Infrastructure.Repositories;

namespace XblApp.Infrastructure
{
    public static partial class DependencyInjection
    {
        public static void AddInfrastructureRepositoryServices(this IHostApplicationBuilder builder)
        {
            string? connectionString = builder.Configuration.GetConnectionString("SqlConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            builder.Services.AddScoped<IGamerRepository, GamerRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();
        }
    }
}
