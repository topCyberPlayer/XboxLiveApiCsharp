using XblApp.DependencyInjection;

namespace XblApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
            .AddInfrastructureServices(builder.Configuration)
            .AddApplicationDatabase(builder.Configuration)
            .AddApplicationIdentity();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();
            //app.MapUsersEndpoints();
            //await app.SetupApplicationDatabaseAsync();
            app.MapControllers();

            app.Run();
        }
    }
}
