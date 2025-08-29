using Application;
using Infrastructure;
using Infrastructure.InternalService;
using Infrastructure.Seed;
using Infrastructure.XboxLiveService;
using XblApp.API.Middlewares;

namespace XblApp.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Environment.EnvironmentName = "Development";

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            builder.AddApplicationServices();
            builder.AddInfrastructureRepositoryServices();
            builder.AddInfrastructureInternalServices();
            builder.AddInfrastructureXblServices();

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHealthChecks();

            WebApplication? app = builder.Build();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();

            app.MapHealthChecks("/isAlive");
            app.MapControllers();

            await app.InitializeInfrastructureIdentityAsync();

            app.Run();
        }
    }
}
