using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XblApp.Application.UseCases;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.Data;
using XblApp.Infrastructure.Data.Repositories;
using XblApp.Infrastructure.Data.Seeding;
using XblApp.Infrastructure.XboxLiveServices;

namespace XblApp
{   
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.RegisterApplicationServices();
            WebApplication? app = builder.Build();

            await app.ConfigureMiddleware();
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

            builder.Services.AddHttpClient<IXboxLiveGamerService, GamerService>(client =>
            {
                client.BaseAddress = new Uri("https://profile.xboxlive.com");
                client.DefaultRequestHeaders.Add("x-xbl-contract-version", "3");
            });

            builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            builder.Services.AddScoped<IGamerRepository, GamerRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IXboxLiveGamerService, GamerService>();
            //builder.Services.AddScoped<IXboxLiveGameService, GameService>();

            builder.Services.AddScoped<AuthenticationUseCase>();
            builder.Services.AddScoped<GamerProfileUseCase>();
            builder.Services.AddScoped<GameUseCase>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<XblAppDbContext>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            

            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();


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

        }
    }

    public static partial class MiddlewareInitializer
    {
        public static async Task<WebApplication> ConfigureMiddleware(this WebApplication app)
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
            await app.SetupDatabaseAsync();

            app.UseAuthorization();

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
