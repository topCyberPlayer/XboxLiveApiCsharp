using Microsoft.EntityFrameworkCore;
using XblApp.Application.UseCases;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.Data;
using XblApp.Infrastructure.Data.Repositories;
using XblApp.Infrastructure.Services;

namespace XblApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.RegisterApplicationServices();
            WebApplication? app = builder.Build();

            app.ConfigureMiddleware();
            app.RegisterEndpoints();

            app.Run();
        }
    }

    public static partial class ServiceInitializer
    {
        public static WebApplicationBuilder RegisterApplicationServices(this WebApplicationBuilder builder)
        {
            string? dbProvider = 
            builder.Configuration.GetConnectionString("DatabaseProvider");

            builder.Services.AddScoped<IXboxLiveService, XboxLiveService>();
            builder.Services.AddScoped<IGamerRepository, GamerRepository>();
            builder.Services.AddScoped<GamerProfileUseCase>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddHttpClient();
            builder.Services.AddRazorPages();


            switch (dbProvider)
            {
                case "MsSql":
                    builder.Services.AddDbContext<XblAppDbContext, MsSqlDbContext>();
                    break;
                case "PostgreSql":
                    builder.Services.AddDbContext<XblAppDbContext, PostgresDbContext>();
                    break;
            }

            return builder;

            //builder.Services.AddDbContext<XblAppDbContext>(options => options.UseSqlServer(connectionString));
            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }

    public static partial class MiddlewareInitializer
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.SetupDatabaseAsync();

            //app.UseAuthorization();

            return app;
        }
    }

    public static partial class EndpointMapper
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapRazorPages();

            return app;
        }
    }
}
