using Microsoft.AspNetCore.Identity;
using XblApp.Application;
using XblApp.Infrastructure;
using XblApp.Infrastructure.Contexts;
using XblApp.Infrastructure.Models;
using XblApp.InternalService;
using XblApp.XboxLiveService;

namespace XblApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.AddApplicationServices();
            builder.AddInfrastructureRepositoryServices();
            builder.AddInfrastructureInternalServices();
            builder.AddInfrastructureXblServices();

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<XblAppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHealthChecks();

            WebApplication? app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();

            app.MapHealthChecks("/isAlive");
            app.MapControllers();

            app.Run();
        }
    }
}
