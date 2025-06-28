using XblApp.API.Endpoints;
using XblApp.DependencyInjection;
using XblApp.Database.Extensions;

namespace XblApp.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.RegisterApplicationServices();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            //app.MapUsersEndpoints();
            //await app.SetupApplicationDatabaseAsync();
            app.MapControllers();

            app.Run();
        }
    }
    public static partial class ServiceInitializer
    {
        public static WebApplicationBuilder RegisterApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddApplicationDatabase(builder.Configuration)
            .AddApplicationIdentity();

            builder.Services.AddHttpContextAccessor();

            return builder;
        }
    }

}
